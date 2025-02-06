using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UploadingQuestionsFromTicket : MonoBehaviour
{
    private CreateObjectManager createObjectManager;
    private int currentQuestionIndex = 0;
    private List<Question> questions; // Список вопросов
    private List<Answer> answers = new List<Answer>(); // Список ответов
    private Ticket ticketData;
    private int incorrectAnswers = 0;
    public delegate void TicketCompletionHandler(StatisticRequest statisticRequest);
    public static event TicketCompletionHandler OnTicketCompleted;

    public delegate void TicketCompletionSecondHandler(string message);
    public static event TicketCompletionSecondHandler OnTicketCompletedSecond;

    public delegate void CollisionHandler(string message);
    public static event CollisionHandler OnCollisionHandler;

    public delegate void ExplanationHandler(string explanation);
    public static event ExplanationHandler OnExplanationRequested;

    void Start()
    {
        createObjectManager = ScriptableObject.CreateInstance<CreateObjectManager>();
        LoadTickets();

        GlobalManager.FinishSuccess += OnFinishSuccess;
        TimerController.FinishUnsuccessful += OnFinishUnsuccessful;
        EventButton.OnShowExplanation += HandleShowExplanation;
        RoadUsersCollisionHandler.onCollisionWithRoadUser += OnCollision;
    }

    private void LoadTickets()
    {
        string bearerToken = GlobalState.userToken; // Получаем токен
        string url;
        if (GlobalState.ticketId != -1) url = TicketURL.GET_TICKET_BY_ID(GlobalState.ticketId);
        else url = TicketURL.RANDOM_TICKET_URL;

        ApiHandler.SendGetRequest(url, this, OnTicketsReceived, bearerToken);
    }

    private void OnCollision(string message)
    {
        OnCollisionHandler.Invoke(message);
        Debug.Log(message); // Обработка события завершения с ошибкой
        RecordAnswer(false);
        incorrectAnswers++;
        currentQuestionIndex++;
        DisplayNextQuestion(); // Переход к следующему вопросу
    }

    private void OnTicketsReceived(ApiResponse response)
    {
        if (response.StatusCode != 200)
        {
            Debug.LogError("Error loading tickets: " + response.Body);
            return;
        }

        Ticket ticket = JsonUtility.FromJson<Ticket>(response.Body);
        questions = ticket.questions; // Извлекаем вопросы из билета
        ticketData = ticket; // Получаем ID билета

        DisplayNextQuestion(); // Отображаем первый вопрос
    }

    void DisplayNextQuestion()
    {
        GlobalManager.carCount = 0;
        if (currentQuestionIndex < questions.Count)
        {
            createObjectManager.ProcessTicketData(questions[currentQuestionIndex]);
        }
        else
        {
            Debug.Log("Все вопросы пройдены!");
            CompleteTicket();
        }
    }

    private void OnFinishSuccess(string message)
    {
        Debug.Log(message); // Обработка события успешного завершения
        RecordAnswer(true);
        currentQuestionIndex++;
        DisplayNextQuestion(); // Переход к следующему вопросу
    }

    private void OnFinishUnsuccessful(string message)
    {
        Debug.Log(message); // Обработка события завершения с ошибкой
        RecordAnswer(false);
        incorrectAnswers++;
        currentQuestionIndex++;
        DisplayNextQuestion(); // Переход к следующему вопросу
    }

    private void RecordAnswer(bool result)
    {
        Answer answer = new Answer
        {
            questionId = questions[currentQuestionIndex].id,
            result = result
        };
        answers.Add(answer);
    }

    private void CompleteTicket()
    {
        bool overallResult = incorrectAnswers <= 2; // Определяем общий результат
        StatisticRequest statisticRequest = new StatisticRequest
        {
            ticketId = ticketData.id,
            result = overallResult,
            answers = answers
        };

        OnTicketCompleted?.Invoke(statisticRequest); // Генерируем событие завершения билета
        OnTicketCompletedSecond?.Invoke(statisticRequest.result ? "Поздравляем \nс успешной сдачей \nбилета!" : "Не расстраивайся, в\n следующий раз все \nобязательно получится!");
    }

    private void HandleShowExplanation(string message)
    {
        if (ticketData != null && !string.IsNullOrEmpty(questions[currentQuestionIndex].explanation))
        {
            OnExplanationRequested?.Invoke(questions[currentQuestionIndex].explanation);
        }
    }

    private void OnDestroy()
    {
        GlobalManager.FinishSuccess -= OnFinishSuccess;
        TimerController.FinishUnsuccessful -= OnFinishUnsuccessful;
        EventButton.OnShowExplanation -= HandleShowExplanation;
        RoadUsersCollisionHandler.onCollisionWithRoadUser -= OnCollision;

        GlobalState.ClearData();
    }
}
