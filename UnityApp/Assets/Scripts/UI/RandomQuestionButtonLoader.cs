using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class RandomQuestionButtonLoader : MonoBehaviour
{
    [SerializeField]
    private Button randomQuestionButton; // Ссылка на кнопку
    [SerializeField]
    private SceneLoader sceneLoader; // Ссылка на SceneLoader
    private string apiUrl = QuestionURL.RANDOM_QESTION_URL; // Замените на ваш URL

    private void Start()
    {
        randomQuestionButton.onClick.AddListener(OnRandomQuestionButtonClick);
    }

    private void OnRandomQuestionButtonClick()
    {
        StartCoroutine(LoadRandomQuestion());
    }

    private IEnumerator LoadRandomQuestion()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                // Десериализация JSON-ответа
                Question question = JsonUtility.FromJson<Question>(webRequest.downloadHandler.text);
                GlobalState.questionId = question.id; // Сохраняем question_id в GlobalState

                // Здесь можно добавить код для загрузки следующей сцены, если это необходимо
                sceneLoader.LoadScene();
            }
        }
    }
}

