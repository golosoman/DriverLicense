using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class SearchFilter : MonoBehaviour
{
    public TMP_InputField searchInput; // Поле для ввода текста
    public GameObject content; // Контейнер для кнопок
    private List<GameObject> buttons = new List<GameObject>(); // Список всех кнопок

    void Start()
    {
        QuestionsLoader questionsLoader = FindObjectOfType<QuestionsLoader>();

        if (questionsLoader != null)
        {
            questionsLoader.OnQuestionsLoaded += RefreshButtonList;
        }

        // Заполняем список кнопок из дочерних объектов в content
        RefreshButtonList();

        // Добавляем слушателя для InputField
        searchInput.onValueChanged.AddListener(OnSearchValueChanged);
    }

    void RefreshButtonList()
    {
        // Очищаем список перед добавлением
        buttons.Clear();

        // Находим все дочерние объекты в content и добавляем их в список
        foreach (Transform child in content.transform)
        {
            // Проверяем, является ли дочерний объект кнопкой
            if (child.GetComponent<Button>() != null)
            {
                buttons.Add(child.gameObject);
            }
        }
    }

    void OnSearchValueChanged(string searchText)
    {
        // Применяем фильтрацию на основе текста поиска
        FilterButtons(searchText);
    }

    void FilterButtons(string searchText)
    {
        // Приводим текст к нижнему регистру для нечувствительности к регистру
        string lowerCaseSearchText = searchText.ToLower();

        // Проходим по всем кнопкам и проверяем текст
        foreach (GameObject button in buttons)
        {
            // Получаем текст кнопки
            Transform textInButton = button.transform.Find("Text (TMP)");

            if (textInButton != null)
            {
                TextMeshProUGUI tmpText = textInButton.GetComponent<TextMeshProUGUI>();
                if (tmpText != null)
                {
                    string buttonText = tmpText.text.ToLower(); // Получаем текст и приводим к нижнему регистру
                    button.SetActive(buttonText.Contains(lowerCaseSearchText));
                }
            }
        }
    }
}
