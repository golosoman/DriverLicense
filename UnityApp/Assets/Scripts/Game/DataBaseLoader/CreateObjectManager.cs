using System.Collections.Generic;
using UnityEngine;

public class CreateObjectManager : ScriptableObject
{
    private Dictionary<string, GameObject> roadUserSpawnPoints;
    private Dictionary<string, GameObject> signSpawnPoints;
    private Dictionary<string, GameObject> trafficLightSpawnPoints;
    private IntersectionRoutesManager intersectionManager;
    RoadManager roadUserManager;
    // private RuleChecker ruleChecker;

    // private TicketData ticketData;

    public void GetNewSpawnPoint()
    {
        roadUserSpawnPoints?.Clear();
        signSpawnPoints?.Clear();
        trafficLightSpawnPoints?.Clear();
    }

    public void ProcessTicketData(Question ticketData)
    {

        // this.ticketData = ticketData;
        GetNewSpawnPoint();
        CreateIntersection(ticketData.intersectionType);
        InitRuleManager(ticketData.intersectionType, ticketData.trafficLights, ticketData.signs, roadUserSpawnPoints);
        CreateEntities(ticketData.signs, signSpawnPoints, FilePath.PATH_PREFAB_SIGNS, CreateSign);
        CreateEntities(ticketData.trafficLights, trafficLightSpawnPoints, FilePath.PATH_PREFAB_TRAFFIC_LIGHTS, CreateTrafficLight);
        CreateEntities(ticketData.trafficParticipants, roadUserSpawnPoints, FilePath.PATH_PREFAB_ROAD_USERS, CreateRoadUser);
    }

    void InitRuleManager(string intersection, TrafficLight[] trafficLightDatas, Sign[] signDatas, Dictionary<string, GameObject> roadUserSpawnPoints)
    {
        roadUserManager = FindObjectOfType<RoadManager>();
        roadUserManager.Initialize(intersection, trafficLightDatas, signDatas, roadUserSpawnPoints);
    }

    private GameObject intersectionContainer;

    void CreateIntersection(string intersection)
    {
        // Удаляем старые объекты, если контейнер уже существует
        if (intersectionContainer != null)
        {
            // Destroy(intersectionContainer);
            intersectionContainer.SetActive(false);
        }

        // Создаем новый контейнер для объектов перекрестка
        intersectionContainer = new GameObject("IntersectionContainer");

        GameObject intersectionPrefab = PrefabManager.GetPrefab($"{FilePath.PATH_PREFAB_INTERSECTIONS}{intersection}");
        GameObject intersectionInstance = Instantiate(intersectionPrefab, Vector3.zero, Quaternion.identity, intersectionContainer.transform);

        intersectionManager = intersectionInstance.GetComponent<IntersectionRoutesManager>();

        if (intersectionManager == null)
        {
            Debug.LogError("IntersectionManager component is missing on the intersection prefab.");
        }

        InitializeDictionaries();
    }

    // void CreateIntersection(string intersection)
    // {
    //     GameObject intersectionPrefab = PrefabManager.GetPrefab($"{FilePath.PATH_PREFAB_INTERSECTIONS}{intersection}");
    //     GameObject intersectionInstance = Instantiate(intersectionPrefab, Vector3.zero, Quaternion.identity);

    //     intersectionManager = intersectionInstance.GetComponent<IntersectionRoutesManager>();

    //     if (intersectionManager == null)
    //     {
    //         Debug.LogError("IntersectionManager component is missing on the intersection prefab.");
    //     }

    //     InitializeDictionaries();
    // }

    void CreateEntities<T>(T[] entities, Dictionary<string, GameObject> spawnPoints, string prefabPath, System.Action<T, GameObject> createAction)
    {
        foreach (var entity in entities)
        {
            string modelName = GetModelName(entity);
            GameObject prefab = PrefabManager.GetPrefab($"{prefabPath}{modelName}");
            if (prefab != null)
            {
                string sidePosition = GetSidePosition(entity);
                if (spawnPoints.TryGetValue(sidePosition, out GameObject spawnPoint))
                {
                    createAction(entity, spawnPoint);
                }
                else
                {
                    Debug.LogError($"The spawn point for the position {sidePosition} was not found.");
                }
            }
            else
            {
                Debug.LogError($"The prefab for the model {modelName} was not found.");
            }
        }
    }

    void CreateRoadUser(TrafficParticipant roadUserData, GameObject spawnPoint)
    {
        // Debug.Log($"{FilePath.PATH_PREFAB_ROAD_USERS}{roadUserData.ModelName}");
        GameObject roadUserInstance = Instantiate(PrefabManager.GetPrefab($"{FilePath.PATH_PREFAB_ROAD_USERS}{roadUserData.modelName}"), spawnPoint.transform.position, spawnPoint.transform.rotation, intersectionContainer.transform);
        roadUserInstance.name = $"{roadUserData.modelName}_{roadUserData.sidePosition}_{roadUserData.numberPosition}";
        roadUserInstance.tag = GetTagFromTypeParticipant(roadUserData.participantType);
        // Устанавливаем позицию z, чтобы roadUser  не перекрывался другими объектами
        Vector3 newPosition = roadUserInstance.transform.position;
        newPosition.z = -1f; // Устанавливаем z на 1, чтобы объект был выше других
        roadUserInstance.transform.position = newPosition;

        IntersectionRoutesManager.Direction roadUserDirection = GetDirectionFromData(roadUserData.sidePosition);
        Transform[] route = intersectionManager.GetRoute(roadUserDirection, roadUserData.direction);

        RoadUserMovement roadUserMovement = roadUserInstance.GetComponent<RoadUserMovement>();
        roadUserMovement.Route = route;
        roadUserMovement.RUD = roadUserData;
        GlobalManager.IncrementCarCount(); // Увеличиваем счетчик машин

        // RoadRuleChecker roadRuleChecker = roadUserInstance.GetComponent<RoadRuleChecker>();
        // roadRuleChecker.Initialize(roadUserManager);ы

        // roadUserManager.AddRoadUser(roadUserData, roadUserInstance);
    }

