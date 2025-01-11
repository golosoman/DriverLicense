using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Не забудьте добавить эту строку для использования TextMeshPro

public class DropdownManager : MonoBehaviour
{
    public TMP_Dropdown dropdown; // Ссылка на Dropdown
    public GameObject buttonPrefab; // Префаб кнопки с TMP
    public Transform buttonContainer; // Контейнер для кнопок
    public int maxButtons = 5; // Максимальное количество кнопок

    private List<string> selectedOptions = new List<string>(); // Список выбранных опций

    void Start()
    {
        // Инициализация Dropdown
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        PopulateDropdown();
    }

    void PopulateDropdown()
    {
        dropdown.ClearOptions();
        List<string> options = new List<string> { "Выберите вопрос" };
        options.AddRange(new List<string> { "Вопрос 1", "Вопрос 2", "Вопрос 3", "Вопрос 4", "Вопрос 5" });

        foreach (var option in selectedOptions)
        {
            options.Remove(option); // Удаляем уже выбранные опции
        }

        dropdown.AddOptions(options);
    }

    void OnDropdownValueChanged(int index)
    {
        if (index == 0) return; // Игнорируем значение "Выберите вопрос"

        string selectedOption = dropdown.options[index].text;

        if (selectedOptions.Count < maxButtons)
        {
            selectedOptions.Add(selectedOption);
            CreateButton(selectedOption);
            PopulateDropdown(); // Обновляем Dropdown
        }

        dropdown.value = 0; // Сбрасываем Dropdown
    }

    void CreateButton(string option)
    {
        GameObject buttonObject = Instantiate(buttonPrefab, buttonContainer);

        // Получаем компоненты из дочерних объектов
        Button button = buttonObject.GetComponentInChildren<Button>();
        TextMeshProUGUI optionText = buttonObject.GetComponentsInChildren<TextMeshProUGUI>()[1];

        // Устанавливаем текст опции в TMP
        optionText.text = option;

        // Устанавливаем слушатель на клик кнопки
        button.onClick.AddListener(() => OnRemoveButtonClick(option, buttonObject));

        TextMeshProUGUI buttonMinusText = button.GetComponentInChildren<TextMeshProUGUI>();
        buttonMinusText.text = "-";
    }

    void OnRemoveButtonClick(string option, GameObject buttonObject)
    {
        selectedOptions.Remove(option);
        Destroy(buttonObject);
        PopulateDropdown(); // Обновляем Dropdown
    }

    void Update()
    {
        // Скрываем или показываем Dropdown в зависимости от количества кнопок
        dropdown.gameObject.SetActive(selectedOptions.Count < maxButtons);
    }
}
