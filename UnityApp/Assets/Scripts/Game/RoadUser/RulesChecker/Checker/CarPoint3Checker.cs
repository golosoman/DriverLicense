using System;
using UnityEngine;

public class CarPoint3Checker : CarPointHandler
{
    public delegate void StopCar(GameObject trafficParticipant);
    public static event StopCar OnStopCar; // Событие экземпляра

    public delegate void HasObstacle(GameObject trafficParticipant);
    public static event HasObstacle OnHasObstacle; // Событие экземпляра

    private void OnPointWay3ExitHandler(GameObject trafficParticipant)
    {
        GameObject otherTrafficParticipant = CarInSpawnPoint();
        if (otherTrafficParticipant != null && TrafficRuleChecker
            .CheckIntersectionWithAnotherRoadUser(trafficParticipant,
            otherTrafficParticipant, trafficParticipant.GetComponent<CarMovement>(),
            otherTrafficParticipant.GetComponent<CarMovement>()))
        {
            Debug.Log("Не пропустил помеху спереди");
            OnHasObstacle?.Invoke(trafficParticipant);
        }
    }

    private void OnPointWay3EnterHandler(GameObject trafficParticipant)
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
        Point3Checker.OnPointWay3Enter += OnPointWay3EnterHandler;
        Point3Checker.OnPointWay3Exit += OnPointWay3ExitHandler;
    }

    protected override void UnsubscribeFromEvents()
    {
        Point3Checker.OnPointWay3Enter -= OnPointWay3EnterHandler;
        Point3Checker.OnPointWay3Exit -= OnPointWay3ExitHandler;
    }
}
