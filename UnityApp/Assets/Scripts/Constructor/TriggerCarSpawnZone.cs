using UnityEngine;

public class TriggerCarSpawnZone : MonoBehaviour
{
    public AllowedDirectionEnumType[] allowedDirections; // Разрешенные направления движения
    public int laneNumber = 1; // Номер полосы
    public SideDirectionEnumType sidePosition; // Сторона света (например, North, South, East, West)

    public delegate void UIInteractionEventHandler(GameObject uiElement);
    public event UIInteractionEventHandler OnEnterZone;
    public event UIInteractionEventHandler OnExitZone;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DraggableCar")) // Убедитесь, что у вашего DraggableCar установлен тег
        {
            Debug.Log("Вы в зоне!");
            OnEnterZone?.Invoke(other.gameObject); // Вызываем событие при входе в зону
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("DraggableCar")) // Убедитесь, что у вашего DraggableCar установлен тег
        {
            Debug.Log("Вы снаружи зоны!");
            OnExitZone?.Invoke(other.gameObject); // Вызываем событие при выходе из зоны
        }
    }
}
