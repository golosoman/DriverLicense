using UnityEngine;

public class TriggerCarSpawnZone : MonoBehaviour
{
    public AllowedDirectionEnumType[] allowedDirections; // Разрешенные направления движения
    public int laneNumber = 1; // Номер полосы
    public SideDirectionEnumType sidePosition; // Сторона света (например, North, South, East, West)
    public GameObject currentCar; // Текущий автомобиль на точке спавна
}

