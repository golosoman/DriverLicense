using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class QuestionsLoader : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonPrefab; // Префаб кнопки
    [SerializeField]
    private Transform content; // Контейнер для кнопок (ScrollView)
    [SerializeField]
    private SceneLoader sceneLoader; // Ссылка на SceneLoader
    public delegate void QuestionsLoadedHandler();
    public event QuestionsLoadedHandler OnQuestionsLoaded;
    private string apiUrl = QuestionURL.ALL_QUESTION_URL; // Замените на ваш URL

    private void Start()
    {
        StartCoroutine(LoadQuestions());
    }

    private IEnumerator LoadQuestions()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                string jsonResponse = webRequest.downloadHandler.text;
                List<Question> questions = JsonUtility.FromJson<QuestionList>("{\"questions\":" + jsonResponse + "}").questions;

                foreach (Question question in questions)
                {
                    CreateButton(question);
                }

                // После создания всех кнопок вызываем событие
                OnQuestionsLoaded?.Invoke();
            }
        }
    }

    private void CreateButton(Question question)
    {
        GameObject buttonObject = Instantiate(buttonPrefab, content);
        TMP_Text buttonText = buttonObject.GetComponentInChildren<TMP_Text>();
        buttonText.text = question.question;

        Button button = buttonObject.GetComponent<Button>();
        button.onClick.AddListener(() => OnButtonClick(question.id));
    }

    private void OnButtonClick(int questionId)
    {
        GlobalState.questionId = questionId; // Предполагается, что у вас есть класс GlobalState
        // Используем SceneLoader для загрузки сцены
        sceneLoader.LoadScene(); // Вызываем метод загрузки сцены
    }
}
