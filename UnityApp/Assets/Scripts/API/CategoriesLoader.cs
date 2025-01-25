using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class CategoriesLoader : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown categoryDropdown; // Ссылка на Dropdown
    private string apiUrl = CategoryURL.ALL_CATEGORY_URL; // URL для получения категорий

    private void Awake()
    {
        LoadCategories();
    }

    private void LoadCategories()
    {
        string bearerToken = GlobalState.userToken; // Получаем токен
        ApiHandler.SendGetRequest(apiUrl, this, OnCategoriesReceived, bearerToken);
    }

    private void OnCategoriesReceived(ApiResponse response)
    {
        if (response.StatusCode != 200)
        {
            Debug.LogError("Error loading categories: " + response.Body);
            return;
        }

        CategoryList categoryList = JsonUtility.FromJson<CategoryList>("{\"categories\":" + response.Body + "}");

        categoryDropdown.ClearOptions();
        List<string> categoryNames = new List<string>();

        foreach (Category category in categoryList.categories)
        {
            categoryNames.Add(category.name);
        }

        categoryDropdown.AddOptions(categoryNames);
    }
}