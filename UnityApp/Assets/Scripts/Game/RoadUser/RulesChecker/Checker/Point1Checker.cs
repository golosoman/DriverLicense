using System;
using UnityEngine;
using UnityEngine.Events;

public class Point1Checker : CheckerHandler
{
    public delegate void PointWay1Enter(GameObject trafficParticipant);
    public static event PointWay1Enter OnPointWay1Enter;
    public delegate void PointWay1Exit(GameObject trafficParticipant);
    public static event PointWay1Exit OnPointWay1Exit;

    // private void Start()
    // {
    //     BaseInit();
    //     // CarPoint1Checker.
    // }

    public override void CheckObstacleOnRight(GameObject trafficParticipant)
    {
        Debug.Log("Проверяю помеху справа");
        OnPointWay1Enter.Invoke(trafficParticipant);
    }

    public override void ReCheckObstacleOnRight(GameObject trafficParticipant)
    {
        Debug.Log("Проверяю помеху справа еще один раз");
        OnPointWay1Exit.Invoke(trafficParticipant);
    }

}