using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CarMovement : RoadUserMovement
{
    [SerializeField]
    private float brakingFactor = 2f; // Фактор для экстренного торможения
    private bool hasPriority = false;
    private float previousRotationAngle; // Предыдущий угол поворота
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
        previousRotationAngle = gameObject.transform.rotation.eulerAngles.z;
        while (IsMoving && CurrentPoint < Route.Length)
        {
            Vector2 targetPosition = Route[CurrentPoint].position;

            while (Vector2.Distance(transform.position, targetPosition) > 0.1f)
            {
                MoveTowardsTarget(targetPosition);
                yield return null;
            }
            // Debug.Log(CurrentPoint);
            CurrentPoint++;
            yield return null;
        }

        IsMoving = false;
        Debug.Log("RoadUser has finished the route.");
    }

    public bool HasTurned()
    {

        // Получаем текущий угол поворота
        float currentRotationAngle = gameObject.transform.rotation.eulerAngles.z;
        // Debug.Log(currentRotationAngle + "    " + previousRotationAngle);
        // Проверяем, превышает ли разница между предыдущим и текущим углом порог
        if (Mathf.Abs(currentRotationAngle - previousRotationAngle) > 5f)
        {
            // Если превышает, запоминаем текущий угол поворота
            previousRotationAngle = currentRotationAngle;
            return true; // Возвращаем true, если поворот произошел
        }

        return false; // Возвращаем false, если поворота не было
    }

    public override void MoveTowardsTarget(Vector2 targetPosition)
    {
        Vector2 direction = targetPosition - (Vector2)transform.position;
        float targetAngleZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;

        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngleZ);
        float distanceToTarget = Vector2.Distance((Vector2)transform.position, targetPosition);
        AdjustSpeed(distanceToTarget);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
        // Debug.Log(transform.rotation);
        transform.position = Vector2.MoveTowards((Vector2)transform.position, targetPosition, CurrentSpeed * Time.deltaTime);
    }
}
