using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrafficRuleEnforcerPoint1 : TrafficRuleEnforcer
{
    public override bool CheckObstacleOnRight(GameObject gameObject, RoadUserMovement userMovement, Vector2 rayPosition)
    {
        Vector2 direction = RoadUserManager.RoadUserSpawnPoints[SideDirectionTypes.WEST].transform.position;
        return TrafficRuleChecker.CheckObstacleWithRays(gameObject, userMovement, rayPosition, direction);
    }

    public override bool ReCheckObstacleOnRight(Vector2 rayPosition)
    {
        return RaycastingUtils.CheckRoadUser(RaycastingUtils.LetOutRay(rayPosition, Vector2.up)) != null;
    }

    public override bool CheckPriority(GameObject gameObject, CarMovement userMovement, Vector2 rayPosition)
    {
        return CheckPriorityInternal(gameObject, userMovement, rayPosition, SideDirectionTypes.WEST);
    }

    public override bool ReCheckPriority(GameObject gameObject, CarMovement userMovement, Vector2 rayPosition)
    {
        return ReCheckPriorityInternal(gameObject, userMovement, rayPosition, Vector2.up, Vector2.right);
    }
}