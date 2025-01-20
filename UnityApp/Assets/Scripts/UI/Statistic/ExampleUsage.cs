using UnityEngine;
using XCharts.Runtime;

public class CustomBarChart : MonoBehaviour
{
    [SerializeField]
    private BarChart barChart; // Ссылка на компонент BarChart

    private void Start()
    {
        if (barChart == null)
        {
            Debug.LogError("BarChart не назначен в инспекторе.");
            return;
        }

        // Настройка заголовка графика
        barChart.EnsureChartComponent<Title>().text = "Мой Бар График";
        barChart.EnsureChartComponent<Title>().subText = "Тестовые данные";

        // Удаляем предыдущие данные, если есть
        barChart.RemoveData();

        // Создаем серию
        var serie = barChart.AddSerie<Bar>("Тестовая серия");

        // Добавляем тестовые данные
        for (int i = 0; i < 10; i++)
        {
            barChart.AddXAxisData("хуясехуясехуясехуясехуясе" + (i + 1)); // Добавляем метки по оси X
            barChart.AddData(0, Random.Range(10, 100)); // Добавляем случайные данные в диапазоне от 10 до 100
        }

        // Обновляем график
        barChart.RefreshChart();
    }
}
