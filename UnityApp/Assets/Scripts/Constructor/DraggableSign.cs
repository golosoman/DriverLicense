using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableSign : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public GameObject signPrefab; // Префаб дорожного знака
    public Canvas canvas; // Ссылка на Canvas
    private RectTransform rectTransform;
    private Vector2 originalPosition;
    private Image image; // Ссылка на компонент Image
    private Color originalColor; // Оригинальный цвет объекта
    private Color highlightColor = Color.yellow; // Цвет при перетаскивании
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

        // Меняем цвет на желтый при перетаскивании
        ChangeColorToHighlight();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Проверяем, попадает ли объект на точку спавна
        RaycastHit2D hit = Physics2D.Raycast(rectTransform.position, Vector2.zero);
        if (hit.collider != null)
        {
            // Проверяем, имеет ли коллайдер тег signSpawn
            if (hit.collider.CompareTag(TagObjectNamesTypes.SIGN_SPAWN))
            {
                ChangeColorToOriginal(); // Меняем цвет на оригинальный
                // Инстанцируем префаб дорожного знака с правильным вращением
                GameObject sign = Instantiate(signPrefab, hit.collider.transform.position, Quaternion.Euler(0, 0, hit.collider.transform.eulerAngles.z));

                // Устанавливаем объект в центр точки спавна и делаем его дочерним элементом Canvas
                sign.transform.SetParent(canvas.transform, false);
                sign.transform.position = hit.collider.transform.position; // Устанавливаем позицию

                // Добавляем компонент ClickableObject к знаку
                sign.AddComponent<ClickableObject>();

                // Добавляем объект в список размещенных объектов
                FindObjectOfType<SceneBuilder>().AddPlacedSignObject(sign); // Метод для добавления знаков

                rectTransform.anchoredPosition = originalPosition;
                Debug.Log("Знак успешно размещен!");
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

    private void ChangeColorToOriginal()
    {
        image.color = originalColor; // Меняем цвет на оригинальный
    }

    private void ChangeColorToHighlight()
    {
        image.color = highlightColor; // Меняем цвет на желтый
    }
}
