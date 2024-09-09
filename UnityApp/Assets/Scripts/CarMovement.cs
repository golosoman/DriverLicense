using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public Transform[] route;
    private int currentPoint = 0;
    public float speed = 5f;
    public float rotationSpeed = 5f;
    private bool isMoving = false;

    public bool IsMoving => isMoving;

    public void SetRoute(Transform[] route, float speed)
    {
        this.route = route;
        this.speed = speed;
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
        speed = 0f;
        isMoving = false;
        Debug.Log("Car movement stopped.");
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
            float targetAngleZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Получаем текущий угол вращения
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngleZ+90);

            // Пока автомобиль не достиг следующей точки
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                // Плавно поворачиваем автомобиль по оси Z
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                
                // Перемещаем автомобиль к следующей точке
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

                yield return null;
            }

            currentPoint++;
            yield return null;
        }

        isMoving = false;
        Debug.Log("Car has finished the route.");
    }
}
