using System.Collections;
using UnityEngine;
using TMPro;
using System;

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
    private string errorMessage; // Текст уведомления
    public static event Action<string> OnErrorOccurred;

    public void CheckAuthorization()
    {
        string login = LoginField.text;
        string password = PasswordField.text;

        // Проверяем, что поля не пустые
        if (InputValidator.IsNullOrEmpty(login))
        {
            errorMessage = "Логин введен неверно";
            Debug.LogError(errorMessage);
            OnErrorOccurred?.Invoke(errorMessage);
            return;
        }

        // Проверяем на наличие кириллицы
        if (InputValidator.ContainsCyrillic(login))
        {
            errorMessage = "Логин не должен содержать кириллицу";
            Debug.LogError(errorMessage);
            OnErrorOccurred?.Invoke(errorMessage);
            return;
        }

        if (InputValidator.IsNullOrEmpty(password))
        {
            errorMessage = "Пароль введен неверно";
            Debug.LogError(errorMessage);
            OnErrorOccurred?.Invoke(errorMessage);
            return;
        }

        if (InputValidator.ContainsCyrillic(password))
        {
            errorMessage = "Пароль не должен содержать кириллицу";
            Debug.LogError(errorMessage);
            OnErrorOccurred?.Invoke(errorMessage);
            return;
        }

        SignInRequest requestData = new SignInRequest(login, password);
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
            string errorMsg = "Ошибка авторизации: " + response.Body;
            Debug.LogError(errorMsg);
            OnErrorOccurred?.Invoke("Данные неверные. Ошибка 403.");
        }
    }
}
