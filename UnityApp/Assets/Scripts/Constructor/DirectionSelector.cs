using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DirectionSelector : MonoBehaviour, IPointerClickHandler
{
    public GameObject dropdownPrefab; // Префаб выпадающего списка
    private GameObject dropdownInstance;

    private string[] availableDirections; // Доступные направления

    // Метод для установки доступных направлений
    public void SetAvailableDirections(string[] directions)
    {
        availableDirections = directions;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Проверяем, нажата ли правая кнопка мыши
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (dropdownInstance == null)
            {
                ShowDropdown(eventData.position); // Передаем позицию клика
            }
            else
            {
                HideDropdown();
            }
        }
    }

    private void ShowDropdown(Vector2 clickPosition)
    {
        // Преобразуем экранные координаты в локальные координаты канваса
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(FindObjectOfType<Canvas>().GetComponent<RectTransform>(), clickPosition, Camera.main, out localPoint);

        // Создаем экземпляр выпадающего списка
        dropdownInstance = Instantiate(dropdownPrefab, localPoint, Quaternion.identity);
        dropdownInstance.transform.SetParent(FindObjectOfType<Canvas>().transform, false);

        // Получаем компонент TMP_Dropdown и настраиваем его
        TMP_Dropdown dropdown = dropdownInstance.GetComponent<TMP_Dropdown>();
        dropdown.ClearOptions();
        dropdown.AddOptions(new List<string>(availableDirections));
        dropdown.onValueChanged.AddListener(OnDirectionSelected);

        // Позиционируем dropdown немного ниже точки клика, чтобы он не перекрывал его
        RectTransform dropdownRectTransform = dropdownInstance.GetComponent<RectTransform>();
        dropdownRectTransform.anchoredPosition = new Vector2(localPoint.x, localPoint.y - 50); // Пример сдвига вниз
    }

    private void HideDropdown()
    {
        if (dropdownInstance != null)
        {
            Destroy(dropdownInstance);
        }
    }

    private void OnDirectionSelected(int index)
    {
        string selectedDirection = availableDirections[index];
        Debug.Log($"Направление выбрано: {selectedDirection}");

        // Здесь можно добавить логику для изменения направления автомобиля

        HideDropdown(); // Скрываем выпадающий список после выбора
    }

    private void OnDestroy()
    {
        HideDropdown(); // Убедимся, что список скрыт, если объект уничтожается
    }
}
