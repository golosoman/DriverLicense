using UnityEngine;
using XCharts.Runtime;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class StatisticsManager : MonoBehaviour
{
    [SerializeField]
    private BarChart barChart; // Ссылка на компонент BarChart
    [SerializeField]
    private GameObject buttonPrefab; // Префаб кнопки для вопросов
    [SerializeField]
    private Transform scrollViewContent; // Контейнер для кнопок в ScrollView

    private void Start()
    {
        GetStatistics();
    }

    private void GetStatistics()
    {
        string bearerToken = GlobalState.userToken; // Получаем токен
        ApiHandler.SendGetRequest(StatisticURL.GET_STATISTIC_FOR_ADMIN, this, OnStatisticsReceived, bearerToken);
    }

    private void OnStatisticsReceived(ApiResponse response)
    {
        if (response.StatusCode != 200)
        {
            Debug.LogError("Error loading statistics: " + response.Body);
            return;
        }

        AdminStatisticsResponse statistics = JsonUtility.FromJson<AdminStatisticsResponse>(response.Body);
        UpdateBarChart(statistics.ticketStatistics);
        UpdateScrollView(statistics.questionStatistics);
    }

    private void UpdateBarChart(TicketStatistics[] ticketStats)
    {
        if (barChart == null)
        {
            Debug.LogError("BarChart не назначен в инспекторе.");
            return;
        }

        barChart.RemoveData();
        var serie = barChart.AddSerie<Bar>("Статистика по билетам");

        foreach (var ticket in ticketStats)
        {
            barChart.AddXAxisData(ticket.ticketName);
            barChart.AddData(0, (float)ticket.percentage);
        }

        barChart.RefreshChart();
    }

    private void UpdateScrollView(QuestionStatistics[] questionStats)
    {
        foreach (var question in questionStats)
        {
            GameObject button = Instantiate(buttonPrefab, scrollViewContent);
            button.GetComponentInChildren<TMP_Text>().text = $"{question.question} - {question.percentage}%"; // Установка текста кнопки
            button.GetComponent<Button>().onClick.AddListener(() => OnQuestionButtonClick(question.id)); // Добавление обработчика нажатия
        }
    }

    private void OnQuestionButtonClick(long questionId)
    {
        Debug.Log($"Нажата кнопка для вопроса с ID: {questionId}");
        // Здесь можно добавить дополнительную логику для обработки нажатия на кнопку
    }
}
