using System;
using UnityEngine;

public class CarPoint4Checker : CarPointHandler
{
    public delegate void StopCar(GameObject trafficParticipant);
    public static event StopCar OnStopCar; // Событие экземпляра

    public delegate void HasObstacle(GameObject trafficParticipant);
    public static event HasObstacle OnHasObstacle; // Событие экземпляра


    private void OnPointWay4ExitHandler(GameObject trafficParticipant)
    {
        GameObject otherTrafficParticipant = CarInSpawnPoint();
        if (otherTrafficParticipant != null && TrafficRuleChecker
            .CheckIntersectionWithAnotherRoadUser(trafficParticipant,
            otherTrafficParticipant, trafficParticipant.GetComponent<CarMovement>(),
            otherTrafficParticipant.GetComponent<CarMovement>()))
        {
            Debug.Log("Не пропустил помеху сзади");
            OnHasObstacle?.Invoke(trafficParticipant);
        }
    }


    private void OnPointWay4EnterHandler(GameObject trafficParticipant)
    {
        GameObject otherTrafficParticipant = CarInSpawnPoint();
        if (otherTrafficParticipant != null && TrafficRuleChecker
            .CheckIntersectionWithAnotherRoadUser(trafficParticipant,
            otherTrafficParticipant, trafficParticipant.GetComponent<CarMovement>(),
            otherTrafficParticipant.GetComponent<CarMovement>()))
        {
            Debug.Log("Car is in the spawn point at point 3! Sending stopCar event.");
            OnStopCar?.Invoke(trafficParticipant);
        }
    }

    protected override void SubscribeToEvents()
    {
        Point4Checker.OnPointWay4Enter += OnPointWay4EnterHandler;
        Point4Checker.OnPointWay4Exit += OnPointWay4ExitHandler;
    }

    protected override void UnsubscribeFromEvents()
    {
        Point4Checker.OnPointWay4Enter -= OnPointWay4EnterHandler;
        Point4Checker.OnPointWay4Exit -= OnPointWay4ExitHandler;
    }
}
