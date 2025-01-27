using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableCar : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public GameObject carPrefab; // Префаб машины
    public Canvas canvas; // Ссылка на Canvas
    private RectTransform rectTransform;
    private Vector2 originalPosition;
    private Image image; // Ссылка на компонент Image
    private Color originalColor; // Оригинальный цвет объекта
    private Color highlightColor = Color.red; // Цвет при перетаскивании

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition; // Сохраняем исходную позицию
        // Получаем компонент Image и сохраняем оригинальный цвет
        image = GetComponent<Image>();
        originalColor = image.color;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Перемещение объекта, учитывая масштаб Canvas
        float scaleFactor = canvas.scaleFactor;
        rectTransform.anchoredPosition += eventData.delta / scaleFactor;

        // Проверяем пересечение с другими UI элементами
        ChangeColorToHighlight();
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        // Проверяем, попадает ли объект на точку спавна
        RaycastHit2D hit = Physics2D.Raycast(rectTransform.position, Vector2.zero);
        if (hit.collider != null)
        {

            // Проверяем, имеет ли коллайдер тег CarSpawn
            if (hit.collider.CompareTag(TagObjectNamesTypes.CAR_SPAWN))
            {
                TriggerCarSpawnZone spawnZone = hit.collider.GetComponent<TriggerCarSpawnZone>();
                ChangeColorToOriginal(); // Меняем цвет на оригинальный
                // Инстанцируем префаб машины с правильным вращением
                GameObject car = Instantiate(carPrefab, hit.collider.transform.position, Quaternion.Euler(0, 0, hit.collider.transform.eulerAngles.z));

                // Здесь мы можем получить информацию о точке спавна
                string direction = spawnZone.allowedDirections[0].ToString(); // Берем первое разрешенное направление
                int laneNumber = spawnZone.laneNumber;
                string sidePosition = spawnZone.sidePosition.ToString(); ;

                PlacedObjectData data = new PlacedObjectData
                {
                    participantType = "Car",
                    modelName = car.name, // или другой способ получения имени модели
                    direction = direction,
                    numberPosition = laneNumber.ToString(),
                    sidePosition = sidePosition,
                    srcBySpawnPoint = spawnZone
                };

                // Логируем информацию о размещенном автомобиле
                Debug.Log($"Автомобиль размещен: {data.modelName}, Направление: {data.direction}, Полоса: {data.numberPosition}, Сторона: {data.sidePosition}");

                // Устанавливаем объект в центр точки спавна и делаем его дочерним элементом Canvas
                car.transform.SetParent(canvas.transform, false);
                car.transform.position = hit.collider.transform.position; // Устанавливаем позицию

                // Удаляем компонент DraggableObject
                Destroy(car.GetComponent<DraggableCar>());

                // Добавляем компонент ClickableObject к машине
                car.AddComponent<ClickableObject>();

                // Добавляем объект в список размещенных объектов
                FindObjectOfType<SceneBuilder>().AddPlacedCarObject(car, data);

                rectTransform.anchoredPosition = originalPosition;
                Debug.Log("Да неужели попал");
            }
            else
            {
                // Если не попадает на точку спавна, возвращаем на исходную позицию
                ChangeColorToOriginal(); // Меняем цвет на оригинальный
                rectTransform.anchoredPosition = originalPosition;
            }
        }
        else
        {
            // Если не попадает на точку спавна, возвращаем на исходную позицию
            ChangeColorToOriginal(); // Меняем цвет на оригинальный
            rectTransform.anchoredPosition = originalPosition;
            Debug.Log("Не попал в точку!");
        }
    }

    public void ChangeColorToOriginal()
    {
        image.color = originalColor; // Меняем цвет на оригинальный
    }

    public void ChangeColorToHighlight()
    {
        image.color = highlightColor; // Меняем цвет на красный
    }
}
