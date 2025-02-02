using UnityEngine;

public class TriggerCarSpawnZone : MonoBehaviour
{
    public AllowedDirectionEnumType[] allowedDirections; // Разрешенные направления движения
    public int laneNumber = 1; // Номер полосы
    public SideDirectionEnumType sidePosition; // Сторона света (например, North, South, East, West)
    public GameObject currentCar; // Текущий автомобиль на точке спавна

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, если объект, вошедший в триггер, имеет тег "Car"
        if (other.CompareTag("CarImg") && currentCar == null)
        {
            other.gameObject.GetComponent<DraggableCar>().ChangeColorToOriginal();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Проверяем, если объект, вышедший из триггера, имеет тег "Car"
        if (other.CompareTag("CarImg") && currentCar == null)
        {
            other.gameObject.GetComponent<DraggableCar>().ChangeColorToHighlight();
        }
    }
}

