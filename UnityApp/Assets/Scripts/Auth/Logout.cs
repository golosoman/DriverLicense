using UnityEngine;

public class LogoutButton : MonoBehaviour
{
    [SerializeField]
    private SceneLoader sceneLoader; // Ссылка на SceneLoader

    public void OnLogoutButtonClicked()
    {
        // Сбрасываем токен пользователя
        GlobalState.userToken = "";

        // Загружаем сцену авторизации
        sceneLoader.LoadScene();
    }
}

