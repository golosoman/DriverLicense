using UnityEngine;

[System.Serializable]
public class SpawnPointData
{
    public string[] allowedDirections; // Разрешенные направления движения
    public int laneNumber; // Номер полосы
    public string sidePosition; // Сторона света
}

[System.Serializable]
public class PlacedObjectData
{
    public string participantType; // Тип объекта (например, "Car", "Sign", "TrafficLight")
    public string modelName; // Позиция объекта
    public string direction; // Вращение объекта
    public int numberPosition; // Дополнительная информация, если необходимо
    public string sidePosition;
    public TriggerCarSpawnZone srcBySpawnPoint;
}