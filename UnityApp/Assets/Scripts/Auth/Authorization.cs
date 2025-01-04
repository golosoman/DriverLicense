using System.Collections;
using UnityEngine;
using TMPro;

public class Authorization : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField LoginField;
    [SerializeField]
    private TMP_InputField PasswordField;
    [SerializeField]
    public SceneLoader sceneLoaderForAdmin; // Ссылка на SceneLoader
    [SerializeField]
    public SceneLoader sceneLoaderForStudent; // Ссылка на SceneLoader
    [SerializeField]
    private GameObject errorPanel; // Панель для уведомления
    [SerializeField]
    private TMP_Text errorMessageText; // Текст уведомления

    private string errorMessage;

    public void CheckAuthorization()
    {
        string login = LoginField.text;
        string password = PasswordField.text;

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

        SignInRequest requestData = new SignInRequest(login, password);
        // Отправляем запрос на авторизацию
        string url = AuthURL.SIGN_IN;
        ApiHandler.SendPostRequest(url, requestData, this, HandleAuthorizationResponse);
    }

    private void HandleAuthorizationResponse(ApiResponse response)
    {
        Debug.Log("Status Code: " + response.StatusCode);
        Debug.Log("Response Body: " + response.Body);

        if (response.StatusCode == 200)
        {
            // Успешная авторизация
            Debug.Log("Авторизация прошла успешно!");
            AuthResponse authResponse = JsonUtility.FromJson<AuthResponse>(response.Body);
            GlobalState.userToken = authResponse.token;
            if (LoginField.text == "admin")
            {
                sceneLoaderForAdmin.LoadScene();
            }
            else
            {
                sceneLoaderForStudent.LoadScene();
            }
        }
        else
        {
            // Обработка ошибки авторизации
            string errorMsg = "Ошибка авторизации: " + response.Body;
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
