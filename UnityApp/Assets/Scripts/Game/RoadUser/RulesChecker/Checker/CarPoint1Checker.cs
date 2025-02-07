using System;
using UnityEngine;

public class CarPoint1Checker : CarPointHandler
{
    public delegate void StopCar(GameObject trafficParticipant);
    public static event StopCar OnStopCar; // Событие экземпляра

    public delegate void HasObstacle(GameObject trafficParticipant);
    public static event HasObstacle OnHasObstacle; // Событие экземпляра

    private void OnPointWay1ExitHandler(GameObject trafficParticipant)
    {
        GameObject otherTrafficParticipant = CarInSpawnPoint();
        Debug.Log(otherTrafficParticipant);
        Debug.Log(TrafficRuleChecker
            .CheckIntersectionWithAnotherRoadUser(trafficParticipant,
            otherTrafficParticipant, trafficParticipant.GetComponent<CarMovement>(),
            otherTrafficParticipant.GetComponent<CarMovement>()));
        if (otherTrafficParticipant != null && TrafficRuleChecker
            .CheckIntersectionWithAnotherRoadUser(trafficParticipant,
            otherTrafficParticipant, trafficParticipant.GetComponent<CarMovement>(),
            otherTrafficParticipant.GetComponent<CarMovement>()))
        {
            Debug.Log("!!!!!!!!!!!!Не пропустил помеху справа");
            OnHasObstacle?.Invoke(trafficParticipant); // Теперь это работает
        }
    }


    private void OnPointWay1EnterHandler(GameObject trafficParticipant)
    {
        GameObject otherTrafficParticipant = CarInSpawnPoint();
        if (otherTrafficParticipant != null && TrafficRuleChecker
            .CheckIntersectionWithAnotherRoadUser(trafficParticipant,
            otherTrafficParticipant, trafficParticipant.GetComponent<CarMovement>(),
            otherTrafficParticipant.GetComponent<CarMovement>()))
        {
            Debug.Log("Car is in the spawn point! Sending stopCar event.");
            OnStopCar?.Invoke(trafficParticipant); // Теперь это работает тоже
        }
    }

    protected override void SubscribeToEvents()
    {
        Point1Checker.OnPointWay1Enter += OnPointWay1EnterHandler;
        Point1Checker.OnPointWay1Exit += OnPointWay1ExitHandler;
    }

    protected override void UnsubscribeFromEvents()
    {
        Point1Checker.OnPointWay1Enter -= OnPointWay1EnterHandler;
        Point1Checker.OnPointWay1Exit -= OnPointWay1ExitHandler;
    }
}
