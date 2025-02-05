using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoCollector : MonoBehaviour
{
    // Ссылки на UI элементы
    public TMP_InputField questionInputField; // Первое текстовое поле
    public TMP_InputField explanationInputField; // Второе текстовое поле
    public TMP_Dropdown categoryDropdown; // Выпадающее меню
    public SceneLoader sceneLoader; // Ссылка на SceneLoader

    public delegate void ErrorOccurred(string message);
    public static event ErrorOccurred OnErrorOccurred;

    // Метод для сбора информации и сохранения её в GlobalStateForConstructor
    public void CollectInfo()
    {
        // Получаем текст из первого текстового поля (вопрос)
        string question = questionInputField.text;

        // Получаем текст из второго текстового поля (описание)
        string explanation = explanationInputField.text;

        // Получаем выбранный элемент из выпадающего меню (категория)
        string categoryName = categoryDropdown.options[categoryDropdown.value].text;

        // Валидация данных
        if (!ValidateData(question, explanation, categoryName))
        {
            return;
        }

        // Сохраняем собранную информацию в GlobalStateForConstructor
        GlobalStateForConstructor.primaryInfoData = new PrimaryInfoData(question, explanation, categoryName);

        // Выводим информацию в консоль для проверки
        Debug.Log("Сохранённый вопрос: " + GlobalStateForConstructor.primaryInfoData.question);
        Debug.Log("Сохранённое описание: " + GlobalStateForConstructor.primaryInfoData.explanation);
        Debug.Log("Сохранённая категория: " + GlobalStateForConstructor.primaryInfoData.categoryName);

        // Загружаем следующую сцену
        sceneLoader.LoadScene();
    }

    private bool ValidateData(string question, string explanation, string categoryName)
    {
        if (string.IsNullOrEmpty(question) || question.Length < 6 || question.Length > 150)
        {
            OnErrorOccurred?.Invoke("Вопрос должен содержать от 6 до 150 символов.");
            return false;
        }

        if (string.IsNullOrEmpty(explanation) || explanation.Length < 20 || explanation.Length > 150)
        {
            OnErrorOccurred?.Invoke("Описание должно содержать от 20 до 150 символов.");
            return false;
        }

        if (string.IsNullOrEmpty(categoryName) || categoryName.Length < 5 || categoryName.Length > 30)
        {
            OnErrorOccurred?.Invoke("Категория должна содержать от 5 до 30 символов.");
            return false;
        }

        return true;
    }
}
