using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceneBuilder : MonoBehaviour
{
    public TMP_Dropdown intersectionDropdown; // Дропдаун для выбора перекрестка
    public GameObject dropdownPrefab; // Префаб для DirectionSelector (TMP_Dropdown)
    public GameObject[] intersections; // Массив префабов перекрестков
    public Transform mainArea; // Точка, где будет отображаться перекресток

    // Словарь для хранения автомобилей и их данных
    private Dictionary<GameObject, PlacedObjectData> carDictionary = new Dictionary<GameObject, PlacedObjectData>();

    // Словарь для хранения знаков и их данных
    private Dictionary<GameObject, PlacedSignData> signDictionary = new Dictionary<GameObject, PlacedSignData>();

    // Словарь для хранения светофоров и их данных
    private Dictionary<GameObject, PlacedTrafficLightData> trafficLightDictionary = new Dictionary<GameObject, PlacedTrafficLightData>();

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
        // Добавляем в словарь
        if (!carDictionary.ContainsKey(car))
        {
            carDictionary[car] = placeObjectData;

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

            Debug.Log($"Автомобиль {placeObjectData.modelName} добавлен в словарь.");
        }
        else
        {
            Debug.LogWarning($"Автомобиль {car.name} уже существует в словаре.");
        }
    }

    public void RemovePlacedCarObject(GameObject car)
    {
        // Удаляем объект из словаря
        if (carDictionary.TryGetValue(car, out PlacedObjectData dataToRemove))
        {
            carDictionary.Remove(car);
            Debug.Log($"Автомобиль {dataToRemove.modelName} удален из словаря размещенных объектов.");
        }
        else
        {
            Debug.LogWarning($"Автомобиль {car.name} не найден в словаре.");
        }
    }

    public void AddPlacedSignObject(GameObject sign, PlacedSignData signData)
    {
        // Добавляем в словарь
        if (!signDictionary.ContainsKey(sign))
        {
            signDictionary[sign] = signData;
            Debug.Log($"Знак {signData.modelName} добавлен в словарь.");
        }
        else
        {
            Debug.LogWarning($"Знак {sign.name} уже существует в словаре.");
        }
    }

    public void RemovePlacedSignObject(GameObject sign)
    {
        // Удаляем объект из словаря
        if (signDictionary.TryGetValue(sign, out PlacedSignData dataToRemove))
        {
            signDictionary.Remove(sign);
            Debug.Log($"Знак {dataToRemove.modelName} удален из словаря размещенных объектов.");
        }
        else
        {
            Debug.LogWarning($"Знак {sign.name} не найден в словаре.");
        }
    }

    public void AddPlacedTrafficLightObject(GameObject trafficLight, PlacedTrafficLightData trafficLightData)
    {
        // Добавляем в словарь
        if (!trafficLightDictionary.ContainsKey(trafficLight))
        {
            trafficLightDictionary[trafficLight] = trafficLightData;
            Debug.Log($"Светофор {trafficLightData.modelName} добавлен в словарь.");
        }
        else
        {
            Debug.LogWarning($"Светофор {trafficLight.name} уже существует в словаре.");
        }
    }

    public void RemovePlacedTrafficLightObject(GameObject trafficLight)
    {
        // Удаляем объект из словаря
        if (trafficLightDictionary.TryGetValue(trafficLight, out PlacedTrafficLightData dataToRemove))
        {
            trafficLightDictionary.Remove(trafficLight);
            Debug.Log($"Светофор {dataToRemove.modelName} удален из словаря размещенных объектов.");
        }
        else
        {
            Debug.LogWarning($"Светофор {trafficLight.name} не найден в словаре.");
        }
    }

    public List<PlacedSignData> GetSigns()
    {
        return new List<PlacedSignData>(signDictionary.Values);
    }

    public List<PlacedTrafficLightData> GetTrafficLights()
    {
        return new List<PlacedTrafficLightData>(trafficLightDictionary.Values);
    }

    public List<PlacedObjectData> GetTrafficParticipants()
    {
        return new List<PlacedObjectData>(carDictionary.Values);
    }

    public string GetIntersectionName()
    {
        return currentIntersection.name;
    }
}
