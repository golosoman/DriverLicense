using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class TrafficRuleEnforcerPoint2 : TrafficRuleEnforcer
{
    public override bool CheckObstacleOnRight(GameObject roadUserDataObject, RoadUserMovement roadUserMovement)
    {
        RoadUserData roadUserData = roadUserMovement.RUD;
        bool hasObstacleOnRight = false;

        GameObject visibilityZoneTrigger = roadUserDataObject.transform.Find(SceneObjectNames.VISIBILITY_AREA).gameObject;

        VisibilityZoneTrigger visibilityZoneScript = visibilityZoneTrigger.GetComponent<VisibilityZoneTrigger>();
        List<GameObject> visibleObjects = visibilityZoneScript.VisibleObjects;

        foreach (GameObject visibleObject in visibleObjects)
        {
            Vector3 carPosition = roadUserDataObject.transform.position;
            Vector3 otherCarPosition = visibleObject.transform.position;

            if (otherCarPosition.y > carPosition.y && CheckObstaclesConditions(SideDirectionTypes.EAST, SideDirectionTypes.SOUTH, 
                SideDirectionTypes.WEST, roadUserData))
            {
                hasObstacleOnRight = true;
                break;
            }
        }

        return hasObstacleOnRight;
    }
}