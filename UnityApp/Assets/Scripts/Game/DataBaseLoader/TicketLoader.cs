using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TicketLoader : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonPrefab; // Префаб кнопки
    [SerializeField]
    private Transform content; // Контейнер для кнопок (ScrollView)
    [SerializeField]
    private SceneLoader sceneLoader; // Ссылка на SceneLoader

    public delegate void TicketsLoadedHandler();
    public event TicketsLoadedHandler OnTicketsLoaded;

    private string apiUrl = TicketURL.ALL_TICKET_URL; // URL для получения всех билетов

    private void Start()
    {
        LoadTickets();
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

        TicketList ticketList = JsonUtility.FromJson<TicketList>("{\"tickets\":" + response.Body + "}");

        foreach (Ticket ticket in ticketList.tickets)
        {
            CreateButton(ticket);
        }

        OnTicketsLoaded?.Invoke();
    }

    private void CreateButton(Ticket ticket)
    {
        GameObject buttonObject = Instantiate(buttonPrefab, content);
        TMP_Text buttonText = buttonObject.GetComponentInChildren<TMP_Text>();
        buttonText.text = ticket.name; // Название билета

        Button button = buttonObject.GetComponent<Button>();
        button.onClick.AddListener(() => OnButtonClick(ticket.id));
    }

    private void OnButtonClick(int ticketId)
    {
        GlobalState.ticketId = ticketId;
        sceneLoader.LoadScene(); // Вызываем метод загрузки сцены
    }
}
