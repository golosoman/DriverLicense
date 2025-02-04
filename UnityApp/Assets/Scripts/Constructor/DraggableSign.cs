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
            // Проверяем, имеет ли коллайдер тег signSpawn
            if (hit.collider.CompareTag(TagObjectNamesTypes.SIGN_SPAWN))
            {
                TriggerSignSpawnZone spawnZone = hit.collider.GetComponent<TriggerSignSpawnZone>();

                // Проверяем, есть ли уже знак на точке спавна
                if (spawnZone.currentSign != null)
                {
                    Debug.LogWarning($"На точке спавна уже есть знак: {spawnZone.currentSign.name}");
                    ChangeColorToOriginal();
                    rectTransform.anchoredPosition = originalPosition;
                    return;
                }

                ChangeColorToOriginal(); // Меняем цвет на оригинальный

                // Инстанцируем префаб дорожного знака с правильным вращением
                GameObject sign = Instantiate(signPrefab, hit.collider.transform.position, Quaternion.Euler(0, 0, hit.collider.transform.eulerAngles.z));

                // Устанавливаем объект в центр точки спавна и делаем его дочерним элементом Canvas
                sign.transform.SetParent(canvas.transform, false);
                sign.transform.position = hit.collider.transform.position; // Устанавливаем позицию

                // Добавляем компонент ClickableObject к знаку
                sign.AddComponent<ClickableObject>();

                // Получаем информацию о точке спавна
                string sidePosition = spawnZone.sidePosition.ToString();

                PlacedSignData signData = new PlacedSignData
                {
                    modelName = sign.name, // или другой способ получения имени модели
                    sidePosition = sidePosition,
                    srcBySpawnPoint = spawnZone
                };

                // Логируем информацию о размещенном знаке
                Debug.Log($"Знак размещен: {signData.modelName}, Сторона: {signData.sidePosition}");

                // Добавляем объект в список размещенных объектов
                FindObjectOfType<SceneBuilder>().AddPlacedSignObject(sign, signData); // Метод для добавления знаков

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

    public void ChangeColorToOriginal()
    {
        image.color = originalColor; // Меняем цвет на оригинальный
    }

    public void ChangeColorToHighlight()
    {
        image.color = highlightColor; // Меняем цвет на желтый
    }
}
