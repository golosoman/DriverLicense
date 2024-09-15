using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : RoadUserMovement
{
    [SerializeField]
    private  float brakingFactor = 2f; // Фактор для экстренного торможения
    private CarTurnIndicators turnIndicators;

    private void Start()
    {
        turnIndicators = GetComponent<CarTurnIndicators>();
    }

    public void AdjustSpeed(float distanceToTarget)
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

    public void MoveTowardsTarget(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        float targetAngleZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;

        // Включение поворотника в зависимости от направления поворота
        // if (targetAngleZ > transform.eulerAngles.z + 10f)
        // {
        //     turnIndicators.TurnOnRightIndicator();
        // }
        // else if (targetAngleZ < transform.eulerAngles.z - 10f)
        // {
        //     turnIndicators.TurnOnLeftIndicator();
        // }

        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngleZ);

        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        AdjustSpeed(distanceToTarget);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, CurrentSpeed * Time.deltaTime);
    }
}
