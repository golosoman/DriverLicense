using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceneBuilder : MonoBehaviour
{
    public TMP_Dropdown intersectionDropdown; // Дропдаун для выбора перекрестка
    public GameObject dropdownPrefab; // Префаб для DirectionSelector (TMP_Dropdown)
    public GameObject[] intersections; // Массив префабов перекрестков
    public Transform mainArea; // Точка, где будет отображаться перекресток
    public List<GameObject> carObjects = new List<GameObject>(); // Список размещенных объектов
    public static List<PlacedObjectData> placedCars = new List<PlacedObjectData>();

    private GameObject currentIntersection;

    void Start()
    {
        intersectionDropdown.onValueChanged.AddListener(OnIntersectionChanged);
        OnIntersectionChanged(0); // Инициализация с первым элементом
    }

    public void OnIntersectionChanged(int index)
    {
        if (currentIntersection != null)
        {
            Destroy(currentIntersection);
        }

        // Инстанцируем префаб
        currentIntersection = Instantiate(intersections[index], mainArea.position, Quaternion.identity, mainArea);

        // Устанавливаем масштаб перекрестка
        float scaleValue = 28f;
        currentIntersection.transform.localScale = new Vector3(scaleValue, scaleValue, 1f);
    }


    public void AddPlacedCarObject(GameObject car, PlacedObjectData placeObjectData)
    {
        // Добавляем данные в список
        placedCars.Add(placeObjectData);

        // Добавляем DirectionSelector к автомобилю
        DirectionSelector directionSelector = car.AddComponent<DirectionSelector>();
        directionSelector.dropdownPrefab = dropdownPrefab;

        // Получаем разрешенные направления из точки спавна
        TriggerCarSpawnZone spawnZone = placeObjectData.srcBySpawnPoint;
        string[] allowedDirections = new string[spawnZone.allowedDirections.Length];

        // Преобразуем enum в строку
        for (int i = 0; i < spawnZone.allowedDirections.Length; i++)
        {
            allowedDirections[i] = spawnZone.allowedDirections[i].ToString(); // Преобразование enum в строку
        }

        // Устанавливаем доступные направления в DirectionSelector
        directionSelector.SetAvailableDirections(allowedDirections);

        Debug.Log(dropdownPrefab);
    }

    public void RemovePlacedCarObject(GameObject car)
    {
        // Находим объект в списке и удаляем его
        PlacedObjectData dataToRemove = placedCars.Find(data => data.modelName == car.name);
        if (dataToRemove != null)
        {
            placedCars.Remove(dataToRemove);
            Debug.Log($"Автомобиль {dataToRemove.modelName} удален из списка размещенных объектов.");
        }
    }

    public void AddPlacedSignObject(GameObject obj)
    {
        // Добавьте логику для хранения знаков, если это необходимо
        Debug.Log("Знак добавлен в список: " + obj.name);
    }
}
