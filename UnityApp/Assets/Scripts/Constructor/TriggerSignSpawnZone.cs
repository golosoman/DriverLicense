using UnityEngine;

public class TriggerSignSpawnZone : MonoBehaviour
{
    public SideDirectionEnumType sidePosition; // Сторона света (например, North, South, East, West)
    public GameObject currentSign; // Текущий знак на точке спавна

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, если объект, вошедший в триггер, имеет тег "Car"
        if (other.CompareTag("SignImg") && currentSign == null)
        {
            other.gameObject.GetComponent<DraggableSign>().ChangeColorToOriginal();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Проверяем, если объект, вышедший из триггера, имеет тег "Car"
        if (other.CompareTag("SignImg") && currentSign == null)
        {
            other.gameObject.GetComponent<DraggableSign>().ChangeColorToHighlight();
        }
    }
}
