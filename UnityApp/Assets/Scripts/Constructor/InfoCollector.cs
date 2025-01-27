
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

    // Метод для сбора информации и сохранения её в GlobalStateForConstructor
    public void CollectInfo()
    {
        // Получаем текст из первого текстового поля (вопрос)
        string question = questionInputField.text;

        // Получаем текст из второго текстового поля (описание)
        string explanation = explanationInputField.text;

        // Получаем выбранный элемент из выпадающего меню (категория)
        string categoryName = categoryDropdown.options[categoryDropdown.value].text;

        // Сохраняем собранную информацию в GlobalStateForConstructor
        GlobalStateForConstructor.primaryInfoData = new PrimaryInfoData(question, explanation, categoryName);

        // Выводим информацию в консоль для проверки
        Debug.Log("Сохранённый вопрос: " + GlobalStateForConstructor.primaryInfoData.question);
        Debug.Log("Сохранённое описание: " + GlobalStateForConstructor.primaryInfoData.explanation);
        Debug.Log("Сохранённая категория: " + GlobalStateForConstructor.primaryInfoData.categoryName);

        // Загружаем следующую сцену
        sceneLoader.LoadScene();
    }
}