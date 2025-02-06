using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadUsersCollisionHandler : MonoBehaviour
{
    public delegate void CollisionWithRoadUser(string message);
    public static event CollisionWithRoadUser onCollisionWithRoadUser;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("Произошла колизия!");
        // Проверка столкновения с другим автомобилем
        if (collision.gameObject.CompareTag("Car"))
        {
            Debug.Log("Collision with another car detected!");
            // Логика для обработки столкновения, например, остановка автомобиля
            onCollisionWithRoadUser?.Invoke("Вопрос завершен неудачно из-за создания аварийной ситуации. Обнаружено столкновение с автомобилем.");
            StopRoadUser();
        }
        // Проверка столкновения с препятствием
        else if (collision.gameObject.CompareTag("Tram"))
        {
            Debug.Log("Collision with an tram detected!");
            onCollisionWithRoadUser?.Invoke("Вопрос завершен неудачно из-за создания аварийной ситуации. Обнаружено столкновение с трамваем.");
            // Логика для обработки столкновения с препятствием
            StopRoadUser();
        }
        else if (collision.gameObject.CompareTag("Human"))
        {
            Debug.Log("Collision with an human detected!");
            onCollisionWithRoadUser?.Invoke("Вопрос завершен неудачно из-за создания аварийной ситуации. Обнаружено столкновение с человеком.");
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
        else
        {
            Debug.LogError("RoadUserMovement is null. Cannot start movement.");
        }
    }
}
