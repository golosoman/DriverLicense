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
        UploadingQuestionsFromTicket.OnTicketCompletedSecond += ShowFinishModal;
        UploadingQuestionsFromTicket.OnCollisionHandler += ShowExplanationModal;
        // UploadingQuestionsFromTicket.OnObstacleHanlder += ShowExplanationModal;

        EventButton.OnShowExitModal += ShowExitModal;
        CheckerHandler.OnHasObstacle += ShowExplanationModal;

        DatabaseLoader.OnCollisionHandler += ShowFinishModal;
        DatabaseLoader.OnQuestionCompleted += ShowFinishModal;
        DatabaseLoader.OnTimerStopHandler += ShowFinishModal;
        DatabaseLoader.OnExplanationRequested += ShowExplanationModal;
        // DatabaseLoader.OnObstacleHanlder += ShowFinishModal;
    }

    private void OnDisable()
    {
        UploadingQuestionsFromTicket.OnExplanationRequested -= ShowExplanationModal;
        UploadingQuestionsFromTicket.OnTicketCompletedSecond -= ShowFinishModal;
        EventButton.OnShowExitModal -= ShowExitModal;

        DatabaseLoader.OnCollisionHandler -= ShowFinishModal;
        DatabaseLoader.OnQuestionCompleted -= ShowFinishModal;
        DatabaseLoader.OnTimerStopHandler -= ShowFinishModal;
        DatabaseLoader.OnExplanationRequested -= ShowExplanationModal;
    }

    public void ShowFinishModal(string message)
    {
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
