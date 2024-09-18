using System.Collections.Generic;
using UnityEngine;

public class CreateObjectManager : ScriptableObject
{
    private Dictionary<string, GameObject> roadUserSpawnPoints = new();
    private Dictionary<string, GameObject> signSpawnPoints = new();
    private Dictionary<string, GameObject> trafficLightSpawnPoints = new();
    private IntersectionRoutesManager intersectionManager;
    RoadUserManager roadUserManager;
    // private RuleChecker ruleChecker;

    // private TicketData ticketData;

    public void ProcessTicketData(TicketData ticketData)
    {
        // this.ticketData = ticketData;
        InitRuleManager(ticketData.TypeIntersection);
        CreateIntersection(ticketData.TypeIntersection);
        CreateEntities(ticketData.RoadUsersArr, roadUserSpawnPoints, FilePath.PATH_PREFAB_ROAD_USERS, CreateRoadUser);
        CreateEntities(ticketData.SignsArr, signSpawnPoints, FilePath.PATH_PREFAB_SIGNS, CreateSign);
        CreateEntities(ticketData.TrafficLightsArr, trafficLightSpawnPoints, FilePath.PATH_PREFAB_TRAFFIC_LIGHTS, CreateTrafficLight);
    }

    void InitRuleManager(string intersection){
        roadUserManager = FindObjectOfType<RoadUserManager>();
        roadUserManager.Initialize(intersection);
    }

    void CreateIntersection(string intersection)
    {
        GameObject intersectionPrefab = PrefabManager.GetPrefab($"{FilePath.PATH_PREFAB_INTERSECTIONS}{intersection}");
        GameObject intersectionInstance = Instantiate(intersectionPrefab, Vector3.zero, Quaternion.identity);

        intersectionManager = intersectionInstance.GetComponent<IntersectionRoutesManager>();

        if (intersectionManager == null)
        {
            Debug.LogError("IntersectionManager component is missing on the intersection prefab.");
        }

        InitializeDictionaries();
    }

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

    void CreateRoadUser(RoadUserData roadUserData, GameObject spawnPoint)
    {
        // Debug.Log($"{FilePath.PATH_PREFAB_ROAD_USERS}{roadUserData.ModelName}");
        GameObject roadUserInstance = Instantiate(PrefabManager.GetPrefab($"{FilePath.PATH_PREFAB_ROAD_USERS}{roadUserData.ModelName}"), spawnPoint.transform.position, spawnPoint.transform.rotation);
        roadUserInstance.name = $"{roadUserData.ModelName}_{roadUserData.SidePosition}_{roadUserData.NumberPosition}";
        roadUserInstance.tag = GetTagFromTypeParticipant(roadUserData.TypeParticipant);

        IntersectionRoutesManager.Direction roadUserDirection = GetDirectionFromData(roadUserData.SidePosition);
        Transform[] route = intersectionManager.GetRoute(roadUserDirection, roadUserData.MovementDirection);

        RoadUserMovement roadUserMovement = roadUserInstance.GetComponent<RoadUserMovement>();
        roadUserMovement.Route = route;
        roadUserMovement.RUD = roadUserData;
        
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

    void CreateSign(SignData signData, GameObject spawnPoint)
    {
        Instantiate(PrefabManager.GetPrefab($"{FilePath.PATH_PREFAB_SIGNS}{signData.ModelName}"), spawnPoint.transform.position, spawnPoint.transform.rotation);
    }

    void CreateTrafficLight(TrafficLightData trafficLightData, GameObject spawnPoint)
    {
        Instantiate(PrefabManager.GetPrefab($"{FilePath.PATH_PREFAB_TRAFFIC_LIGHTS}{trafficLightData.ModelName}{trafficLightData.State}"), spawnPoint.transform.position, spawnPoint.transform.rotation);
    }

    string GetModelName<T>(T entity)
    {
        return entity switch
        {
            RoadUserData roadUserData => roadUserData.ModelName,
            SignData signData => signData.ModelName,
            TrafficLightData trafficLightData => trafficLightData.ModelName,
            _ => throw new System.ArgumentException("Unknown entity type")
        };
    }

    string GetSidePosition<T>(T entity)
    {
        return entity switch
        {
            RoadUserData roadUserData => roadUserData.SidePosition,
            SignData signData => signData.SidePosition,
            TrafficLightData trafficLightData => trafficLightData.SidePosition,
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
