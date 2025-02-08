using System;
using System.Collections;
using UnityEngine;

public class CarPoint1Checker : CarPointHandler
{
    public delegate void StopCar(GameObject trafficParticipant);
    public static event StopCar OnStopCar; // Событие экземпляра

    public delegate void HasObstacle(GameObject trafficParticipant);
    public static event HasObstacle OnHasObstacle; // Событие экземпляра

    public delegate void CheckEvent(GameObject trafficParticipant);
    public static event CheckEvent OnCheckAnotherCar;


    // ПРОЕЗД РАВНОЗНАЧНЫХ ПЕРЕКРЕСТКОВ *
    private void OnPointWay1ExitHandler(GameObject trafficParticipant)
    {

        GameObject otherTrafficParticipant = CarInSpawnPoint();

        if (otherTrafficParticipant != null && otherTrafficParticipant != trafficParticipant && TrafficRuleChecker
            .CheckIntersectionWithAnotherRoadUser(trafficParticipant,
            otherTrafficParticipant, trafficParticipant.GetComponent<CarMovement>(),
            otherTrafficParticipant.GetComponent<CarMovement>()))
        {
            OnStopCar?.Invoke(trafficParticipant);
            OnHasObstacle?.Invoke(trafficParticipant);
        }
    }


    private void OnPointWay1EnterHandler(GameObject trafficParticipant)
    {
        GameObject otherTrafficParticipant = CarInSpawnPoint();
        if (otherTrafficParticipant != null && otherTrafficParticipant != trafficParticipant && TrafficRuleChecker
            .CheckIntersectionWithAnotherRoadUser(trafficParticipant,
            otherTrafficParticipant, trafficParticipant.GetComponent<CarMovement>(),
            otherTrafficParticipant.GetComponent<CarMovement>()))
        {
            Debug.Log("Car is in the spawn point! Sending stopCar event.");
            OnStopCar?.Invoke(trafficParticipant);
        }
    }
    // *


    // ПРОЕЗД НЕРЕГУЛИРУЕМЫХ ПЕРЕКРЕСТКОВ **
    private void OnPointWay1CheckPriorityExitHandler(GameObject trafficParticipant)
    {
        HandlePriorityExit(trafficParticipant, 1);
    }

    private void OnPointWay1CheckPriority1EnterHandler(GameObject trafficParticipant)
    {
        HandlePriorityEnter(trafficParticipant, 1);
    }

    private void HandlePriorityExit(GameObject trafficParticipant, int pointNumber = -1)
    {
        CarMovement trafficParticipantMovement = trafficParticipant.GetComponent<CarMovement>();

        if (trafficParticipantMovement.HasPriority)
        {
            GameObject otherTrafficParticipant = CarInSpawnPoint();
            Debug.Log(otherTrafficParticipant);
            if (otherTrafficParticipant != null && otherTrafficParticipant != trafficParticipant)
            {
                CarMovement otherTrafficParticipantMovement = otherTrafficParticipant.GetComponent<CarMovement>();
                if (otherTrafficParticipantMovement.HasPriority && TrafficRuleChecker
                    .CheckIntersectionWithAnotherRoadUser(trafficParticipant, otherTrafficParticipant, trafficParticipantMovement, otherTrafficParticipantMovement))
                {
                    Debug.Log($"Car is in spawn point at point {pointNumber}! Sending stopCar event.");
                    OnStopCar?.Invoke(trafficParticipant);
                    OnHasObstacle?.Invoke(trafficParticipant);
                }
            }
        }
        else
        {
            GameObject otherTrafficParticipant = CarInSpawnPoint();
            if (otherTrafficParticipant != null && otherTrafficParticipant != trafficParticipant)
            {
                CarMovement otherTrafficParticipantMovement = otherTrafficParticipant.GetComponent<CarMovement>();

                if (otherTrafficParticipantMovement.HasPriority && TrafficRuleChecker
                    .CheckIntersectionWithAnotherRoadUser(trafficParticipant, otherTrafficParticipant, trafficParticipantMovement, otherTrafficParticipantMovement))
                {
                    Debug.Log($"Car is in spawn point at point {pointNumber}! Sending stopCar event.");
                    OnStopCar?.Invoke(trafficParticipant);
                    OnHasObstacle?.Invoke(trafficParticipant);
                }
                else if (!otherTrafficParticipantMovement.HasPriority && TrafficRuleChecker
                    .CheckIntersectionWithAnotherRoadUser(trafficParticipant, otherTrafficParticipant, trafficParticipantMovement, otherTrafficParticipantMovement))
                {
                    Debug.Log($"Car is in spawn point at point {pointNumber}! Sending stopCar event.");
                    OnStopCar?.Invoke(trafficParticipant);
                }
            }
        }
    }

