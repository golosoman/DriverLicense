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
        UploadingQuestionsFromTicket.OnTicketCompletedSecond += CompleteHandler;
        DatabaseLoader.OnQuestionCompleted += CompleteHandler;
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
            TimerStop();
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
        timerText.text = "00:00";
        FinishUnsuccessful?.Invoke("Время вышло!"); // Вызов события
    }

    private void TimerStop()
    {
        CancelInvoke("UpdateTimer"); // Останавливаем обновление таймера
    }

    private void CompleteHandler(string message)
    {
        TimerStop();
    }

    private void OnDestroy()
    {
        UploadingQuestionsFromTicket.OnTicketCompletedSecond -= CompleteHandler;
    }
}
