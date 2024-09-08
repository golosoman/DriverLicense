using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollisionHandler : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Произошла колизия!");
        // Проверка столкновения с другим автомобилем
        if (collision.gameObject.CompareTag("Car"))
        {
            Debug.Log("Collision with another car detected!");
            // Логика для обработки столкновения, например, остановка автомобиля
            StopCar();
        }
        // Проверка столкновения с препятствием
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Collision with an obstacle detected!");
            // Логика для обработки столкновения с препятствием
            StopCar();
        }
    }

    private void StopCar()
    {
        CarMovement carMovement = GetComponent<CarMovement>();
        if (carMovement != null)
        {
            carMovement.StopMovement();
        }
        else {
            Debug.LogError("CarMovement is null. Cannot start movement.");
        }
    }
}
