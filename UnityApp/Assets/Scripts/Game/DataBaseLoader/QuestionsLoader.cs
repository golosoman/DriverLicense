using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class QuestionsLoader : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonPrefab;
    [SerializeField]
    private Transform content;
    [SerializeField]
    private SceneLoader sceneLoader;

    public delegate void QuestionsLoadedHandler();
    public event QuestionsLoadedHandler OnQuestionsLoaded;

    private string apiUrl = QuestionURL.ALL_QUESTION_URL;

    private void Start()
    {
        LoadQuestions();
    }

    private void LoadQuestions()
    {
        string bearerToken = GlobalState.userToken; // Получаем токен
        ApiHandler.SendGetRequest(apiUrl, this, OnQuestionsReceived, bearerToken);
    }

    private void OnQuestionsReceived(ApiResponse response)
    {
        if (response.StatusCode != 200)
        {
            Debug.LogError("Error loading questions: " + response.Body);
            return;
        }

        List<Question> questions = JsonUtility.FromJson<QuestionList>("{\"questions\":" + response.Body + "}").questions;

        foreach (Question question in questions)
        {
            CreateButton(question);
        }

        OnQuestionsLoaded?.Invoke();
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
        GlobalState.questionId = questionId;
        sceneLoader.LoadScene();
    }
}
