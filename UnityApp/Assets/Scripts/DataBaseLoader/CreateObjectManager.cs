using System.Collections.Generic;
using UnityEngine;

public class CreateObjectManager : ScriptableObject
{
    private Dictionary<string, GameObject> roadUserSpawnPoints = new();
    private Dictionary<string, GameObject> signSpawnPoints = new();
    private Dictionary<string, GameObject> trafficLightSpawnPoints = new();
    private IntersectionManager intersectionManager;
    private TicketData ticketData;

    public void ProcessTicketData(TicketData ticketData)
    {
        this.ticketData = ticketData;
        InitRuleManager(ticketData.RoadUsersArr);
        CreateIntersection(ticketData.TypeIntersection);
        CreateEntities(ticketData.RoadUsersArr, roadUserSpawnPoints, "Prefabs/roadUsers/", CreateRoadUser);
        CreateEntities(ticketData.SignsArr, signSpawnPoints, "Prefabs/signs/", CreateSign);
        CreateEntities(ticketData.TrafficLightsArr, trafficLightSpawnPoints, "Prefabs/trafficLights/", CreateTrafficLight);
    }

    void InitRuleManager(RoadUserData[] roadUserData){
        RoadUserManager roadUserManager = FindObjectOfType<RoadUserManager>();
        roadUserManager.Initialize(roadUserData);
    }

    void CreateIntersection(string intersection)
    {
        GameObject intersectionPrefab = PrefabManager.GetPrefab($"Prefabs/intersections/{intersection}");
        GameObject intersectionInstance = Instantiate(intersectionPrefab, Vector3.zero, Quaternion.identity);

        intersectionManager = intersectionInstance.GetComponent<IntersectionManager>();

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
        GameObject roadUserInstance = Instantiate(PrefabManager.GetPrefab($"Prefabs/roadUsers/{roadUserData.ModelName}"), spawnPoint.transform.position, spawnPoint.transform.rotation);
        roadUserInstance.name = $"{roadUserData.ModelName}_{roadUserData.SidePosition}_{roadUserData.NumberPosition}";
        roadUserInstance.tag = GetTagFromTypeParticipant(roadUserData.TypeParticipant);

        IntersectionManager.Direction roadUserDirection = GetDirectionFromData(roadUserData.SidePosition);
        Transform[] route = intersectionManager.GetRoute(roadUserDirection, roadUserData.MovementDirection);

        RoadUserMovement roadUserMovement = roadUserInstance.GetComponent<RoadUserMovement>();
        roadUserMovement.Route = route;
        roadUserMovement.RUD = roadUserData;

        RuleChecker ruleChecker = roadUserInstance.GetComponent<CarRuleChecker>();
        ruleChecker.Initialize(ticketData);
    }

    private RuleChecker CreateRuleChecker(RoadUserData roadUserData)
    {
        switch (roadUserData.TypeParticipant)
        {
            case "car":
                return new CarRuleChecker();
            // case "human":
            //     return new HumanRuleChecker();
            // case "tram":
            //     return new TramRuleChecker();
            default:
                Debug.LogError("Unknown participant type: " + roadUserData.TypeParticipant);
                return null;
        }
    }

    void CreateSign(SignData signData, GameObject spawnPoint)
    {
        Instantiate(PrefabManager.GetPrefab($"Prefabs/signs/{signData.ModelName}"), spawnPoint.transform.position, spawnPoint.transform.rotation);
    }

    void CreateTrafficLight(TrafficLightData trafficLightData, GameObject spawnPoint)
    {
        Instantiate(PrefabManager.GetPrefab($"Prefabs/trafficLights/{trafficLightData.ModelName}"), spawnPoint.transform.position, spawnPoint.transform.rotation);
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
        return typeParticipant switch
        {
            "car" => "Car",
            "human" => "Human",
            "tram" => "Tram",
            _ => "Untagged"
        };
    }

    IntersectionManager.Direction GetDirectionFromData(string position)
    {
        return position switch
        {
            "west" => IntersectionManager.Direction.West,
            "east" => IntersectionManager.Direction.East,
            "north" => IntersectionManager.Direction.North,
            "south" => IntersectionManager.Direction.South,
            _ => IntersectionManager.Direction.North
        };
    }

    private void InitializeDictionaries()
    {
        roadUserSpawnPoints = new Dictionary<string, GameObject>
        {
            { "west", GameObject.Find("CarSpawnLeft") },
            { "east", GameObject.Find("CarSpawnRight") },
            { "north", GameObject.Find("CarSpawnTop") },
            { "south", GameObject.Find("CarSpawnBottom") }
        };

        signSpawnPoints = new Dictionary<string, GameObject>
        {
            { "west", GameObject.Find("SignSpawnLeft") },
            { "east", GameObject.Find("SignSpawnRight") },
            { "north", GameObject.Find("SignSpawnTop") },
            { "south", GameObject.Find("SignSpawnBottom") }
        };

        trafficLightSpawnPoints = new Dictionary<string, GameObject>
        {
            { "west", GameObject.Find("TrafficLightSpawnLeft") },
            { "east", GameObject.Find("TrafficLightSpawnRight") },
            { "north", GameObject.Find("TrafficLightSpawnTop") },
            { "south", GameObject.Find("TrafficLightSpawnBottom") }
        };
    }
}
