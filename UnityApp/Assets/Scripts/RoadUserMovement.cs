using System.Collections;
using System.Collections.Generic;
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
    private int currentPoint = 0;
    private bool isMoving = false;
    private float currentSpeed = 0f; // Текущая скорость автомобиля

    public bool IsMoving => isMoving;

    public void SetRoute(Transform[] route)
    {
        this.route = route;
    }

    public int GetLengthRoute(){
        return route.Length;
    }

    public void StartMovement()
    {
        if (!isMoving && route != null && route.Length > 0)
        {
            StartCoroutine(MoveAlongRoute());
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

    IEnumerator MoveAlongRoute()
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
            
            // Получаем направление к следующей точке
            Vector3 direction = targetPosition - transform.position;

            // Рассчитываем угол поворота только по оси Z
            float targetAngleZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;

            // Получаем текущий угол вращения
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngleZ);

            // Пока автомобиль не достиг следующей точки
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                // Если автомобиль находится вблизи следующей точки, замедляем его перед поворотом
                float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
                if (distanceToTarget < decelerationStartDistance)
                {
                    currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed * decelerationFactor, Time.deltaTime * acceleration);
                }
                else
                {
                    // Постепенное увеличение скорости до максимальной
                    currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, Time.deltaTime * acceleration);
                }

                // Плавно поворачиваем автомобиль по оси Z
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                
                // Перемещаем автомобиль к следующей точке с текущей скоростью
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, currentSpeed * Time.deltaTime);

                yield return null;
            }

            currentPoint++;
            yield return null;
        }

        isMoving = false;
        Debug.Log("RoadUser has finished the route.");
    }
}
