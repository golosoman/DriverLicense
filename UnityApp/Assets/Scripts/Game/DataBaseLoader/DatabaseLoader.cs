using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System;

public class DatabaseLoader : MonoBehaviour
{
    private CreateObjectManager createObjectManager;
    public delegate void CollisionHandler(string message);
    public static event CollisionHandler OnCollisionHandler;

    public delegate void QuestionCompletionHandler(string message);
    public static event QuestionCompletionHandler OnQuestionCompleted;

    public delegate void TimerStopHandler(string message);
    public static event TimerStopHandler OnTimerStopHandler;

    public delegate void ExplanationHandler(string explanation);
    public static event ExplanationHandler OnExplanationRequested;

    public delegate void ObstacleHandler(string message);
    public static event ObstacleHandler OnObstacleHanlder;

    private Question question;
    // private TrafficRulesManager trafficRulesManager;

    void Start()
    {
        createObjectManager = ScriptableObject.CreateInstance<CreateObjectManager>();
        RoadUsersCollisionHandler.onCollisionWithRoadUser += OnCollision;
        GlobalManager.FinishSuccess += OnFinishSuccess;
        TimerController.FinishUnsuccessful += OnFinishUnsuccessful;
        EventButton.OnShowExplanation += HandleShowExplanation;
        TrafficRuleEnforcer.OnObstacleHanlder += HandleObstacleHandler;
        StartCoroutine(LoadTicketData());
    }

    private void HandleObstacleHandler(string message)
    {
        OnObstacleHanlder.Invoke(message);
        Debug.Log(message); // Обработка события завершения с ошибкой
    }

    private void OnCollision(string message)
    {
        OnCollisionHandler.Invoke(message);
        Debug.Log(message); // Обработка события завершения с ошибкой
    }

    private void OnFinishSuccess(string message)
    {
        Debug.Log(message); // Обработка события успешного завершения
        OnQuestionCompleted?.Invoke(message);
    }

    private void OnFinishUnsuccessful(string message)
    {
        Debug.Log(message); // Обработка события завершения с ошибкой
        OnTimerStopHandler?.Invoke("Вопрос завершен неудачно, т.к. время его выполнения вышло.");
    }

    private void HandleShowExplanation(string message)
    {
        if (question != null && !string.IsNullOrEmpty(question.explanation))
        {
            OnExplanationRequested?.Invoke(question.explanation);
        }
    }

    IEnumerator LoadTicketData()
    {
        string url;
        if (GlobalState.questionId != -1) url = QuestionURL.GET_QUESTION_BY_ID(GlobalState.questionId);
        else url = QuestionURL.RANDOM_QESTION_URL;

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonData = request.downloadHandler.text;
            Debug.Log(jsonData);
            try
            {
                question = JsonConvert.DeserializeObject<Question>(jsonData);
                Debug.Log(question);
                // Debug.Log(ticketData.Id + " " + ticketData.IntersectionType + " " + ticketData.Question + " " + ticketData.Explanation + " " + ticketData.SignsArr.ToString() + " " + ticketData.TrafficLightsArr.ToString() + " " + ticketData.TrafficParticipantsArr.ToString() + " ");
                createObjectManager.ProcessTicketData(question);
            }
            catch (JsonException ex)
            {
                Debug.LogError("Failed to parse JSON data: " + ex.Message);
            }
        }
        else
        {
            Debug.LogError("Failed to load ticket data: " + request.error);
        }
    }

    private void OnDestroy()
    {
        RoadUsersCollisionHandler.onCollisionWithRoadUser -= OnCollision;
        GlobalManager.FinishSuccess -= OnFinishSuccess;
        TimerController.FinishUnsuccessful -= OnFinishUnsuccessful;
        EventButton.OnShowExplanation -= HandleShowExplanation;
        TrafficRuleEnforcer.OnObstacleHanlder -= HandleObstacleHandler;
        GlobalState.ClearData();
    }
}




