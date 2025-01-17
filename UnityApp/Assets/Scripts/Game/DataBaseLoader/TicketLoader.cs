using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

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
        StartCoroutine(LoadTickets());
    }

    private IEnumerator LoadTickets()
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
                TicketList ticketList = JsonUtility.FromJson<TicketList>("{\"tickets\":" + jsonResponse + "}");

                foreach (Ticket ticket in ticketList.tickets)
                {
                    CreateButton(ticket);
                }

                // После создания всех кнопок вызываем событие
                OnTicketsLoaded?.Invoke();
            }
        }
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
        // Используем SceneLoader для загрузки сцены
        sceneLoader.LoadScene(); // Вызываем метод загрузки сцены
    }
}
