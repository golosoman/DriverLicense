using UnityEngine;
using System;

public static class GlobalManager
{
    public static int carCount = 0;

    public static event Action<string> FinishSuccess; // Событие для успешного завершения

    public static void IncrementCarCount()
    {
        carCount++;
        Debug.Log(carCount);
    }

    public static void DecrementCarCount()
    {
        carCount--;
        Debug.Log(carCount);
        if (carCount <= 0)
        {
            // ShowSuccessModal();
            FinishSuccess?.Invoke("Поздравляем с успешной сдачей вопроса!"); // Вызов события
        }
    }
}
