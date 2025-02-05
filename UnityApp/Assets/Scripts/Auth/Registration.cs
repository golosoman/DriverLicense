using System;
using UnityEngine;
using TMPro;

public class Registration : MonoBehaviour
{
    public static event Action<string> OnErrorOccurred;

    [SerializeField]
    private TMP_InputField LoginField;
    [SerializeField]
    private TMP_InputField PasswordField;
    [SerializeField]
    private TMP_InputField RepeatPasswordField;
    [SerializeField]
    public SceneLoader sceneLoader;

    private string errorMessage;

    public void CheckRegistration()
    {
        string login = LoginField.text;
        string password = PasswordField.text;
        string repeatPassword = RepeatPasswordField.text;

        if (InputValidator.IsNullOrEmpty(login))
        {
            errorMessage = "Логин введен неверно";
            Debug.LogError(errorMessage);
            OnErrorOccurred?.Invoke(errorMessage);
            return;
        }
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
        if (!InputValidator.ArePasswordsMatching(password, repeatPassword))
        {
            errorMessage = "Пароли не совпадают";
            Debug.LogError(errorMessage);
            OnErrorOccurred?.Invoke(errorMessage);
            return;
        }

        SignupRequest requestData = new SignupRequest(login, password, repeatPassword);
        string url = AuthURL.SIGN_UP;
        ApiHandler.SendPostRequest(url, requestData, this, HandleRegistrationResponse);
    }

    private void HandleRegistrationResponse(ApiResponse response)
    {
        Debug.Log("Status Code: " + response.StatusCode);
        Debug.Log("Response Body: " + response.Body);

        if (response.StatusCode == 200)
        {
            Debug.Log("Регистрация прошла успешно!");
            AuthResponse authResponse = JsonUtility.FromJson<AuthResponse>(response.Body);
            GlobalState.userToken = authResponse.token;
            sceneLoader.LoadScene();
        }
        else
        {
            string errorMsg = "Ошибка регистрации: " + response.Body;
            Debug.LogError(errorMsg);
            OnErrorOccurred?.Invoke("Данные неверные. Ошибка 403.");
        }
    }
}