    // private RuleChecker CreateRuleChecker(RoadUserData roadUserData)
    // {
    //     switch (roadUserData.TypeParticipant)
    //     {
    //         case "car":
    //             return new CarRuleChecker();
    //         // case "human":
    //         //     return new HumanRuleChecker();
    //         // case "tram":
    //         //     return new TramRuleChecker();
    //         default:
    //             Debug.LogError("Unknown participant type: " + roadUserData.TypeParticipant);
    //             return null;
    //     }
    // }

    void CreateSign(Sign signData, GameObject spawnPoint)
    {
        Instantiate(PrefabManager.GetPrefab($"{FilePath.PATH_PREFAB_SIGNS}{signData.modelName}"), spawnPoint.transform.position, spawnPoint.transform.rotation, intersectionContainer.transform);
    }

    void CreateTrafficLight(TrafficLight trafficLightData, GameObject spawnPoint)
    {
        Instantiate(PrefabManager.GetPrefab($"{FilePath.PATH_PREFAB_TRAFFIC_LIGHTS}{trafficLightData.modelName}"), spawnPoint.transform.position, spawnPoint.transform.rotation, intersectionContainer.transform);
    }

    string GetModelName<T>(T entity)
    {
        return entity switch
        {
            TrafficParticipant roadUserData => roadUserData.modelName,
            Sign signData => signData.modelName,
            TrafficLight trafficLightData => trafficLightData.modelName,
            _ => throw new System.ArgumentException("Unknown entity type")
        };
    }

    string GetSidePosition<T>(T entity)
    {
        return entity switch
        {
            TrafficParticipant roadUserData => roadUserData.sidePosition,
            Sign signData => signData.sidePosition,
            TrafficLight trafficLightData => trafficLightData.sidePosition,
            _ => throw new System.ArgumentException("Unknown entity type")
        };
    }

    string GetTagFromTypeParticipant(string typeParticipant)
    {
        // Выдача тегов по типу участника движения
        return typeParticipant switch
        {
            RoadUserTypes.CAR => TagObjectNamesTypes.CAR,
            RoadUserTypes.HUMAN => TagObjectNamesTypes.HUMAN,
            RoadUserTypes.TRAM => TagObjectNamesTypes.TRAM,
            _ => TagObjectNamesTypes.UNTAGGED
        };
    }

    IntersectionRoutesManager.Direction GetDirectionFromData(string position)
    {
        return position switch
        {
            SideDirectionTypes.WEST => IntersectionRoutesManager.Direction.WEST,
            SideDirectionTypes.EAST => IntersectionRoutesManager.Direction.EAST,
            SideDirectionTypes.NORTH => IntersectionRoutesManager.Direction.NORTH,
            SideDirectionTypes.SOUTH => IntersectionRoutesManager.Direction.SOUTH,
            _ => IntersectionRoutesManager.Direction.NORTH
        };
    }

    private void InitializeDictionaries()
    {
        roadUserSpawnPoints = new Dictionary<string, GameObject>
        {
            { SideDirectionTypes.WEST, GameObject.Find(CarSpawnPoint.CAR_SPAWN_WEST) },
            { SideDirectionTypes.EAST, GameObject.Find(CarSpawnPoint.CAR_SPAWN_EAST) },
            { SideDirectionTypes.NORTH, GameObject.Find(CarSpawnPoint.CAR_SPAWN_NORTH) },
            { SideDirectionTypes.SOUTH, GameObject.Find(CarSpawnPoint.CAR_SPAWN_SOUTH) }
        };

        signSpawnPoints = new Dictionary<string, GameObject>
        {
            { SideDirectionTypes.WEST, GameObject.Find(SignSpawnPoint.SIGN_SPAWN_WEST) },
            { SideDirectionTypes.EAST, GameObject.Find(SignSpawnPoint.SIGN_SPAWN_EAST) },
            { SideDirectionTypes.NORTH, GameObject.Find(SignSpawnPoint.SIGN_SPAWN_NORTH) },
            { SideDirectionTypes.SOUTH, GameObject.Find(SignSpawnPoint.SIGN_SPAWN_SOUTH) }
        };

        trafficLightSpawnPoints = new Dictionary<string, GameObject>
        {
            { SideDirectionTypes.WEST, GameObject.Find(TrafficLightSpawnPoint.TRAFFIC_LIGHT_SPAWN_WEST) },
            { SideDirectionTypes.EAST, GameObject.Find(TrafficLightSpawnPoint.TRAFFIC_LIGHT_SPAWN_EAST) },
            { SideDirectionTypes.NORTH, GameObject.Find(TrafficLightSpawnPoint.TRAFFIC_LIGHT_SPAWN_NORTH) },
            { SideDirectionTypes.SOUTH, GameObject.Find(TrafficLightSpawnPoint.TRAFFIC_LIGHT_SPAWN_SOUTH) }
        };
    }
}
