using UnityEngine;
using UnityEngine.UI;

public class Histogram2D : MonoBehaviour
{
    [SerializeField]
    private GameObject barPrefab; // Префаб для столбиков
    [SerializeField]
    private int numberOfBars = 20; // Максимальное количество столбиков
    [SerializeField]
    private float barWidth = 50f; // Ширина столбиков
    [SerializeField]
    private float maxHeight = 200f; // Максимальная высота столбиков

    public void CreateHistogram(float[] data)
    {
        // Очистка предыдущих баров
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Убедимся, что данные не превышают максимальное количество столбиков
        int barsCount = Mathf.Min(data.Length, numberOfBars);

        for (int i = 0; i < barsCount; i++)
        {
            GameObject bar = Instantiate(barPrefab, transform);
            RectTransform barRect = bar.GetComponent<RectTransform>();
            float height = Mathf.Clamp(data[i], 0, maxHeight); // Ограничиваем высоту
            barRect.sizeDelta = new Vector2(barWidth, height); // Установка высоты
            barRect.anchoredPosition = new Vector2(i * (barWidth + 10), height / 2); // Позиционирование
        }
    }
}