    private void HandlePriorityEnter(GameObject trafficParticipant, int pointNumber)
    {
        CarMovement trafficParticipantMovement = trafficParticipant.GetComponent<CarMovement>();

        if (trafficParticipantMovement.HasPriority)
        {
            GameObject otherTrafficParticipant = CarInSpawnPoint();
            if (otherTrafficParticipant != null && otherTrafficParticipant != trafficParticipant)
            {
                CarMovement otherTrafficParticipantMovement = otherTrafficParticipant.GetComponent<CarMovement>();
                if (otherTrafficParticipantMovement.HasPriority && TrafficRuleChecker
                    .CheckIntersectionWithAnotherRoadUser(trafficParticipant, otherTrafficParticipant, trafficParticipantMovement, otherTrafficParticipantMovement))
                {
                    Debug.Log($"Car is in spawn point at point {pointNumber}! Sending stopCar event.");
                    OnStopCar?.Invoke(trafficParticipant);
                }
            }
        }
        else
        {
            StartCoroutine(WaitForCheckFirstCar(trafficParticipant));

            GameObject otherTrafficParticipant = CarInSpawnPoint();
            if (otherTrafficParticipant != null && otherTrafficParticipant != trafficParticipant)
            {
                CarMovement otherTrafficParticipantMovement = otherTrafficParticipant.GetComponent<CarMovement>();
                if (otherTrafficParticipantMovement.HasPriority && TrafficRuleChecker
                    .CheckingIntersectionLastCoordinates(trafficParticipant, otherTrafficParticipant, trafficParticipantMovement, otherTrafficParticipantMovement))
                {
                    OnStopCar?.Invoke(trafficParticipant);
                    OnHasObstacle?.Invoke(trafficParticipant);
                }
                else if (otherTrafficParticipantMovement.HasPriority && TrafficRuleChecker
                    .CheckIntersectionWithAnotherRoadUser(trafficParticipant, otherTrafficParticipant, trafficParticipantMovement, otherTrafficParticipantMovement))
                {
                    OnStopCar?.Invoke(trafficParticipant);
                }
                else if (!otherTrafficParticipantMovement.HasPriority && TrafficRuleChecker
                    .CheckIntersectionWithAnotherRoadUser(trafficParticipant, otherTrafficParticipant, trafficParticipantMovement, otherTrafficParticipantMovement))
                {
                    Debug.Log($"Car is in spawn point at point {pointNumber}! Sending stopCar event.");
                    OnStopCar?.Invoke(trafficParticipant);
                }
            }
        }
    }

    private IEnumerator WaitForCheckFirstCar(GameObject trafficParticipant)
    {
        Debug.Log("Ожидание события CheckFirstCar...");

        // Ожидание события CheckFirstCar
        yield return new WaitUntil(() => OnCheckAnotherCar != null);

        // Вызываем событие CheckFirstCar
        OnCheckAnotherCar?.Invoke(trafficParticipant);

        // Ждем ответа от CheckFirstCar
        yield return new WaitUntil(() => CheckAnotherCarResponseReceived);

        Debug.Log("Получен ответ от CheckFirstCar. Теперь вызываем CheckSecondCar...");
    }

    private void OnCheckAnotherCarHandler(GameObject trafficParticipant)
    {
        GameObject otherTrafficParticipant = CarInSpawnPoint();
        Debug.Log(otherTrafficParticipant);

        if (otherTrafficParticipant != null && otherTrafficParticipant != trafficParticipant)
        {
            CarMovement trafficParticipantMovement = trafficParticipant.GetComponent<CarMovement>();
            CarMovement otherTrafficParticipantMovement = otherTrafficParticipant.GetComponent<CarMovement>();
            Debug.Log("Шо за фигня: " + TrafficRuleChecker.ByPointChecker(trafficParticipantMovement, otherTrafficParticipantMovement));
            if (TrafficRuleChecker.ByPointChecker(trafficParticipantMovement, otherTrafficParticipantMovement))
            {
                OnStopCar?.Invoke(trafficParticipant);
                OnHasObstacle?.Invoke(trafficParticipant);
            }

        }

        CarPoint3Checker.CheckAnotherCarResponseReceived = true;
        Debug.Log("CheckFirstCar обработан.");
    }


    // Переменные для отслеживания ответов
    public static bool CheckAnotherCarResponseReceived { get; set; }

    // **

    protected override void SubscribeToEvents()
    {
        Point1Checker.OnPointWay1CheckObstacle1Enter += OnPointWay1EnterHandler;
        Point1Checker.OnPointWay1CheckObstacleExit += OnPointWay1ExitHandler;
        Point1Checker.OnPointWay1CheckPriority1Enter += OnPointWay1CheckPriority1EnterHandler;
        Point1Checker.OnPointWay1CheckPriorityExit += OnPointWay1CheckPriorityExitHandler;
        CarPoint3Checker.OnCheckAnotherCar += OnCheckAnotherCarHandler;
    }



    protected override void UnsubscribeFromEvents()
    {
        Point1Checker.OnPointWay1CheckObstacle1Enter -= OnPointWay1EnterHandler;
        Point1Checker.OnPointWay1CheckObstacleExit -= OnPointWay1ExitHandler;
        Point1Checker.OnPointWay1CheckPriority1Enter -= OnPointWay1CheckPriority1EnterHandler;
        Point1Checker.OnPointWay1CheckPriorityExit -= OnPointWay1CheckPriorityExitHandler;
        CarPoint3Checker.OnCheckAnotherCar += OnCheckAnotherCarHandler;
    }
}
