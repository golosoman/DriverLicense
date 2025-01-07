using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableObject : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 originalPosition; // Для хранения исходной позиции

    // Ссылка на префаб машины (можно назначить в инспекторе)
    public GameObject carPrefab;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = rectTransform.anchoredPosition; // Сохраняем исходную позицию
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvasGroup.alpha;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Проверяем, попадает ли объект на точку спавна
        RaycastHit2D hit = Physics2D.Raycast(rectTransform.position, Vector2.zero);
        if (hit.collider != null)
        {
            // Проверяем, имеет ли коллайдер тег carSpawnPoint
            if (hit.collider.CompareTag("carSpawnPoint"))
            {
                // Инстанцируем префаб машины на точке спавна
                Instantiate(carPrefab, hit.collider.transform.position, Quaternion.identity);
                // Здесь можно добавить логику для удаления или скрытия перетаскиваемого объекта
                Destroy(gameObject); // Уничтожаем объект, если он был успешно размещен
            }
            else
            {
                // Если не попадает на точку спавна, возвращаем на исходную позицию
                rectTransform.anchoredPosition = originalPosition;
            }
        }
        else
        {
            // Если не попадает на точку спавна, возвращаем на исходную позицию
            rectTransform.anchoredPosition = originalPosition;
        }
    }
}
