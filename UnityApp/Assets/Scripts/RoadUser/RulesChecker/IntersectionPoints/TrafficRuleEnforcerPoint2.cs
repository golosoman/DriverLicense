using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class TrafficRuleEnforcerPoint2 : TrafficRuleEnforcer
{
    public override bool CheckObstacleOnRight(GameObject roadUserDataObject, RoadUserMovement roadUserMovement)
    {
        GameObject visibilityZoneTrigger = roadUserDataObject.transform.Find(SceneObjectNames.VISIBILITY_AREA).gameObject;

        VisibilityZoneTrigger visibilityZoneScript = visibilityZoneTrigger.GetComponent<VisibilityZoneTrigger>();
        List<GameObject> visibleObjects = visibilityZoneScript.VisibleObjects;

        foreach (GameObject visibleObject in visibleObjects)
        {
            Vector3 userPosition = roadUserDataObject.transform.position;
            Vector3 otherUserPosition = visibleObject.transform.position;
            
            if (otherUserPosition.y > userPosition.y)
            {
                RoadUserMovement otherCarMovement = visibleObject.GetComponent<RoadUserMovement>();
                if (otherCarMovement.CurrentPoint + 1 < otherCarMovement.Route.Length && roadUserMovement.CurrentPoint + 1 < roadUserMovement.Route.Length)
                {
                    // Debug.Log("Получилось получить поинты");
                    Vector3 otherUserPositionEnd = otherCarMovement.Route[otherCarMovement.CurrentPoint + 1].transform.position;
                    Vector3 userPositionEnd = roadUserMovement.Route[roadUserMovement.CurrentPoint + 1].transform.position;
                    // Debug.Log(otherCarMovement.CurrentPoint + "        " + roadUserMovement.CurrentPoint);
                    // Debug.Log(CheckIntersectionSecondType(userPosition, userPositionEnd, otherUserPosition, otherUserPositionEnd));
                    // Debug.Log(userPosition + "   " + userPositionEnd + "   " + otherUserPosition + "  " + otherUserPositionEnd);
                    if (CheckIntersectionSecondType(userPosition, userPositionEnd, otherUserPosition, otherUserPositionEnd)){
                        hasObstacleOnRight = true;
                        break;
                    }
                }
            }
        }

        return hasObstacleOnRight;
    }
}