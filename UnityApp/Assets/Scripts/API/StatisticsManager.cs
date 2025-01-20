using UnityEngine;
using UnityEngine.Networking;
using XCharts.Runtime;
using UnityEngine.UI;
using System.Collections;
using TMPro; // Для работы с UI элементами

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
        StartCoroutine(GetStatistics());
    }

    private IEnumerator GetStatistics()
    {
        string bearerToken = GlobalState.userToken; // Замените на ваш токен

        using (UnityWebRequest webRequest = UnityWebRequest.Get(StatisticURL.GET_STATISTIC_FOR_ADMIN))
        {
            // Добавляем заголовок Authorization
            webRequest.SetRequestHeader("Authorization", "Bearer " + bearerToken);

            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Ошибка загрузки данных: " + webRequest.error);
            }
            else
            {
                // Десериализация JSON
                string jsonResponse = webRequest.downloadHandler.text;
                AdminStatisticsResponse statistics = JsonUtility.FromJson<AdminStatisticsResponse>(jsonResponse);

                // Обновление гистограммы
                UpdateBarChart(statistics.ticketStatistics);

                // Обновление ScrollView
                UpdateScrollView(statistics.questionStatistics);
            }
        }
    }

    private void UpdateBarChart(TicketStatistics[] ticketStats)
    {
        if (barChart == null)
        {
            Debug.LogError("BarChart не назначен в инспекторе.");
            return;
        }

        // Очищаем предыдущие данные
        barChart.RemoveData();

        // Создаем серию
        var serie = barChart.AddSerie<Bar>("Статистика по билетам");

        foreach (var ticket in ticketStats)
        {
            barChart.AddXAxisData(ticket.ticketName); // Добавляем метки по оси X
            barChart.AddData(0, (float)ticket.percentage); // Добавляем процент в данные
        }

        // Обновляем график
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
