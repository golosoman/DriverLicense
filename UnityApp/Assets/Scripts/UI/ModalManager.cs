using UnityEngine;
using UnityEngine.UI; // Не забудьте добавить этот using для работы с UI
using TMPro;

public class ModalManager : MonoBehaviour
{
    public GameObject modalPanel; // Ссылка на панель модального окна
    public TMP_Text messageText; // Ссылка на текстовое поле (или TextMeshProUGUI, если используете TextMeshPro)

    private void Start()
    {
        HideModal(); // Скрываем модальное окно при старте
        GlobalManager.Initialize(this); // Передаем ссылку на ModalManager
    }

    public void ShowModal(string message)
    {
        messageText.text = message; // Устанавливаем текст сообщения
        modalPanel.SetActive(true); // Показываем модальное окно
    }

    public void HideModal()
    {
        modalPanel.SetActive(false); // Скрываем модальное окно
    }
}
