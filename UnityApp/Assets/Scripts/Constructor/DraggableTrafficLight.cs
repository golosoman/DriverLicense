using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableTrafficLight : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public GameObject trafficLightPrefab; // Префаб светофора
    public Canvas canvas; // Ссылка на Canvas
    private RectTransform rectTransform;
    private Vector2 originalPosition;
    private Image image; // Ссылка на компонент Image
    private Color originalColor; // Оригинальный цвет объекта
    private Color highlightColor = Color.green; // Цвет при перетаскивании
    private EventTrigger eventTrigger; // Ссылка на EventTrigger

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

        // Меняем цвет на зеленый при перетаскивании
        ChangeColorToHighlight();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Проверяем, попадает ли объект на точку спавна
        RaycastHit2D hit = Physics2D.Raycast(rectTransform.position, Vector2.zero);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag(TagObjectNamesTypes.TRAFFIC_LIGHT_SPAWN))
            {
                ChangeColorToOriginal();
                GameObject trafficLight = Instantiate(trafficLightPrefab, hit.collider.transform.position, Quaternion.Euler(0, 0, hit.collider.transform.eulerAngles.z));
                trafficLight.transform.SetParent(canvas.transform, false);
                trafficLight.transform.position = hit.collider.transform.position;

                // Создаем данные для светофора
                PlacedTrafficLightData trafficLightData = new PlacedTrafficLightData
                {
                    modelName = trafficLight.name,
                    sidePosition = hit.collider.GetComponent<TriggerTrafficLightZone>().sidePosition.ToString(),
                    state = "Red", // начальное состояние
                    srcBySpawnPoint = hit.collider.GetComponent<TriggerTrafficLightZone>()
                };

                // Удаляем компонент DraggableObject
                Destroy(trafficLight.GetComponent<DraggableCar>());

                // Добавляем компонент ClickableObject к машине
                trafficLight.AddComponent<ClickableObject>();

                // Добавляем объект в список размещенных светофоров
                FindObjectOfType<SceneBuilder>().AddPlacedTrafficLightObject(trafficLight, trafficLightData);

                rectTransform.anchoredPosition = originalPosition;
                Debug.Log("Светофор успешно размещен!");
            }
            else
            {
                ChangeColorToOriginal();
                rectTransform.anchoredPosition = originalPosition;
            }
        }
        else
        {
            ChangeColorToOriginal();
            rectTransform.anchoredPosition = originalPosition;
            Debug.Log("Не попал в точку!");
        }
    }


    private void ChangeColorToOriginal()
    {
        image.color = originalColor; // Меняем цвет на оригинальный
    }

    private void ChangeColorToHighlight()
    {
        image.color = highlightColor; // Меняем цвет на зеленый
    }
}
