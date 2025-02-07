using System;
using UnityEngine;

public class CarPoint2Checker : CarPointHandler
{
    public delegate void StopCar(GameObject trafficParticipant);
    public static event StopCar OnStopCar; // Событие экземпляра

    public delegate void HasObstacle(GameObject trafficParticipant);
    public static event HasObstacle OnHasObstacle; // Событие экземпляра

    private void OnPointWay2ExitHandler(GameObject trafficParticipant)
    {
        GameObject otherTrafficParticipant = CarInSpawnPoint();
        if (otherTrafficParticipant != null && TrafficRuleChecker
            .CheckIntersectionWithAnotherRoadUser(trafficParticipant,
            otherTrafficParticipant, trafficParticipant.GetComponent<CarMovement>(),
            otherTrafficParticipant.GetComponent<CarMovement>()))
        {
            Debug.Log("Не пропустил помеху слева");
            OnHasObstacle?.Invoke(trafficParticipant);
        }
    }

    private void OnPointWay2EnterHandler(GameObject trafficParticipant)
    {
        GameObject otherTrafficParticipant = CarInSpawnPoint();
        if (otherTrafficParticipant != null && TrafficRuleChecker
            .CheckIntersectionWithAnotherRoadUser(trafficParticipant,
            otherTrafficParticipant, trafficParticipant.GetComponent<CarMovement>(),
            otherTrafficParticipant.GetComponent<CarMovement>()))
        {
            Debug.Log("Car is in the spawn point at point 2! Sending stopCar event.");
            OnStopCar?.Invoke(trafficParticipant);
        }
    }

    protected override void SubscribeToEvents()
    {
        Point2Checker.OnPointWay2Enter += OnPointWay2EnterHandler;
        Point2Checker.OnPointWay2Exit += OnPointWay2ExitHandler;
    }

    protected override void UnsubscribeFromEvents()
    {
        Point2Checker.OnPointWay2Enter -= OnPointWay2EnterHandler;
        Point2Checker.OnPointWay2Exit -= OnPointWay2ExitHandler;
    }
}
