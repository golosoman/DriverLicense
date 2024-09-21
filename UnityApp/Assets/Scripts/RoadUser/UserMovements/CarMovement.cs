using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CarMovement : RoadUserMovement
{
    [SerializeField]
    private  float brakingFactor = 2f; // Фактор для экстренного торможения
    private bool hasPriority = false;
    public bool HasPriority { get => hasPriority; set => hasPriority = value; }


    public override void AdjustSpeed(float distanceToTarget)
    {
        if (distanceToTarget < DecelerationFactor)
        {
            // Экстренное торможение, если скорость высокая
            if (CurrentSpeed > MaxSpeed / 2)
                CurrentSpeed = Mathf.Lerp(CurrentSpeed, 0, Time.deltaTime * Acceleration * brakingFactor);
            else
                CurrentSpeed = Mathf.Lerp(CurrentSpeed, MaxSpeed * DecelerationFactor, Time.deltaTime * Acceleration);
        }
        else
        {
            CurrentSpeed = Mathf.Lerp(CurrentSpeed, MaxSpeed, Time.deltaTime * Acceleration);
        }
    }

    public override IEnumerator MoveAlongRoute()
    {
        if (Route == null || Route.Length == 0)
        {
            Debug.LogError("Route is not set or empty.");
            yield break;
        }

        IsMoving = true;

        while (IsMoving && CurrentPoint < Route.Length)
        {
            Vector3 targetPosition = Route[CurrentPoint].position;

            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                MoveTowardsTarget(targetPosition);
                yield return null;
            }

            CurrentPoint++;
            yield return null;
        }

        IsMoving = false;
        Debug.Log("RoadUser has finished the route.");
    }


    public override void MoveTowardsTarget(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        float targetAngleZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;

        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngleZ);

        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        AdjustSpeed(distanceToTarget);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, CurrentSpeed * Time.deltaTime);
    }
}
