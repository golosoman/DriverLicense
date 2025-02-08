using System;
using System.Collections;
using UnityEngine;

public class CarPoint2Checker : CarPointHandler
{
    public delegate void StopCar(GameObject trafficParticipant);
    public static event StopCar OnStopCar; // Событие экземпляра

    public delegate void HasObstacle(GameObject trafficParticipant);
    public static event HasObstacle OnHasObstacle; // Событие экземпляра
    public delegate void CheckEvent(GameObject trafficParticipant);
    public static event CheckEvent OnCheckAnotherCar;

    private void OnPointWay2ExitHandler(GameObject trafficParticipant)
    {
        GameObject otherTrafficParticipant = CarInSpawnPoint();
        if (otherTrafficParticipant != null && otherTrafficParticipant != trafficParticipant && TrafficRuleChecker
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
        if (otherTrafficParticipant != null && otherTrafficParticipant != trafficParticipant && TrafficRuleChecker
            .CheckIntersectionWithAnotherRoadUser(trafficParticipant,
            otherTrafficParticipant, trafficParticipant.GetComponent<CarMovement>(),
            otherTrafficParticipant.GetComponent<CarMovement>()))
        {
            Debug.Log("Car is in the spawn point at point 2! Sending stopCar event.");
            OnStopCar?.Invoke(trafficParticipant);
        }
    }

    private void OnPointWay2CheckPriorityExitHandler(GameObject trafficParticipant)
    {
        HandlePriorityExit(trafficParticipant, 2);
    }

    private void OnPointWay2CheckPriority1EnterHandler(GameObject trafficParticipant)
    {
        HandlePriorityEnter(trafficParticipant, 2);
    }

    private void HandlePriorityExit(GameObject trafficParticipant, int pointNumber = -1)
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

        CarPoint4Checker.CheckAnotherCarResponseReceived = true;
        Debug.Log("CheckFirstCar обработан.");
    }

    // Переменные для отслеживания ответов
    public static bool CheckAnotherCarResponseReceived { get; set; }

    protected override void SubscribeToEvents()
    {
        Point2Checker.OnPointWay2CheckObstacleEnter += OnPointWay2EnterHandler;
        Point2Checker.OnPointWay2CheckObstacleExit += OnPointWay2ExitHandler;
        Point2Checker.OnPointWay2CheckPriority1Enter += OnPointWay2CheckPriority1EnterHandler;
        Point2Checker.OnPointWay2CheckPriorityExit += OnPointWay2CheckPriorityExitHandler;
        CarPoint4Checker.OnCheckAnotherCar += OnCheckAnotherCarHandler;
    }

    protected override void UnsubscribeFromEvents()
    {
        Point2Checker.OnPointWay2CheckObstacleEnter -= OnPointWay2EnterHandler;
        Point2Checker.OnPointWay2CheckObstacleExit -= OnPointWay2ExitHandler;
        Point2Checker.OnPointWay2CheckPriority1Enter -= OnPointWay2CheckPriority1EnterHandler;
        Point2Checker.OnPointWay2CheckPriorityExit -= OnPointWay2CheckPriorityExitHandler;
        CarPoint4Checker.OnCheckAnotherCar -= OnCheckAnotherCarHandler;
    }
}
