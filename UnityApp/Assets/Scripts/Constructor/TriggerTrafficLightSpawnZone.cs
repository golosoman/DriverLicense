using UnityEngine;

public class TriggerTrafficLightZone : MonoBehaviour
{
    public SideDirectionEnumType sidePosition; // Сторона света (например, North, South, East, West)
    public GameObject currentTrafficLight; // Текущий светофор на точке спавна
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, если объект, вошедший в триггер, имеет тег "Car"
        if (other.CompareTag("TrafficLightImg") && currentTrafficLight == null)
        {
            other.gameObject.GetComponent<DraggableTrafficLight>().ChangeColorToOriginal();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Проверяем, если объект, вышедший из триггера, имеет тег "Car"
        if (other.CompareTag("TrafficLightImg") && currentTrafficLight == null)
        {
            other.gameObject.GetComponent<DraggableTrafficLight>().ChangeColorToHighlight();
        }
    }
}
