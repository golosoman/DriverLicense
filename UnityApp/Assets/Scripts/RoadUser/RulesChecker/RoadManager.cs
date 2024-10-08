using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class RoadManager : MonoBehaviour
{
    private string typeIntersection;
    private TrafficLightData[] trafficLightDatas;
    private SignData[] signDatas;
    private Dictionary<string, GameObject> roadUserSpawnPoints;
    // private Dictionary<string, Dictionary<RoadUserData, GameObject>> roadUsers;
    public string TypeIntersection { get => typeIntersection; }
    public Dictionary<string, GameObject> RoadUserSpawnPoints { get => roadUserSpawnPoints; }
    public TrafficLightData[] TrafficLightDatas { get => trafficLightDatas; }
    public SignData[] SignDatas { get => signDatas; }
    public bool ViolationRules { get; set; }

    public void Initialize(string typeIntersection, TrafficLightData[] trafficLightDatas, SignData[] signDatas, Dictionary<string, GameObject> roadUserSpawnPoints)
    {
        this.typeIntersection = typeIntersection;
        this.trafficLightDatas = trafficLightDatas;
        this.signDatas = signDatas;
        this.roadUserSpawnPoints = roadUserSpawnPoints;
        ViolationRules = false;
    }
}
