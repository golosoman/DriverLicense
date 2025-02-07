using System;
using UnityEngine;
using UnityEngine.Events;

public class Point2Checker : CheckerHandler
{
    public delegate void PointWay2Enter(GameObject trafficParticipant);
    public static event PointWay2Enter OnPointWay2Enter;
    public delegate void PointWay2Exit(GameObject trafficParticipant);
    public static event PointWay2Exit OnPointWay2Exit;

    public override void CheckObstacleOnRight(GameObject trafficParticipant)
    {
        OnPointWay2Enter?.Invoke(trafficParticipant);
    }

    public override void ReCheckObstacleOnRight(GameObject trafficParticipant)
    {
        OnPointWay2Exit.Invoke(trafficParticipant);
    }
}
