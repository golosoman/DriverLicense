using System;
using UnityEngine;
using UnityEngine.Events;

public class Point3Checker : CheckerHandler
{
    public delegate void PointWay3Enter(GameObject trafficParticipant);
    public static event PointWay3Enter OnPointWay3CheckObstacleEnter;
    public static event PointWay3Enter OnPointWay3CheckPriority1Enter;
    public static event PointWay3Enter OnPaintWay3CheckTrafficLightPriorityEnter;
    public delegate void PointWay3Exit(GameObject trafficParticipant);
    public static event PointWay3Exit OnPointWay3CheckObstacleExit;
    public static event PointWay3Exit OnPointWay3CheckPriorityExit;
    public static event PointWay3Exit OnPointWay3CheckTrafficLightPriorityExit;

    public override void CheckObstacleOnRight(GameObject trafficParticipant)
    {
        OnPointWay3CheckObstacleEnter?.Invoke(trafficParticipant);
    }

    public override void ReCheckObstacleOnRight(GameObject trafficParticipant)
    {
        OnPointWay3CheckObstacleExit.Invoke(trafficParticipant);
    }

    public override void CheckSignPriority(GameObject trafficParticipant)
    {
        OnPointWay3CheckPriority1Enter?.Invoke(trafficParticipant);
    }

    public override void ReCheckSignPriority(GameObject trafficParticipant)
    {
        OnPointWay3CheckPriorityExit?.Invoke(trafficParticipant);
    }

    public override void CheckTrafficLightPriority(GameObject trafficParticipant)
    {
        OnPaintWay3CheckTrafficLightPriorityEnter?.Invoke(trafficParticipant);
    }

    public override void RecheckTrafficLightPriority(GameObject trafficParticipant)
    {
        OnPointWay3CheckTrafficLightPriorityExit?.Invoke(trafficParticipant);
    }
}