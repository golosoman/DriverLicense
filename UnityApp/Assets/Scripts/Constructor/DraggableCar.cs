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
    public BoxCollider2D boxCollider; // Ссылка на BoxCollider2D
    public Rigidbody2D rb; // Ссылка на Rigidbody2D
    private bool turnColor = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition; // Сохраняем исходную позицию
        // Получаем компонент Image и сохраняем оригинальный цвет
        image = GetComponent<Image>();
        originalColor = image.color;

        // Отключаем компоненты по умолчанию
        if (boxCollider != null) boxCollider.enabled = false;
        if (rb != null) rb.simulated = false; // Отключаем физику
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!turnColor)
        {
            ChangeColorToHighlight();
            turnColor = !turnColor;
            // Включаем компоненты при перетаскивании
            if (boxCollider != null) boxCollider.enabled = true;
            if (rb != null) rb.simulated = true; // Включаем физику
        }

        // Перемещение объекта, учитывая масштаб Canvas
        float scaleFactor = canvas.scaleFactor;
        rectTransform.anchoredPosition += eventData.delta / scaleFactor;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        // Отключаем компоненты после завершения перетаскивания
        if (boxCollider != null) boxCollider.enabled = false;
        if (rb != null) rb.simulated = false; // Отключаем физику
        turnColor = !turnColor;

        // Проверяем, попадает ли объект на точку спавна
        RaycastHit2D hit = Physics2D.Raycast(rectTransform.position, Vector2.zero);
        if (hit.collider != null)
        {

            // Проверяем, имеет ли коллайдер тег CarSpawn
            if (hit.collider.CompareTag(TagObjectNamesTypes.CAR_SPAWN))
            {
                TriggerCarSpawnZone spawnZone = hit.collider.GetComponent<TriggerCarSpawnZone>();

                // Проверяем, есть ли уже автомобиль на точке спавна
                if (spawnZone.currentCar != null)
                {
                    Debug.LogWarning($"На точке спавна уже есть автомобиль: {spawnZone.currentCar.name}");
                    ChangeColorToOriginal();
                    rectTransform.anchoredPosition = originalPosition;
                    return;
                }

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
