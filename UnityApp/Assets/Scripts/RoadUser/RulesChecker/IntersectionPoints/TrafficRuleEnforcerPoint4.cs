using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrafficRuleEnforcerPoint4 : TrafficRuleEnforcer
{
    public override bool CheckObstacleOnRight(GameObject gameObject, RoadUserMovement userMovement, Vector3 rayPosition)
    {
        Vector3 direction = RoadUserManager.RoadUserSpawnPoints[SideDirectionTypes.EAST].transform.position;
        return CheckObstacleWithRays(gameObject, userMovement, rayPosition, direction);
    }

    public override bool ReCheckObstacleOnRight(Vector3 rayPosition)
    {
        return CheckRoadUser(LetOutRay(rayPosition, Vector2.down));
    }
}