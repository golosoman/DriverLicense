using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class TrafficRuleEnforcerPoint2 : TrafficRuleEnforcer
{
    public override bool CheckObstacleOnRight(GameObject roadUserDataObject, RoadUserData roadUserData)
    {
        Dictionary<string, Dictionary<RoadUserData, GameObject>> roadUsers = RoadUserManager.RoadUsers;

        bool hasObstacleOnRight = false;

        foreach (KeyValuePair<RoadUserData, GameObject> roadUser in roadUsers[TagObjectNamesTypes.CAR])
        {
            // Пропустить текущий автомобиль
            if (roadUser.Value == roadUserDataObject)
                continue;

            Vector3 carPosition = roadUserDataObject.transform.position;
            Vector3 otherCarPosition = roadUser.Value.transform.position;

            if (otherCarPosition.y > carPosition.y)
            {
                if (roadUserData.SidePosition == SideDirectionTypes.EAST && (roadUserData.MovementDirection == DirectionMovementTypes.FORWARD || 
                    roadUserData.MovementDirection == DirectionMovementTypes.LEFT || 
                    roadUserData.MovementDirection == DirectionMovementTypes.BACKWARD))
                    { hasObstacleOnRight = true; break; }

                if (roadUserData.SidePosition == SideDirectionTypes.SOUTH && (roadUserData.MovementDirection == DirectionMovementTypes.LEFT || 
                    roadUserData.MovementDirection == DirectionMovementTypes.BACKWARD))
                    { hasObstacleOnRight = true; break; }
                
                if (roadUserData.SidePosition == SideDirectionTypes.WEST && roadUserData.MovementDirection == DirectionMovementTypes.BACKWARD)
                    { hasObstacleOnRight = true; break; }
            }
        }

        return hasObstacleOnRight;
    }
}