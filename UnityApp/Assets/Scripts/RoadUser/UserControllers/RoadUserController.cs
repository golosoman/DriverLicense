using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadUserController : MonoBehaviour
{
    private RoadUserMovement roadUserMovement;
    private RuleChecker ruleChecker;
    private RoadUserManager roadUserManager;
    // private TrafficRulesManager trafficRulesManager;

    void Start()
    {
        ruleChecker = GetComponent<RuleChecker>();
        roadUserMovement = GetComponent<RoadUserMovement>();
        roadUserManager = FindObjectOfType<RoadUserManager>();
        // trafficRulesManager = FindObjectOfType<TrafficRulesManager>();

        if (roadUserMovement is null)
        {
             Debug.LogError("RoadUserMovement component is missing on the roadUser object.");
        }
    }

    void OnMouseDown()
    {
        // if (roadUserMovement != null && trafficRulesManager != null)
        if (roadUserMovement != null)
        {
            // trafficRulesManager.UserSelectRoadUser(roadUserMovement);
            roadUserMovement.StartMovement();
            // roadUserManager.DeleteRoadUser(roadUserMovement.RUD);
            // Debug.Log(ruleChecker.IsMovementAllowed(roadUserMovement.RUD));
        }
        else
        {
            Debug.LogError("RoadUserMovement or TrafficRulesManager is null. Cannot start movement.");
        }
    }
}