using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json; // Убедитесь, что у вас установлен Newtonsoft.Json через PackageManager

public class DatabaseLoader : MonoBehaviour
{
    private const string baseUrl = "http://localhost:5000"; // Замените на ваш URL
    private const string prefabPath = "Prefabs";

    void Start()
    {
        StartCoroutine(LoadTicketData());
    }

    IEnumerator LoadTicketData()
    {
        string ticketId = "1"; // Замените на нужный ID билета
        string url = $"{baseUrl}/tickets/{ticketId}";

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonData = request.downloadHandler.text;
            TicketData ticketData = JsonConvert.DeserializeObject<TicketData>(jsonData);

            Debug.Log("Ticket Data Loaded: " + ticketData.type);

            // Создание объектов на сцене
            CreateIntersection(ticketData.type);
            CreateCars(ticketData.carsArr);
            CreateSigns(ticketData.signsArr);
            CreateTrafficLights(ticketData.trafficLightsArr);
        }
        else
        {
            Debug.LogError("Failed to load ticket data: " + request.error);
        }
    }

    void CreateIntersection(string intersection){
        // GameObject intersectionPrefab = Resources.Load<GameObject>($"{prefabPath}/intersections/{intersection}");
        // Предполагается, что префабы находятся в папке Resources
        GameObject intersectionPrefab = Resources.Load<GameObject>($"{prefabPath}/intersections/{intersection}");
        Instantiate(intersectionPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    void CreateCars(CarData[] cars)
    {
        GameObject[] carSpawnPoints = GameObject.FindGameObjectsWithTag("CarSpawn");

        foreach (CarData carData in cars)
        {
            GameObject carPrefab = Resources.Load<GameObject>($"{prefabPath}/cars/{carData.modelName}"); // Предполагается, что префабы находятся в папке Resources

            foreach (GameObject spawnPoint in carSpawnPoints)
            {
                if (spawnPoint.name == "CarSpawnLeft" && carData.position == "left")
                {
                    Instantiate(carPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                    // Специфическая настройка для машины слева
                }
                if (spawnPoint.name == "CarSpawnRight" && carData.position == "right")
                {
                    Instantiate(carPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                    // Специфическая настройка для машины слева
                }
                if (spawnPoint.name == "CarSpawnBottom" && carData.position == "bottom")
                {
                    Instantiate(carPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                    // Специфическая настройка для машины слева
                }
                if (spawnPoint.name == "CarSpawnTop" && carData.position == "top")
                {
                    Instantiate(carPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                    // Специфическая настройка для машины слева
                }
            }
            // GameObject car = Instantiate(carPrefab, carData.position, Quaternion.identity);
            // Дополнительные настройки для машины
        }
    }

    void CreateSigns(SignData[] signs)
    {
        GameObject[] signSpawnPoints = GameObject.FindGameObjectsWithTag("SignSpawn");

        foreach (SignData signData in signs)
        {
            GameObject signPrefab = Resources.Load<GameObject>($"{prefabPath}/signs/{signData.modelName}"); // Предполагается, что префабы находятся в папке Resources
            foreach (GameObject spawnPoint in signSpawnPoints)
            {
                if (spawnPoint.name == "SignSpawnLeft" && signData.position == "left")
                {
                    Instantiate(signPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                    // Специфическая настройка для машины слева
                }
                if (spawnPoint.name == "SignSpawnRight" && signData.position == "right")
                {
                    Instantiate(signPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                    // Специфическая настройка для машины слева
                }
                if (spawnPoint.name == "SignSpawnBottom" && signData.position == "bottom")
                {
                    Instantiate(signPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                    // Специфическая настройка для машины слева
                }
                if (spawnPoint.name == "SignSpawnTop" && signData.position == "top")
                {
                    Instantiate(signPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                    // Специфическая настройка для машины слева
                }
                // GameObject sign = Instantiate(signPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                // Дополнительные настройки для знака, если необходимо
            }
          
            // GameObject sign = Instantiate(signPrefab, signData.position, Quaternion.identity);
            // Дополнительные настройки для знака
        }
       
        
    }

    void CreateTrafficLights(TrafficLightData[] trafficLights)
    {
        GameObject[] trafficLightSpawnPoints = GameObject.FindGameObjectsWithTag("TrafficLightSpawn");
        foreach (TrafficLightData trafficLightData in trafficLights)
        {
            GameObject trafficLightPrefab = Resources.Load<GameObject>($"{prefabPath}/trafficLights/{trafficLightData.modelName}"); // Предполагается, что префабы находятся в папке Resources
            foreach (GameObject spawnPoint in trafficLightSpawnPoints)
            {
                if (spawnPoint.name == "TrafficLightSpawnLeft" && trafficLightData.position == "left")
                {
                    Instantiate(trafficLightPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                    // Специфическая настройка для машины слева
                }
                if (spawnPoint.name == "TrafficLightSpawnRight" && trafficLightData.position == "right")
                {
                    Instantiate(trafficLightPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                    // Специфическая настройка для машины слева
                }
                if (spawnPoint.name == "TrafficLightSpawnBottom" && trafficLightData.position == "bottom")
                {
                    Instantiate(trafficLightPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                    // Специфическая настройка для машины слева
                }
                if (spawnPoint.name == "TrafficLightSpawnTop" && trafficLightData.position == "top")
                {
                    Instantiate(trafficLightPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                    // Специфическая настройка для машины слева
                }
                // GameObject sign = Instantiate(signPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                // Дополнительные настройки для знака, если необходимо
            }
           
            // GameObject trafficLight = Instantiate(trafficLightPrefab, trafficLightData.position, Quaternion.identity);
            // Дополнительные настройки для светофора
        }
    }

    // IEnumerator LoadTicketData()
    // {
    //     string ticketId = "1"; // Замените на нужный ID билета
    //     string url = $"{BaseUrl}/tickets/{ticketId}";

    //     UnityWebRequest request = UnityWebRequest.Get(url);
    //     yield return request.SendWebRequest();

    //     if (request.result == UnityWebRequest.Result.Success)
    //     {
    //         string jsonData = request.downloadHandler.text;
    //         TicketData ticketData = JsonConvert.DeserializeObject<TicketData>(jsonData);

    //         // Теперь у вас есть данные билета, машины, знаки и светофоры
    //         Debug.Log("Ticket Data Loaded: " + ticketData.type);
    //         Debug.Log("Ticket Data Loaded: " + ticketData.cars_arr[0].sprite);
    //         // Обработка данных, например, создание объектов на сцене
    //     }
    //     else
    //     {
    //         Debug.LogError("Failed to load ticket data: " + request.error);
    //     }
    // }

}

// Пример класса для данных билета
public class TicketData
{
    public int id;
    public string type;
    public string question;
    public string correctAnswer;
    public CarData[] carsArr;
    public SignData[] signsArr;
    public TrafficLightData[] trafficLightsArr;
    // Добавьте другие необходимые поля
}

public class CarData
{
    public int id;
    public string modelName;
    public string position;
    public int speed;
    public string movementDirection;
    // Добавьте другие необходимые поля
}

public class SignData
{
    public int id;
    public string modelName;
    public string position;
}

public class TrafficLightData
{
    public int id;
    public string modelName;
    public string position;
    public string cycle;
    // Добавьте другие необходимые поля
}