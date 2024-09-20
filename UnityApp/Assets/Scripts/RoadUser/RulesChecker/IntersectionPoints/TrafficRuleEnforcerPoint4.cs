using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class TrafficRuleEnforcerPoint4 : TrafficRuleEnforcer
{
    public override bool CheckObstacleOnRight(Vector3 startPosition)
    {
       return CheckRoadUserOnRight(LetOutRay(startPosition, Quaternion.Euler(0f, 0f, -55f) * Vector2.up, 20f));
    }
}