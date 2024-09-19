using UnityEngine;
using System.Collections.Generic;

public class RoadManager : MonoBehaviour
{
    private string typeIntersection;
    private TrafficLightData[] trafficLightDatas;
    private SignData[] signDatas;
    // private Dictionary<string, Dictionary<RoadUserData, GameObject>> roadUsers;
    public string TypeIntersection { get => typeIntersection; }
    public TrafficLightData[] TrafficLightDatas { get => trafficLightDatas; }
    public SignData[] SignDatas { get => signDatas; }
    public bool ViolationRules {get; set;}
    // public Dictionary<string, Dictionary<RoadUserData, GameObject>> RoadUsers { get => roadUsers; set => roadUsers = value; }

    public void Initialize(string typeIntersection, TrafficLightData[] trafficLightDatas, SignData[] signDatas){
        // roadUsers = new Dictionary<string, Dictionary<RoadUserData, GameObject>>();
        // // Инициализация ключей
        // roadUsers[RoadUserTypes.CAR] = new Dictionary<RoadUserData, GameObject>();
        // roadUsers[RoadUserTypes.HUMAN] = new Dictionary<RoadUserData, GameObject>();
        // roadUsers[RoadUserTypes.TRAM] = new Dictionary<RoadUserData, GameObject>();
        this.typeIntersection = typeIntersection;
        this.trafficLightDatas = trafficLightDatas;
        this.signDatas = signDatas;
        ViolationRules = false;
    }

    // public void AddRoadUser(RoadUserData newRoadUserData, GameObject newRoadUserObject){
    // if (!roadUsers.ContainsKey(newRoadUserData.TypeParticipant)) {
    //     roadUsers[newRoadUserData.TypeParticipant] = new Dictionary<RoadUserData, GameObject>();
    // }
    // roadUsers[newRoadUserData.TypeParticipant].Add(newRoadUserData, newRoadUserObject);
    // }
}
