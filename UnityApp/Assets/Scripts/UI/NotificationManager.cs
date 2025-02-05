using System.Collections;
using UnityEngine;
using TMPro;

public class NotificationManager : MonoBehaviour
{
    [SerializeField]
    private GameObject errorNotificationPanel;
    [SerializeField]
    private TMP_Text errorNotificationText;
    [SerializeField]
    private GameObject trueNotificationPanel;
    [SerializeField]
    private TMP_Text trueNotificationText;

    private void Awake()
    {
        if (errorNotificationPanel != null)
            errorNotificationPanel.SetActive(false);

        if (trueNotificationPanel != null)
            trueNotificationPanel.SetActive(false);
    }

    private void OnEnable()
    {
        Authorization.OnErrorOccurred += ShowErrorNotification;
        Registration.OnErrorOccurred += ShowErrorNotification;
        TicketManager.OnErrorOccurred += ShowErrorNotification;
        InfoCollector.OnErrorOccurred += ShowErrorNotification;
        QuestionManager.OnErrorOccurred += ShowErrorNotification;
        QuestionManager.OnSuccessOccurred += ShowTrueNotification; // Подписка на новое событие
    }

    private void OnDisable()
    {
        Authorization.OnErrorOccurred -= ShowErrorNotification;
        Registration.OnErrorOccurred -= ShowErrorNotification;
        TicketManager.OnErrorOccurred -= ShowErrorNotification;
        InfoCollector.OnErrorOccurred -= ShowErrorNotification;
        QuestionManager.OnErrorOccurred -= ShowErrorNotification;
        QuestionManager.OnSuccessOccurred -= ShowTrueNotification; // Отписка от нового события
    }


    public void ShowErrorNotification(string message)
    {
        errorNotificationText.text = message;
        errorNotificationPanel.SetActive(true);
        StartCoroutine(HideErrorNotificationAfterDelay(3f));
    }

    public void ShowTrueNotification(string message)
    {
        trueNotificationText.text = message;
        trueNotificationPanel.SetActive(true);
        StartCoroutine(HideTrueNotificationAfterDelay(3f));
    }

    private IEnumerator HideErrorNotificationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        errorNotificationPanel.SetActive(false);
    }

    private IEnumerator HideTrueNotificationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        trueNotificationPanel.SetActive(false);
    }
}

