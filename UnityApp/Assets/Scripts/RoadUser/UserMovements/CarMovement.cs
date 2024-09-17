using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CarMovement : RoadUserMovement
{
    [SerializeField]
    private  float brakingFactor = 2f; // Фактор для экстренного торможения
    private void Start()
    {
        // turnIndicators = GetComponent<CarTurnIndicators>();
    }

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

    // public new void StartMovement()
    // {
    //     if (!IsMoving && Route != null && Route.Length > 0)
    //     {
    //         StartCoroutine(MoveAlongRoute());
    //     }
    //     else
    //     {
    //         Debug.LogError("Route is not set or is empty.");
    //     }
    // }

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

            // if (pointByPointRotation && IsMoving && CurrentPoint + 1 < Route.Length) // Проверяем, есть ли следующая точка
            // {
            //     // Проверяем, нужно ли останавливаться
            //     // Debug.Log(ShouldStopAtNextPoint(CurrentPoint + 1) + "da suda");
            //     if (ShouldStopAtNextPoint(CurrentPoint + 1))
            //     {
            //         if (!isStopped)
            //         {
            //             StopMovement();
            //         }
            //     }
            //     else
            //     {
            //         isStopped = false; // Сбрасываем флаг остановки
            //     }
            // }

            CurrentPoint++;
            yield return null;
        }

        IsMoving = false;
        Debug.Log("RoadUser has finished the route.");
    }


    // private bool ShouldStopAtNextPoint(int nextPointIndex)
    // {
    //     switch (RUD.MovementDirection)
    //     {
    //         case "forward":
    //             if(nextPointIndex == 1) return true;
    //             break;
    //         case "left":
    //             if (nextPointIndex == 1 || nextPointIndex == 2) return true;
    //             break;
    //         case "backward":
    //             if (nextPointIndex == 1 || nextPointIndex == 2 || nextPointIndex == 3 || nextPointIndex == 4 ) return true;
    //             break;
    //         default:
    //             break;
    //     }

    //     return false;
    // }

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
