using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadUserController : MonoBehaviour
{
    private RoadUserMovement roadUserMovement;
    private TrafficRulesManager trafficRulesManager;

    void Start()
    {
        roadUserMovement = GetComponent<RoadUserMovement>();
        trafficRulesManager = FindObjectOfType<TrafficRulesManager>();

        if (roadUserMovement != null)
        {
            Debug.Log("RoadUserMovement is found. Route length: " + roadUserMovement.GetLengthRoute());
        }
        else
        {
            Debug.LogError("RoadUserMovement component is missing on the roadUser object.");
        }
    }

    void OnMouseDown()
    {
        if (roadUserMovement != null && trafficRulesManager != null)
        {
            trafficRulesManager.UserSelectRoadUser(roadUserMovement);
            roadUserMovement.StartMovement();
        }
        else
        {
            Debug.LogError("RoadUserMovement or TrafficRulesManager is null. Cannot start movement.");
        }
    }
}

