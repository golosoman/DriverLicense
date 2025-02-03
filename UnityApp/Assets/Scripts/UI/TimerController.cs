using UnityEngine;
using TMPro;
using System;

public class TimerController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text timerText; // Ссылка на компонент TextMeshPro
    [SerializeField]
    private float timeLeft = 120f; // 2 минуты в секундах

    public static event Action<string> FinishUnsuccessful; // Событие для завершения с ошибкой

    void Start()
    {
        UpdateTimerDisplay();
        InvokeRepeating("UpdateTimer", 1f, 1f); // Обновляем таймер каждую секунду
    }

    void UpdateTimer()
    {
        if (timeLeft > 0)
        {
            timeLeft--;
            UpdateTimerDisplay();
        }
        else
        {
            TimerExpired();
            CancelInvoke("UpdateTimer"); // Останавливаем обновление таймера
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void TimerExpired()
    {
        Debug.Log("Время вышло!");
        timerText.text = "Время вышло!";
        FinishUnsuccessful?.Invoke("Время вышло!"); // Вызов события
    }
}
