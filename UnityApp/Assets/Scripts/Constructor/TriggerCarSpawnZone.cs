using UnityEngine;

public class TriggerCarSpawnZone : MonoBehaviour
{
    public AllowedDirectionEnumType[] allowedDirections; // Разрешенные направления движения
    public int laneNumber = 1; // Номер полосы
    public SideDirectionEnumType sidePosition; // Сторона света (например, North, South, East, West)

    public delegate void CarZoneEventHandler(GameObject car);
    public event CarZoneEventHandler OnCarEnterZone;
    public event CarZoneEventHandler OnCarExitZone;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(TagObjectNamesTypes.CAR_IMG))
        {
            Debug.Log("Вы в зоне!");
            OnCarEnterZone?.Invoke(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(TagObjectNamesTypes.CAR_IMG))
        {
            Debug.Log("Вы снаружи зоны!");
            OnCarExitZone?.Invoke(other.gameObject);
        }
    }
}
