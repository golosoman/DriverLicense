using System;
using UnityEngine;
using UnityEngine.Events;

public class Point2Checker : CheckerHandler
{
    public delegate void PointWay2Enter(GameObject trafficParticipant);
    public static event PointWay2Enter OnPointWay2CheckObstacleEnter;
    public static event PointWay2Enter OnPointWay2CheckPriority1Enter;
    public static event PointWay2Enter OnPaintWay2CheckTrafficLightPriorityEnter;
    public delegate void PointWay2Exit(GameObject trafficParticipant);
    public static event PointWay2Exit OnPointWay2CheckObstacleExit;
    public static event PointWay2Exit OnPointWay2CheckPriorityExit;
    public static event PointWay2Exit OnPointWay2CheckTrafficLightPriorityExit;

    public override void CheckObstacleOnRight(GameObject trafficParticipant)
    {
        OnPointWay2CheckObstacleEnter?.Invoke(trafficParticipant);
    }

    public override void ReCheckObstacleOnRight(GameObject trafficParticipant)
    {
        OnPointWay2CheckObstacleExit.Invoke(trafficParticipant);
    }

    public override void CheckSignPriority(GameObject trafficParticipant)
    {
        OnPointWay2CheckPriority1Enter?.Invoke(trafficParticipant);
    }

    public override void ReCheckSignPriority(GameObject trafficParticipant)
    {
        OnPointWay2CheckPriorityExit?.Invoke(trafficParticipant);
    }

    public override void CheckTrafficLightPriority(GameObject trafficParticipant)
    {
        OnPaintWay2CheckTrafficLightPriorityEnter?.Invoke(trafficParticipant);
    }

    public override void RecheckTrafficLightPriority(GameObject trafficParticipant)
    {
        OnPointWay2CheckTrafficLightPriorityExit?.Invoke(trafficParticipant);
    }
}
