using System;
using UnityEngine;
using UnityEngine.Events;

public class Point1Checker : CheckerHandler
{
    public delegate void PointWay1Enter(GameObject trafficParticipant);
    public static event PointWay1Enter OnPointWay1CheckObstacle1Enter;
    public static event PointWay1Enter OnPointWay1CheckPriority1Enter;
    public delegate void PointWay1Exit(GameObject trafficParticipant);
    public static event PointWay1Exit OnPointWay1CheckObstacleExit;
    public static event PointWay1Exit OnPointWay1CheckPriorityExit;

    // private void Start()
    // {
    //     BaseInit();
    //     // CarPoint1Checker.
    // }

    public override void CheckObstacleOnRight(GameObject trafficParticipant)
    {
        Debug.Log("Проверяю помеху справа");
        OnPointWay1CheckObstacle1Enter.Invoke(trafficParticipant);
    }

    public override void ReCheckObstacleOnRight(GameObject trafficParticipant)
    {
        Debug.Log("Проверяю помеху справа еще один раз");
        OnPointWay1CheckObstacleExit.Invoke(trafficParticipant);
    }

    public override void CheckSignPriority(GameObject trafficParticipant)
    {
        OnPointWay1CheckPriority1Enter?.Invoke(trafficParticipant);
    }

    public override void ReCheckSignPriority(GameObject trafficParticipant)
    {
        OnPointWay1CheckPriorityExit?.Invoke(trafficParticipant);
    }
}