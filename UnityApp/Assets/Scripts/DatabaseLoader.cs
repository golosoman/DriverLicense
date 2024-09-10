using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Unity.VisualScripting;

public class DatabaseLoader : MonoBehaviour
{
    private const string baseUrl = "http://localhost:5000";
    private Dictionary<string, GameObject> prefabCache = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> roadUserSpawnPoints = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> signSpawnPoints = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> trafficLightSpawnPoints = new Dictionary<string, GameObject>();
    private IntersectionManager intersectionManager;

    void Start()
    {
        StartCoroutine(LoadTicketData());
    }

    IEnumerator LoadTicketData()
    {
        string ticketId = "1";
        string url = $"{baseUrl}/tickets/{ticketId}";

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonData = request.downloadHandler.text;
            try
            {
                TicketData ticketData = JsonConvert.DeserializeObject<TicketData>(jsonData);
                Debug.Log("Ticket Data Loaded: " + ticketData.typeIntersection);
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

    void ProcessTicketData(TicketData ticketData)
    {
        CreateIntersection(ticketData.typeIntersection);
        CreateRoadUsers(ticketData.roadUsersArr);
        CreateSigns(ticketData.signsArr);
        CreateTrafficLights(ticketData.trafficLightsArr);
    }

    void CreateIntersection(string intersection)
    {
        GameObject intersectionPrefab = GetPrefab($"Prefabs/intersections/{intersection}");
        GameObject intersectionInstance = Instantiate(intersectionPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        intersectionManager = intersectionInstance.GetComponent<IntersectionManager>();

        if (intersectionManager == null)
        {
            Debug.LogError("IntersectionManager component is missing on the intersection prefab.");
        }

        DictInit();
    }

    void CreateRoadUsers(RoadUserData[] roadUsers)
    {
        if (intersectionManager == null)
        {
            Debug.LogError("IntersectionManager is not initialized. Please make sure to call CreateIntersection first.");
            return;
        }
        
        foreach (RoadUserData roadUserData in roadUsers)
        {
            GameObject roadUserPrefab = GetPrefab($"Prefabs/roadUsers/{roadUserData.modelName}");
            if (roadUserPrefab!= null)
            {
                GameObject spawnPoint;
                if (roadUserSpawnPoints.TryGetValue(roadUserData.sidePosition, out spawnPoint))
                {
                    GameObject roadUserInstance = Instantiate(roadUserPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                    
                    // Задаем направление и маршрут автомобиля
                    IntersectionManager.Direction roadUserDirection = GetDirectionFromData(roadUserData.sidePosition);
                    Transform[] route = intersectionManager.GetRoute(roadUserDirection, roadUserData.movementDirection);

                    // Добавляем компонент для движения и передаем маршрут
                    RoadUserMovement roadUserMovement = roadUserInstance.GetComponent<RoadUserMovement>();
                    roadUserMovement.SetRoute(route);
                }
                else
                {
                    Debug.LogError($"The spawn point for the position was not found {roadUserData.sidePosition}");
                }
            }
            else
            {
                Debug.LogError($"The prefab for the position was not found {roadUserData.modelName}");
            }
        }
    }

    // Преобразование позиции автомобиля в направление
    IntersectionManager.Direction GetDirectionFromData(string position)
    {
        switch (position)
        {
            case "west":
                return IntersectionManager.Direction.West;
            case "east":
                return IntersectionManager.Direction.East;
            case "north":
                return IntersectionManager.Direction.North;
            case "south":
                return IntersectionManager.Direction.South;
            default:
                return IntersectionManager.Direction.North; // Дефолтное направление
        }
    }

    void CreateSigns(SignData[] signs)
    {
        foreach (SignData signData in signs)
        {
            GameObject signPrefab = GetPrefab($"Prefabs/signs/{signData.modelName}");
            if (signPrefab!= null)
            {
                GameObject spawnPoint;
                if (signSpawnPoints.TryGetValue(signData.sidePosition, out spawnPoint))
                {
                    Instantiate(signPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                }
                else
                {
                    Debug.LogError($"The spawn point for the position was not found {signData.sidePosition}");
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
           GameObject trafficLightPrefab = GetPrefab($"Prefabs/trafficLights/{trafficLightData.modelName}");
            if (trafficLightPrefab!= null)
            {
                GameObject spawnPoint;
                if (trafficLightSpawnPoints.TryGetValue(trafficLightData.sidePosition, out spawnPoint))
                {
                    Instantiate(trafficLightPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                }
                else
                {
                    Debug.LogError("The spawn point for the position was not found {trafficLightData.position}"); } } else { Debug.LogError("The prefab for the position was not found {trafficLightData.modelName}");
            }
        }
    }

    GameObject GetPrefab(string prefabPath)
    {
        if (prefabCache.ContainsKey(prefabPath))
        {
            return prefabCache[prefabPath];
        }
        else
        {
            GameObject prefab = Resources.Load<GameObject>($"{prefabPath}");
            if (prefab!= null)
            {
                prefabCache.Add(prefabPath, prefab);
                return prefab;
            }
            else
            {
                Debug.LogError($"The prefab {prefabPath} was not found");
                return null;
            }
        }
    }

    private void DictInit(){
        roadUserSpawnPoints.Add("west", GameObject.Find("CarSpawnLeft"));
        roadUserSpawnPoints.Add("east", GameObject.Find("CarSpawnRight"));
        roadUserSpawnPoints.Add("north", GameObject.Find("CarSpawnTop"));
        roadUserSpawnPoints.Add("south", GameObject.Find("CarSpawnBottom"));

        signSpawnPoints.Add("west", GameObject.Find("SignSpawnLeft"));
        signSpawnPoints.Add("east", GameObject.Find("SignSpawnRight"));
        signSpawnPoints.Add("north", GameObject.Find("SignSpawnTop"));
        signSpawnPoints.Add("south", GameObject.Find("SignSpawnBottom"));

        trafficLightSpawnPoints.Add("west", GameObject.Find("TrafficLightSpawnLeft"));
        trafficLightSpawnPoints.Add("east", GameObject.Find("TrafficLightSpawnRight"));
        trafficLightSpawnPoints.Add("north", GameObject.Find("TrafficLightSpawnTop"));
        trafficLightSpawnPoints.Add("south", GameObject.Find("TrafficLightSpawnBottom"));
    }

    public class TicketData
    {
        public int id;
        public string typeIntersection;
        public string title;
        public string question;
        public string correctAnswer;
        public RoadUserData[] roadUsersArr;
        public SignData[] signsArr;
        public TrafficLightData[] trafficLightsArr;
    }

    public class RoadUserData
    {
        public int id;
        public string typeParticipant;
        public string modelName;
        public string sidePosition;
        public string numberPosition;
        public string movementDirection;
    }

    public class SignData
    {
        public int id;
        public string modelName;
        public string sidePosition;
    }

    public class TrafficLightData
    {
        public int id;
        public string modelName;
        public string sidePosition;
    }
}

        

