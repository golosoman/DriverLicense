using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text timerText; // Ссылка на компонент TextMeshPro
    [SerializeField]
    private float timeLeft = 120f; // 2 минуты в секундах

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
        // Здесь можно добавить любое событие по истечении таймера
        Debug.Log("Время вышло!");
        // Например, можно отобразить сообщение на экране
        timerText.text = "Время вышло!";
    }
}
