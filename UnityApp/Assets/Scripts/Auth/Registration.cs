using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Registration : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField LoginField;
    [SerializeField]
    private TMP_InputField PasswordField;
    [SerializeField]
    private TMP_InputField RepeatPasswordField;
    [SerializeField]
    public SceneLoader sceneLoader; // Ссылка на SceneLoader

    [SerializeField]
    private GameObject errorPanel; // Панель для уведомления
    [SerializeField]
    private TMP_Text errorMessageText; // Текст уведомления

    private string errorMessage;

    public void CheckRegistration()
    {
        string login = LoginField.text;
        string password = PasswordField.text;
        string repeatPassword = RepeatPasswordField.text;

        // Проверяем, что поля не пустые
        if (InputValidator.IsNullOrEmpty(login))
        {
            errorMessage = "Логин введен неверно";
            Debug.LogError(errorMessage);
            ShowError(errorMessage);
            return; // Выходим из метода, если логин неверный
        }
        if (InputValidator.IsNullOrEmpty(password))
        {
            errorMessage = "Пароль введен неверно";
            Debug.LogError(errorMessage);
            ShowError(errorMessage);
            return; // Выходим из метода, если пароль неверный
        }

        // Проверяем на наличие кириллицы
        if (InputValidator.ContainsCyrillic(login))
        {
            errorMessage = "Логин не должен содержать кириллицу";
            Debug.LogError(errorMessage);
            ShowError(errorMessage);
            return; // Выходим из метода, если логин содержит кириллицу
        }
        if (InputValidator.ContainsCyrillic(password))
        {
            errorMessage = "Пароль не должен содержать кириллицу";
            Debug.LogError(errorMessage);
            ShowError(errorMessage);
            return; // Выходим из метода, если пароль содержит кириллицу
        }
        if (!InputValidator.ArePasswordsMatching(password, repeatPassword))
        {
            errorMessage = "Пароли не совпадают";
            Debug.LogError(errorMessage);
            ShowError(errorMessage);
            return; // Выходим из метода, если пароли не совпадают
        }

        SignupRequest requestData = new SignupRequest(login, password, repeatPassword);
        // Отправляем запрос на регистрацию
        string url = AuthURL.SIGN_UP;
        ApiHandler.SendPostRequest(url, requestData, this, HandleRegistrationResponse);
    }

    private void HandleRegistrationResponse(ApiResponse response)
    {
        Debug.Log("Status Code: " + response.StatusCode);
        Debug.Log("Response Body: " + response.Body);

        if (response.StatusCode == 200)
        {
            // Успешная регистрация
            Debug.Log("Регистрация прошла успешно!");
            AuthResponse authResponse = JsonUtility.FromJson<AuthResponse>(response.Body);
            GlobalState.userToken = authResponse.token;
            sceneLoader.LoadScene();
        }
        else
        {
            // Обработка ошибки регистрации
            string errorMsg = "Ошибка регистрации: " + response.Body;
            Debug.LogError(errorMsg);
            ShowError("Данные неверные. Ошибка 403."); // Сообщение для пользователя
        }
    }

    private void ShowError(string message)
    {
        errorMessageText.text = message; // Устанавливаем текст уведомления
        errorPanel.SetActive(true); // Показываем панель

        // Запускаем корутину для закрытия уведомления через 3 секунды
        StartCoroutine(HideErrorAfterDelay(3f));
    }

    private IEnumerator HideErrorAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        errorPanel.SetActive(false); // Скрываем панель
    }
}
