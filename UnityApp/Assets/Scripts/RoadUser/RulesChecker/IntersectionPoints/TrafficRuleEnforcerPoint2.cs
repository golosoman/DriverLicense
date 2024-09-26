using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrafficRuleEnforcerPoint2 : TrafficRuleEnforcer
{
    public override bool CheckObstacleOnRight(GameObject gameObject, RoadUserMovement userMovement, Vector3 rayPosition)
    {
        Vector3 direction = RoadUserManager.RoadUserSpawnPoints[SideDirectionTypes.NORTH].transform.position;
        return TrafficRuleChecker.CheckObstacleWithRays(gameObject, userMovement, rayPosition, direction);
    }

    public override bool ReCheckObstacleOnRight(Vector3 rayPosition)
    {
        return RaycastingUtils.CheckRoadUser(RaycastingUtils.LetOutRay(rayPosition, Vector2.right)) != null;
    }

    public override bool CheckPriority(GameObject gameObject, CarMovement userMovement, Vector3 rayPosition)
    {
        return CheckPriorityInternal(gameObject, userMovement, rayPosition, SideDirectionTypes.NORTH);
    }

    public override bool ReCheckPriority(GameObject gameObject, CarMovement userMovement, Vector3 rayPosition)
    {
        return ReCheckPriorityInternal(gameObject, userMovement, rayPosition, Vector2.right, Vector2.down);
    }
}