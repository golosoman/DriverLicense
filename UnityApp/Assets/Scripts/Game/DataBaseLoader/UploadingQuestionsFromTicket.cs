using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UploadingQuestionsFromTicket : MonoBehaviour
{
    private CreateObjectManager createObjectManager;
    private int currentQuestionIndex = 0;
    private List<Question> questions; // Список вопросов
    private List<Answer> answers = new List<Answer>(); // Список ответов
    private int ticketId;
    private int incorrectAnswers = 0;

    private string apiUrl = "http://localhost:8080/api/tickets/random"; // URL для получения случайного билета

    public delegate void TicketCompletionHandler(StatisticRequest statisticRequest);
    public static event TicketCompletionHandler OnTicketCompleted;

    void Start()
    {
        createObjectManager = ScriptableObject.CreateInstance<CreateObjectManager>();
        LoadTickets();

        // Подписка на события успешного и неуспешного завершения
        GlobalManager.FinishSuccess += OnFinishSuccess;
        TimerController.FinishUnsuccessful += OnFinishUnsuccessful;
    }

    private void LoadTickets()
    {
        string bearerToken = GlobalState.userToken; // Получаем токен
        ApiHandler.SendGetRequest(apiUrl, this, OnTicketsReceived, bearerToken);
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
        ticketId = ticket.id; // Получаем ID билета

        DisplayNextQuestion(); // Отображаем первый вопрос
    }

    void DisplayNextQuestion()
    {
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
            ticketId = ticketId,
            result = overallResult,
            answers = answers
        };

        OnTicketCompleted?.Invoke(statisticRequest); // Генерируем событие завершения билета
    }

    private void OnDestroy()
    {
        // Отписка от событий при уничтожении объекта
        GlobalManager.FinishSuccess -= OnFinishSuccess;
        TimerController.FinishUnsuccessful -= OnFinishUnsuccessful;
    }
}
