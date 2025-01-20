using UnityEngine;
using UnityEngine.UI;

public class ButtonActivController : MonoBehaviour
{
    public Button button1; // Первая кнопка
    public Button button2; // Вторая кнопка
    public GameObject panel1; // Первая панель
    public GameObject panel2; // Вторая панель

    private Color activeColor;
    private Color inactiveColor;

    void Start()
    {
        // Устанавливаем цвета в формате HEX с 80% прозрачности (альфа = 204)
        activeColor = new Color32(191, 189, 171, 204); // Цвет активной кнопки
        inactiveColor = new Color32(156, 156, 136, 204); // Цвет неактивной кнопки

        // Установите первую кнопку активной по умолчанию
        SetActiveButton(button1);

        // Добавляем обработчики событий на кнопки
        button1.onClick.AddListener(() => SetActiveButton(button1));
        button2.onClick.AddListener(() => SetActiveButton(button2));
    }

    private void SetActiveButton(Button activeButton)
    {
        // Устанавливаем активную кнопку
        activeButton.GetComponent<Image>().color = activeColor;

        // Устанавливаем неактивную кнопку
        Button inactiveButton = (activeButton == button1) ? button2 : button1;
        inactiveButton.GetComponent<Image>().color = inactiveColor;

        // Отображаем соответствующую панель
        if (activeButton == button1)
        {
            panel1.SetActive(true);
            panel2.SetActive(false);
        }
        else
        {
            panel1.SetActive(false);
            panel2.SetActive(true);
        }
    }

    private Color HexToColor(string hex)
    {
        // Удаляем символ "#" если он есть
        hex = hex.Replace("#", "");

        // Преобразуем HEX в RGB
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

        return new Color32(r, g, b, 255); // Создаем цвет с полной непрозрачностью
    }
}
