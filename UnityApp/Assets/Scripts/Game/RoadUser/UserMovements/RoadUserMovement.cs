using System.Collections;
using UnityEngine;

public class RoadUserMovement : MonoBehaviour
{
    [SerializeField]
    private float maxSpeed = 10f; // Максимальная скорость
    [SerializeField]
    private float acceleration = 1f; // Ускорение автомобиля
    [SerializeField]
    private float decelerationFactor = 0.5f; // Фактор замедления перед поворотом
    [SerializeField]
    private float rotationSpeed = 5f; // Скорость поворота
    [SerializeField]
    private float decelerationStartDistance = 2f; // Расстояние, на котором автомобиль начнет замедляться

    private Transform[] route;
    private TrafficParticipantData roadUserData;
    private int currentPoint = 0;
    private bool isMoving = false;
    private float currentSpeed = 0f; // Текущая скорость автомобиля

    public TrafficParticipantData RUD { get => roadUserData; set => roadUserData = value; }
    public Transform[] Route { get => route; set => route = value; }
    public bool IsMoving { get => isMoving; set => isMoving = value; }
    public int CurrentPoint { get => currentPoint; set => currentPoint = value; }
    public float Acceleration => acceleration;
    public float RotationSpeed => rotationSpeed;
    public float CurrentSpeed { get => currentSpeed; set => currentSpeed = value; }
    public float MaxSpeed { get => maxSpeed; set => maxSpeed = value; }
    public float DecelerationFactor { get => decelerationFactor; set => decelerationFactor = value; }
    public float DecelerationStartDistance => decelerationStartDistance;

    public int GetLengthRoute() => route.Length;

    public void StartMovement()
    {
        if (route != null && route.Length > 0)
        {
            if (!isMoving) { StartCoroutine(MoveAlongRoute()); }

        }
        else
        {
            Debug.LogError("Route is not set or is empty.");
        }
    }

    public void StopMovement()
    {
        StopAllCoroutines();
        currentSpeed = 0f;
        isMoving = false;
        Debug.Log("RoadUser movement stopped.");
    }

    public virtual IEnumerator MoveAlongRoute()
    {
        if (route == null || route.Length == 0)
        {
            Debug.LogError("Route is not set or empty.");
            yield break;
        }

        isMoving = true;

        while (isMoving && currentPoint < route.Length)
        {
            Vector3 targetPosition = route[currentPoint].position;

            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                MoveTowardsTarget(targetPosition);
                yield return null;
            }

            currentPoint++;
            yield return null;
        }

        isMoving = false;
        Debug.Log("RoadUser has finished the route.");
    }

    public virtual void MoveTowardsTarget(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        float targetAngleZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngleZ);

        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        AdjustSpeed(distanceToTarget);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, currentSpeed * Time.deltaTime);
    }

    public virtual void AdjustSpeed(float distanceToTarget)
    {
        if (distanceToTarget < decelerationStartDistance)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed * decelerationFactor, Time.deltaTime * acceleration);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, Time.deltaTime * acceleration);
        }
    }
}