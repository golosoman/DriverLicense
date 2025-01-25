using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DropdownManager : MonoBehaviour
{
    public TMP_Dropdown dropdown; // Ссылка на Dropdown
    public GameObject buttonPrefab; // Префаб кнопки с TMP
    public Transform buttonContainer; // Контейнер для кнопок
    public int maxButtons = 5; // Максимальное количество кнопок
    public TMP_InputField ticketNameInput; // Поле ввода имени билета
    private TicketManager ticketManager; // Ссылка на TicketManager

    private List<string> selectedOptions = new List<string>(); // Список выбранных опций
    private List<int> selectedIds = new List<int>(); // Список ID выбранных вопросов
    private List<Question> questions; // Список всех вопросов
    private Dictionary<string, List<int>> questionDictionary = new Dictionary<string, List<int>>(); // Словарь для сопоставления текста вопроса с ID

    private void Start()
    {
        // Инициализация Dropdown
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        LoadQuestions(); // Запускаем загрузку вопросов
        ticketManager = GetComponent<TicketManager>(); // Получаем ссылку на TicketManager
    }

    private void LoadQuestions()
    {
        string apiUrl = QuestionURL.ALL_QUESTION_URL; // URL для получения вопросов
        string bearerToken = GlobalState.userToken; // Получаем токен

        ApiHandler.SendGetRequest(apiUrl, this, OnQuestionsReceived, bearerToken);
    }

    private void OnQuestionsReceived(ApiResponse response)
    {
        if (response.StatusCode != 200)
        {
            Debug.LogError("Ошибка загрузки вопросов: " + response.Body);
            return;
        }

        // Десериализация списка вопросов
        questions = JsonUtility.FromJson<QuestionList>("{\"questions\":" + response.Body + "}").questions;

        // Заполнение словаря
        foreach (var question in questions)
        {
            if (!questionDictionary.ContainsKey(question.question))
            {
                questionDictionary[question.question] = new List<int>();
            }
            questionDictionary[question.question].Add(question.id);
        }

        PopulateDropdown();
    }

    private void PopulateDropdown()
    {
        dropdown.ClearOptions();
        List<string> options = new List<string> { "Выберите вопрос" };

        foreach (var question in questions)
        {
            options.Add(question.question); // Добавляем каждую опцию в Dropdown
        }

        foreach (var option in selectedOptions)
        {
            options.Remove(option); // Удаляем уже выбранные опции
        }

        dropdown.AddOptions(options);
    }

    private void OnDropdownValueChanged(int index)
    {
        if (index == 0) return; // Игнорируем значение "Выберите вопрос"

        string selectedOption = dropdown.options[index].text;

        if (selectedOptions.Count < maxButtons)
        {
            // Получаем список ID для выбранного вопроса
            List<int> ids = questionDictionary[selectedOption];

            foreach (int id in ids)
            {
                if (!selectedIds.Contains(id)) // Проверяем, добавлен ли ID
                {
                    selectedOptions.Add(selectedOption);
                    selectedIds.Add(id); // Сохраняем ID выбранного вопроса
                    CreateButton(selectedOption);
                    break; // Выходим из цикла после добавления первого уникального ID
                }
            }

            PopulateDropdown(); // Обновляем Dropdown
        }

        dropdown.value = 0; // Сбрасываем Dropdown
    }

    private void CreateButton(string option)
    {
        GameObject buttonObject = Instantiate(buttonPrefab, buttonContainer);

        // Получаем компоненты из дочерних объектов
        Button button = buttonObject.GetComponentInChildren<Button>();
        TMP_Text optionText = buttonObject.GetComponentInChildren<TMP_Text>();

        // Устанавливаем текст опции в TMP
        Debug.Log(option);
        optionText.text = option;

        // Устанавливаем слушатель на клик кнопки
        button.onClick.AddListener(() => OnRemoveButtonClick(option, buttonObject));

        TMP_Text buttonMinusText = button.GetComponentInChildren<TMP_Text>();
        buttonMinusText.text = "-";
    }

    private void OnRemoveButtonClick(string option, GameObject buttonObject)
    {
        selectedOptions.Remove(option);
        // Получаем список ID для удаляемой опции
        List<int> idsToRemove = questionDictionary[option];

        foreach (int id in idsToRemove)
        {
            selectedIds.Remove(id); // Удаляем все ID, связанные с этой опцией
        }

        Destroy(buttonObject);
        PopulateDropdown(); // Обновляем Dropdown
    }

    public void OnCreateTicketButtonClicked()
    {
        string ticketName = ticketNameInput.text; // Получаем имя билета из input поля

        if (!string.IsNullOrEmpty(ticketName) && selectedIds.Count > 0) // Проверяем, что имя не пустое и есть выбранные вопросы
        {
            ticketManager.CreateTicket(ticketName, selectedIds); // Создаем билет
        }
        else
        {
            Debug.LogWarning("Введите имя билета и выберите хотя бы один вопрос.");
        }
    }

    public List<int> GetSelectedIds()
    {
        return selectedIds; // Возвращаем список выбранных ID
    }

    public List<string> GetSelectedOptions()
    {
        return selectedOptions; // Возвращаем список выбранных опций
    }
}