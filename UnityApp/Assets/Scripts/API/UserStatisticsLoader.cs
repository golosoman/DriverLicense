using UnityEngine;
using UnityEngine.Networking;
using XCharts.Runtime;
using UnityEngine.UI;
using System.Collections;
using TMPro; // Для работы с UI элементами

public class UserStatisticsManager : MonoBehaviour
{
    [SerializeField]
    private BarChart barChart; // Ссылка на компонент BarChart

    [SerializeField]
    private GameObject buttonPrefab; // Префаб кнопки для билетов
    [SerializeField]
    private Transform scrollViewContent; // Контейнер для кнопок в ScrollView

    private void Start()
    {
        LoadUserStatistics();
    }

    private void LoadUserStatistics()
    {
        string bearerToken = GlobalState.userToken; // Получаем токен
        ApiHandler.SendGetRequest(StatisticURL.GET_STATISTIC_FOR_TRAINEE, this, OnUsertatisticsReceived, bearerToken);
    }

    private void OnUsertatisticsReceived(ApiResponse response)
    {
        if (response.StatusCode != 200)
        {
            Debug.LogError("Ошибка загрузки данных: " + response.Body);
            return;
        }

        // Десериализация JSON
        UserStatisticsResponse statistics = JsonUtility.FromJson<UserStatisticsResponse>(response.Body);

        // Обновление ScrollView с билетами
        UpdateScrollView(statistics.ticketStatistics);

        // Обновление гистограммы с категориями
        UpdateBarChart(statistics.categoryStatistics);
    }

    private void UpdateScrollView(TicketStatistic[] ticketStats)
    {
        // Очищаем предыдущие кнопки
        foreach (Transform child in scrollViewContent)
        {
            Destroy(child.gameObject);
        }

        foreach (var ticket in ticketStats)
        {
            GameObject button = Instantiate(buttonPrefab, scrollViewContent);
            button.GetComponentInChildren<TMP_Text>().text = $"{ticket.ticketName} - {ticket.GetStatus()} - Дата: {ticket.date}"; // Установка текста кнопки
            button.GetComponent<Button>().onClick.AddListener(() => OnTicketButtonClick(ticket)); // Добавление обработчика нажатия
        }
    }

    private void OnTicketButtonClick(TicketStatistic ticket)
    {
        Debug.Log($"Нажата кнопка для билета: {ticket.ticketName}, Статус: {ticket.GetStatus()}, Дата: {ticket.date}");
        // Здесь можно добавить дополнительную логику для обработки нажатия на кнопку
    }

    private void UpdateBarChart(CategoryStatistic[] categoryStats)
    {
        if (barChart == null)
        {
            Debug.LogError("BarChart не назначен в инспекторе.");
            return;
        }

        // Очищаем предыдущие данные
        barChart.RemoveData();

        // Создаем серию
        var serie = barChart.AddSerie<Bar>("Статистика по категориям");

        foreach (var category in categoryStats)
        {
            barChart.AddXAxisData(category.categoryName); // Добавляем метки по оси X
            barChart.AddData(0, category.percentage); // Добавляем процент в данные
        }

        // Обновляем график
        barChart.RefreshChart();
    }
}
