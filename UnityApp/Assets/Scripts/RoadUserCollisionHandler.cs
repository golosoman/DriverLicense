using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadUsersCollisionHandler : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("Произошла колизия!");
        // Проверка столкновения с другим автомобилем
        if (collision.gameObject.CompareTag("Car"))
        {
            Debug.Log("Collision with another car detected!");
            // Логика для обработки столкновения, например, остановка автомобиля
            StopRoadUser();
        }
        // Проверка столкновения с препятствием
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Collision with an obstacle detected!");
            // Логика для обработки столкновения с препятствием
            StopRoadUser();
        }
    }

    private void StopRoadUser()
    {
        RoadUserMovement roadUserMovement = GetComponent<RoadUserMovement>();
        if (roadUserMovement != null)
        {
            roadUserMovement.StopMovement();
        }
        else {
            Debug.LogError("RoadUserMovement is null. Cannot start movement.");
        }
    }
}
