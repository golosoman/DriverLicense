using UnityEngine;
using System.Collections.Generic;

public class RoadUserManager : MonoBehaviour
{
    private string typeIntersection;
    private Dictionary<string, Dictionary<RoadUserData, GameObject>> roadUsers;
    public string TypeIntersection {get => typeIntersection; set => typeIntersection = value;}
    public Dictionary<string, Dictionary<RoadUserData, GameObject>> RoadUsers { get => roadUsers; set => roadUsers = value; }

    public void Initialize(string intersection){
        roadUsers = new Dictionary<string, Dictionary<RoadUserData, GameObject>>();
        // Инициализация ключей
        roadUsers[RoadUserTypes.CAR] = new Dictionary<RoadUserData, GameObject>();
        roadUsers[RoadUserTypes.HUMAN] = new Dictionary<RoadUserData, GameObject>();
        roadUsers[RoadUserTypes.TRAM] = new Dictionary<RoadUserData, GameObject>();
        typeIntersection = intersection;
    }

    public void AddRoadUser(RoadUserData newRoadUserData, GameObject newRoadUserObject){
    if (!roadUsers.ContainsKey(newRoadUserData.TypeParticipant)) {
        roadUsers[newRoadUserData.TypeParticipant] = new Dictionary<RoadUserData, GameObject>();
    }
    roadUsers[newRoadUserData.TypeParticipant].Add(newRoadUserData, newRoadUserObject);
    }
}
