using UnityEngine;
using System.Collections.Generic;

public class RoadUserManager : MonoBehaviour
{
    private List<RoadUserData> selectedRoadUsers;
    public void Initialize(RoadUserData[] roadUserDatas){
        selectedRoadUsers = new List<RoadUserData>(roadUserDatas);
    }

    public List<RoadUserData> SelectedRoadUsers => selectedRoadUsers;

    public void SelectRoadUser(RoadUserData roadUserData)
    {
        if (!selectedRoadUsers.Contains(roadUserData))
        {
            selectedRoadUsers.Add(roadUserData);
        }
    }

    public void DeleteRoadUser(RoadUserData roadUserData){
        if (IsRoadUserSelected(roadUserData)){
            selectedRoadUsers.Remove(roadUserData);
        }
    }

    public bool IsRoadUserSelected(RoadUserData roadUserData)
    {
        return selectedRoadUsers.Contains(roadUserData);
    }
}
