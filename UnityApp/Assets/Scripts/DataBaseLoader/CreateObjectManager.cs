using System.Collections.Generic;
using UnityEngine;

public class CreateObjectManager : ScriptableObject
{
    private Dictionary<string, GameObject> roadUserSpawnPoints = new();
    private Dictionary<string, GameObject> signSpawnPoints = new();
    private Dictionary<string, GameObject> trafficLightSpawnPoints = new();
    private IntersectionManager intersectionManager;

    public void ProcessTicketData(TicketData ticketData)
    {
        CreateIntersection(ticketData.typeIntersection);
        CreateRoadUsers(ticketData.roadUsersArr);
        CreateSigns(ticketData.signsArr);
        CreateTrafficLights(ticketData.trafficLightsArr);
    }

    void CreateIntersection(string intersection)
    {
        GameObject intersectionPrefab = PrefabManager.GetPrefab($"Prefabs/intersections/{intersection}");
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
            GameObject roadUserPrefab = PrefabManager.GetPrefab($"Prefabs/roadUsers/{roadUserData.modelName}");
            if (roadUserPrefab != null)
            {
                GameObject spawnPoint;
                if (roadUserSpawnPoints.TryGetValue(roadUserData.sidePosition, out spawnPoint))
                {
                    GameObject roadUserInstance = Instantiate(roadUserPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);

                    // Добавляем уникальное имя и тег
                    roadUserInstance.name = $"{roadUserData.modelName}_{roadUserData.sidePosition}_{roadUserData.numberPosition}";
                    switch (roadUserData.typeParticipant)
                    {
                        case "Car":
                            roadUserInstance.tag = "Car";
                            break;
                        case "Human":
                            roadUserInstance.tag = "Human";
                            break;
                        case "Tram":
                            roadUserInstance.tag = "Tram";
                            break;
                        default:
                            Debug.LogWarning($"Unknown typeParticipant: {roadUserData.typeParticipant}. Using default tag.");
                            // roadUserInstance.tag = "Untagged";
                            break;
                    }

                    IntersectionManager.Direction roadUserDirection = GetDirectionFromData(roadUserData.sidePosition);
                    Transform[] route = intersectionManager.GetRoute(roadUserDirection, roadUserData.movementDirection);

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
                return IntersectionManager.Direction.North;
        }
    }

    void CreateSigns(SignData[] signs)
    {
        foreach (SignData signData in signs)
        {
            GameObject signPrefab = PrefabManager.GetPrefab($"Prefabs/signs/{signData.modelName}");
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
           GameObject trafficLightPrefab = PrefabManager.GetPrefab($"Prefabs/trafficLights/{trafficLightData.modelName}");
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
}
