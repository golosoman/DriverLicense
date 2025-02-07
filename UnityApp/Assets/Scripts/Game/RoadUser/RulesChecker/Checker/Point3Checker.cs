using System;
using UnityEngine;
using UnityEngine.Events;

public class Point3Checker : CheckerHandler
{
    public delegate void PointWay3Enter(GameObject trafficParticipant);
    public static event PointWay3Enter OnPointWay3Enter;
    public delegate void PointWay3Exit(GameObject trafficParticipant);
    public static event PointWay3Exit OnPointWay3Exit;

    public override void CheckObstacleOnRight(GameObject trafficParticipant)
    {
        OnPointWay3Enter?.Invoke(trafficParticipant);
    }

    public override void ReCheckObstacleOnRight(GameObject trafficParticipant)
    {
        OnPointWay3Exit.Invoke(trafficParticipant);
    }
}