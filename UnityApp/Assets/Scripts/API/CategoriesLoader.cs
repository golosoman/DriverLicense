using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class CategoriesLoader : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown categoryDropdown; // Ссылка на Dropdown
    private string apiUrl = CategoryURL.ALL_CATEGORY_URL; // URL для получения категорий

    private void Awake()
    {
        StartCoroutine(LoadCategories());
    }

    private IEnumerator LoadCategories()
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
                Debug.Log("Обращение выполнено");
                string jsonResponse = webRequest.downloadHandler.text;
                CategoryList categoryList = JsonUtility.FromJson<CategoryList>("{\"categories\":" + jsonResponse + "}");
                Debug.Log("Полученные данные: " + jsonResponse);
                // Очищаем существующие элементы в Dropdown
                categoryDropdown.ClearOptions();

                // Создаем список для хранения названий категорий
                List<string> categoryNames = new List<string>();

                // Заполняем список названиями категорий
                foreach (Category category in categoryList.categories)
                {
                    categoryNames.Add(category.name);
                }
                Debug.Log("Добавляем опции в dropDown");
                // Добавляем названия категорий в Dropdown
                categoryDropdown.AddOptions(categoryNames);
            }
        }
    }
}
