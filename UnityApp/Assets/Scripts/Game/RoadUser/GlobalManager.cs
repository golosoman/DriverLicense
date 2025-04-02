using UnityEngine;
using System;

public static class GlobalManager
{
    public static int carCount = 0;
    public static int carHasPriorityCount = 0;

    public static event Action<string> FinishSuccess;
    public static event Action<string> OnReverseTrafficLight;

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
            FinishSuccess?.Invoke("Поздравляем с успешной сдачей вопроса!");
        }
    }

    public static void IncrementCarHasPriorityCount()
    {
        carHasPriorityCount++;
    }

    public static void DecrementCarHasPriorityCount()
    {
        carHasPriorityCount--;
        Debug.Log(carHasPriorityCount);
        if (carHasPriorityCount <= 0)
        {
            OnReverseTrafficLight?.Invoke("");
        }
    }
}
