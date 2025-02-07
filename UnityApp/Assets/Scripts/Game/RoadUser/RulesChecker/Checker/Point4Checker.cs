using System;
using UnityEngine;
using UnityEngine.Events;

public class Point4Checker : CheckerHandler
{
    public delegate void PointWay4Enter(GameObject trafficParticipant);
    public static event PointWay4Enter OnPointWay4Enter;
    public delegate void PointWay4Exit(GameObject trafficParticipant);
    public static event PointWay4Exit OnPointWay4Exit;

    public override void CheckObstacleOnRight(GameObject trafficParticipant)
    {
        OnPointWay4Enter?.Invoke(trafficParticipant);
    }

    public override void ReCheckObstacleOnRight(GameObject trafficParticipant)
    {
        OnPointWay4Exit.Invoke(trafficParticipant);
    }
}