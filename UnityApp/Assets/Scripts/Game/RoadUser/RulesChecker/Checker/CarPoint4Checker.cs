using System;
using System.Collections;
using UnityEngine;

public class CarPoint4Checker : CarPointHandler
{
    public delegate void StopCar(GameObject trafficParticipant);
    public static event StopCar OnStopCar; // Событие экземпляра

    public delegate void HasObstacle(GameObject trafficParticipant);
    public static event HasObstacle OnHasObstacle; // Событие экземпляра
    public delegate void CheckEvent(GameObject trafficParticipant);
    public static event CheckEvent OnCheckAnotherCar;

    private void OnPointWay4ExitHandler(GameObject trafficParticipant)
    {
        GameObject otherTrafficParticipant = CarInSpawnPoint();
        if (otherTrafficParticipant != null && otherTrafficParticipant != trafficParticipant && TrafficRuleChecker
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
        if (otherTrafficParticipant != null && otherTrafficParticipant != trafficParticipant && TrafficRuleChecker
            .CheckIntersectionWithAnotherRoadUser(trafficParticipant,
            otherTrafficParticipant, trafficParticipant.GetComponent<CarMovement>(),
            otherTrafficParticipant.GetComponent<CarMovement>()))
        {
            Debug.Log("Car is in the spawn point at point 3! Sending stopCar event.");
            OnStopCar?.Invoke(trafficParticipant);
        }
    }

    private void OnPointWay4CheckPriorityExitHandler(GameObject trafficParticipant)
    {
        HandlePriorityExit(trafficParticipant, 4);
    }

    private void OnPointWay4CheckPriority1EnterHandler(GameObject trafficParticipant)
    {
        HandlePriorityEnter(trafficParticipant, 4);
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

        CarPoint2Checker.CheckAnotherCarResponseReceived = true;
        Debug.Log("CheckFirstCar обработан.");
    }

    // ПРОЕЗД РЕГУЛИРУЕМЫХ ПЕРЕКРЕСТКОВ
    private void OnPaintWay4CheckTrafficLightPriorityEnterHandler(GameObject trafficParticipant)
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
                    // Debug.Log($"Car is in spawn point at point {pointNumber}! Sending stopCar event.");
                    OnStopCar?.Invoke(trafficParticipant);
                }
            }
        }
        else
        {
            OnStopCar?.Invoke(trafficParticipant);
            OnHasObstacle?.Invoke(trafficParticipant);
        }
    }

    private void OnPointWay4CheckTrafficLightPriorityExitHandler(GameObject trafficParticipant)
    {
        CarMovement trafficParticipantMovement = trafficParticipant.GetComponent<CarMovement>();

        if (trafficParticipantMovement.HasPriority)
        {
            GameObject otherTrafficParticipant = CarInSpawnPoint();
            // Debug.Log(otherTrafficParticipant);
            if (otherTrafficParticipant != null && otherTrafficParticipant != trafficParticipant)
            {
                CarMovement otherTrafficParticipantMovement = otherTrafficParticipant.GetComponent<CarMovement>();
                if (otherTrafficParticipantMovement.HasPriority && TrafficRuleChecker
                    .CheckIntersectionWithAnotherRoadUser(trafficParticipant, otherTrafficParticipant, trafficParticipantMovement, otherTrafficParticipantMovement))
                {
                    // Debug.Log($"Car is in spawn point at point {pointNumber}! Sending stopCar event.");
                    OnStopCar?.Invoke(trafficParticipant);
                    OnHasObstacle?.Invoke(trafficParticipant);
                }
            }
        }
        else
        {
            OnStopCar?.Invoke(trafficParticipant);
            OnHasObstacle?.Invoke(trafficParticipant);
        }
    }

    // Переменные для отслеживания ответов
    public static bool CheckAnotherCarResponseReceived { get; set; }

    protected override void SubscribeToEvents()
    {
        Point4Checker.OnPointWay4CheckObstacleEnter += OnPointWay4EnterHandler;
        Point4Checker.OnPointWay4CheckObstacleExit += OnPointWay4ExitHandler;
        Point4Checker.OnPointWay4CheckPriority1Enter += OnPointWay4CheckPriority1EnterHandler;
        Point4Checker.OnPointWay4CheckPriorityExit += OnPointWay4CheckPriorityExitHandler;
        Point4Checker.OnPaintWay4CheckTrafficLightPriorityEnter += OnPaintWay4CheckTrafficLightPriorityEnterHandler;
        Point4Checker.OnPointWay4CheckTrafficLightPriorityExit += OnPointWay4CheckTrafficLightPriorityExitHandler;
        CarPoint2Checker.OnCheckAnotherCar += OnCheckAnotherCarHandler;
    }

    protected override void UnsubscribeFromEvents()
    {
        Point4Checker.OnPointWay4CheckObstacleEnter -= OnPointWay4EnterHandler;
        Point4Checker.OnPointWay4CheckObstacleExit -= OnPointWay4ExitHandler;
        Point4Checker.OnPointWay4CheckPriority1Enter -= OnPointWay4CheckPriority1EnterHandler;
        Point4Checker.OnPointWay4CheckPriorityExit -= OnPointWay4CheckPriorityExitHandler;
        Point4Checker.OnPaintWay4CheckTrafficLightPriorityEnter -= OnPaintWay4CheckTrafficLightPriorityEnterHandler;
        Point4Checker.OnPointWay4CheckTrafficLightPriorityExit -= OnPointWay4CheckTrafficLightPriorityExitHandler;
        CarPoint2Checker.OnCheckAnotherCar -= OnCheckAnotherCarHandler;
    }
}
