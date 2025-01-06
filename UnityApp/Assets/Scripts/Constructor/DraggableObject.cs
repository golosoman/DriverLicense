using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableObject : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvasGroup.alpha;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Здесь нужно проверить, попадает ли объект на точку спавна
        // Если да, то разместить его, иначе вернуть на исходное место
    }
}
