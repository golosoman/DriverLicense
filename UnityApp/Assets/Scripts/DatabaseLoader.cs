using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json; // Убедитесь, что у вас установлен Newtonsoft.Json через PackageManager

public class DatabaseLoader : MonoBehaviour
{
    private const string baseUrl = "http://localhost:5000"; // Замените на ваш URL
    private const string prefabPath = "Prefabs";
    private Dictionary<string, GameObject> carSpawnPoints = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> signSpawnPoints = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> trafficLightSpawnPoints = new Dictionary<string, GameObject>();


    void Start()
    {
        StartCoroutine(LoadTicketData());
    }
    
    IEnumerator LoadTicketData()
    {
        string ticketId = "2"; // Замените на нужный ID билета
        string url = $"{baseUrl}/tickets/{ticketId}";

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonData = request.downloadHandler.text;
            try
            {
            TicketData ticketData = JsonConvert.DeserializeObject<TicketData>(jsonData);
            Debug.Log("Ticket Data Loaded: " + ticketData.type);
            ProcessTicketData(ticketData);
            }
            catch (JsonException ex)
            {
                Debug.LogError("Failed to parse JSON data: " + ex.Message);
            }
        }
        else
        {
            Debug.LogError("Failed to load ticket data: " + request.error);
        }
    }

    void DictInit(){
        carSpawnPoints.Add("left", GameObject.Find("CarSpawnLeft"));
        carSpawnPoints.Add("right", GameObject.Find("CarSpawnRight"));
        carSpawnPoints.Add("top", GameObject.Find("CarSpawnTop"));
        carSpawnPoints.Add("bottom", GameObject.Find("CarSpawnBottom"));

        signSpawnPoints.Add("left", GameObject.Find("SignSpawnLeft"));
        signSpawnPoints.Add("right", GameObject.Find("SignSpawnRight"));
        signSpawnPoints.Add("top", GameObject.Find("SignSpawnTop"));
        signSpawnPoints.Add("bottom", GameObject.Find("SignSpawnBottom"));

        trafficLightSpawnPoints.Add("left", GameObject.Find("TrafficLightSpawnLeft"));
        trafficLightSpawnPoints.Add("right", GameObject.Find("TrafficLightSpawnRight"));
        trafficLightSpawnPoints.Add("top", GameObject.Find("TrafficLightSpawnTop"));
        trafficLightSpawnPoints.Add("bottom", GameObject.Find("TrafficLightSpawnBottom"));
    }

    void ProcessTicketData(TicketData ticketData)
    {
        CreateIntersection(ticketData.type);
        CreateCars(ticketData.carsArr);
        CreateSigns(ticketData.signsArr);
        CreateTrafficLights(ticketData.trafficLightsArr);
    }

    void CreateIntersection(string intersection){
        GameObject intersectionPrefab = Resources.Load<GameObject>($"{prefabPath}/intersections/{intersection}");
        Instantiate(intersectionPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        DictInit();
    }

    void CreateCars(CarData[] cars)
    {
        foreach (CarData carData in cars)
        {
            GameObject carPrefab = Resources.Load<GameObject>($"{prefabPath}/cars/{carData.modelName}");
            if (carPrefab!= null)
            {
                GameObject spawnPoint;
                if (carSpawnPoints.TryGetValue(carData.position, out spawnPoint))
                {
                    Instantiate(carPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                }
                else
                {
                    Debug.LogError($"The spawn point for the position was not found {carData.position}");
                }
            }
            else
            {
                Debug.LogError($"The prefab for the position was not found {carData.modelName}");
            }
        }
    }

    void CreateSigns(SignData[] signs)
    {
        foreach (SignData signData in signs)
        {
            GameObject signPrefab = Resources.Load<GameObject>($"{prefabPath}/signs/{signData.modelName}");
            if (signPrefab!= null)
            {
                GameObject spawnPoint;
                if (signSpawnPoints.TryGetValue(signData.position, out spawnPoint))
                {
                    Instantiate(signPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                }
                else
                {
                    Debug.LogError($"The spawn point for the position was not found {signData.position}");
                }
            }
            else
            {
                Debug.LogError($"The prefab for the position was not found {signData.modelName}");
            }
        }
    }

    void CreateTrafficLights(TrafficLightData[] trafficLights)
    {
        foreach (TrafficLightData trafficLightData in trafficLights)
        {
            GameObject trafficLightPrefab = Resources.Load<GameObject>($"{prefabPath}/trafficLights/{trafficLightData.modelName}");
            if (trafficLightPrefab!= null)
            {
                GameObject spawnPoint;
                if (trafficLightSpawnPoints.TryGetValue(trafficLightData.position, out spawnPoint))
                {
                    Instantiate(trafficLightPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                }
                else
                {
                    Debug.LogError($"The spawn point for the position was not found {trafficLightData.position}");
                }
            }
            else
            {
                Debug.LogError($"The prefab for the position was not found {trafficLightData.modelName}");
            }
        }
    }
}

public class TicketData
{
    public int id;
    public string type;
    public string question;
    public string correctAnswer;
    public CarData[] carsArr;
    public SignData[] signsArr;
    public TrafficLightData[] trafficLightsArr;
}

public class CarData
{
    public int id;
    public string modelName;
    public string position;
    public int speed;
    public string movementDirection;
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
}