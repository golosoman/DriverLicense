using System.Collections;
using UnityEngine;
using TMPro;

public class ModalManager : MonoBehaviour
{
    [SerializeField]
    private GameObject finishModalPanel;
    [SerializeField]
    private TMP_Text finishModalText;
    [SerializeField]
    private GameObject explanationModalPanel;
    [SerializeField]
    private TMP_Text explanationModalText;
    [SerializeField]
    private GameObject exitModalPanel;
    [SerializeField]
    private TMP_Text exitModalText;

    private void Awake()
    {
        if (finishModalPanel != null)
            finishModalPanel.SetActive(false);

        if (explanationModalPanel != null)
            explanationModalPanel.SetActive(false);

        if (exitModalPanel != null)
            exitModalPanel.SetActive(false);
    }

    private void OnEnable()
    {
        UploadingQuestionsFromTicket.OnExplanationRequested += ShowExplanationModal;
        UploadingQuestionsFromTicket.OnTicketCompleted += ShowFinishModal;
        EventButton.OnShowExitModal += ShowExitModal;
    }

    private void OnDisable()
    {
        UploadingQuestionsFromTicket.OnExplanationRequested -= ShowExplanationModal;
        UploadingQuestionsFromTicket.OnTicketCompleted -= ShowFinishModal;
        EventButton.OnShowExitModal -= ShowExitModal;
    }

    public void ShowFinishModal(StatisticRequest statistic)
    {
        string message;
        if (statistic.result) message = "Поздравляем \nс успешной сдачей \nбилета!";
        else message = "Не расстраивайся, в\n следующий раз все \nобязательно получится!";
        ShowModal(finishModalPanel, finishModalText, message);
    }

    public void ShowExplanationModal(string message)
    {
        ShowModal(explanationModalPanel, explanationModalText, message);
    }


    public void ShowExitModal(string message)
    {
        ShowModal(exitModalPanel, exitModalText, message);
    }

    private void ShowModal(GameObject panel, TMP_Text textComponent, string message)
    {
        textComponent.text = message;
        panel.SetActive(true);
    }

    public void HideFinishModal()
    {
        HideModal(finishModalPanel);
    }

    public void HideExplanationModal()
    {
        HideModal(explanationModalPanel);
    }

    public void HideExitModal()
    {
        HideModal(exitModalPanel);
    }

    private void HideModal(GameObject panel)
    {
        panel.SetActive(false);
    }
}
