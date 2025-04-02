using System;
using UnityEngine;
using UnityEngine.Events;

public class Point4Checker : CheckerHandler
{
    public delegate void PointWay4Enter(GameObject trafficParticipant);
    public static event PointWay4Enter OnPointWay4CheckObstacleEnter;
    public static event PointWay4Enter OnPointWay4CheckPriority1Enter;
    public static event PointWay4Enter OnPaintWay4CheckTrafficLightPriorityEnter;
    public delegate void PointWay4Exit(GameObject trafficParticipant);
    public static event PointWay4Exit OnPointWay4CheckObstacleExit;
    public static event PointWay4Exit OnPointWay4CheckPriorityExit;
    public static event PointWay4Exit OnPointWay4CheckTrafficLightPriorityExit;

    public override void CheckObstacleOnRight(GameObject trafficParticipant)
    {
        OnPointWay4CheckObstacleEnter?.Invoke(trafficParticipant);
    }

    public override void ReCheckObstacleOnRight(GameObject trafficParticipant)
    {
        OnPointWay4CheckObstacleExit.Invoke(trafficParticipant);
    }

    public override void CheckSignPriority(GameObject trafficParticipant)
    {
        OnPointWay4CheckPriority1Enter?.Invoke(trafficParticipant);
    }

    public override void ReCheckSignPriority(GameObject trafficParticipant)
    {
        OnPointWay4CheckPriorityExit?.Invoke(trafficParticipant);
    }

    public override void CheckTrafficLightPriority(GameObject trafficParticipant)
    {
        OnPaintWay4CheckTrafficLightPriorityEnter?.Invoke(trafficParticipant);
    }

    public override void RecheckTrafficLightPriority(GameObject trafficParticipant)
    {
        OnPointWay4CheckTrafficLightPriorityExit?.Invoke(trafficParticipant);
    }
}