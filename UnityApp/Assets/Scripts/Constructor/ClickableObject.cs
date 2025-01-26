using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableObject : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        // Проверяем, нажата ли правая кнопка мыши
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            // Удаляем объект
            Destroy(gameObject);
            Debug.Log("Объект удален");
        }
    }



    private void OnDestroy()
    {
        // Удаляем объект из списка размещенных автомобилей
        FindObjectOfType<SceneBuilder>().RemovePlacedCarObject(gameObject);
    }
}
