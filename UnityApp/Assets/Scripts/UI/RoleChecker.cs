using UnityEngine;
using System;

public class UserRoleChecker : MonoBehaviour
{
    [SerializeField]
    public SceneLoader sceneLoaderForAdmin; // Ссылка на SceneLoader для администратора
    [SerializeField]
    public SceneLoader sceneLoaderForStudent; // Ссылка на SceneLoader для студента

    public void CheckUserRole()
    {
        string url = "http://localhost:8080/auth/me";
        string token = GlobalState.userToken;

        ApiHandler.SendGetRequest(url, this, HandleRoleResponse, token);
    }

    private void HandleRoleResponse(ApiResponse response)
    {
        if (response.StatusCode == 200)
        {
            try
            {
                MeResponse meResponse = JsonUtility.FromJson<MeResponse>(response.Body);
                UserRole userRole = (UserRole)Enum.Parse(typeof(UserRole), meResponse.role);

                if (userRole == UserRole.ROLE_ADMIN)
                {
                    sceneLoaderForAdmin.LoadScene();
                }
                else
                {
                    sceneLoaderForStudent.LoadScene();
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Error parsing response: " + e.Message);
                // Обработка ошибки парсинга
            }
        }
        else
        {
            Debug.LogError("Error fetching user role: " + response.Body);
            // Обработка ошибки запроса
        }
    }
}