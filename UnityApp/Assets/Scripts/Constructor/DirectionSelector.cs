using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DirectionSelector : MonoBehaviour, IPointerClickHandler
{
    public GameObject dropdownPrefab; // Префаб выпадающего списка
    private GameObject dropdownInstance;

    private string[] availableDirections; // Доступные направления
    private int selectDirectionIndex;

    // Событие для изменения направления
    public event Action<string> OnDirectionChanged;

    // Метод для установки доступных направлений
    public void SetAvailableDirections(string[] directions)
    {
        availableDirections = directions;
        selectDirectionIndex = 0;
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
        RectTransformUtility.ScreenPointToLocalPointInRectangle(FindObjectOfType<Canvas>().GetComponent<RectTransform>(), clickPosition, Camera.main, out Vector2 localPoint);

        // Создаем экземпляр выпадающего списка
        dropdownInstance = Instantiate(dropdownPrefab, localPoint, Quaternion.identity);
        dropdownInstance.transform.SetParent(FindObjectOfType<Canvas>().transform, false);

        // Получаем компонент TMP_Dropdown и настраиваем его
        TMP_Dropdown dropdown = dropdownInstance.GetComponent<TMP_Dropdown>();
        dropdown.ClearOptions();
        dropdown.AddOptions(new List<string>(availableDirections));

        // Устанавливаем выбранное значение
        dropdown.value = selectDirectionIndex; // Устанавливаем знач
        dropdown.onValueChanged.AddListener(OnDirectionSelected);

        // Позиционируем dropdown немного ниже точки клика
        RectTransform dropdownRectTransform = dropdownInstance.GetComponent<RectTransform>();
        dropdownRectTransform.anchoredPosition = new Vector2(localPoint.x, localPoint.y - 50);
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
        selectDirectionIndex = index;
        string selectedDirection = availableDirections[selectDirectionIndex];
        Debug.Log($"Направление выбрано: {selectedDirection}");

        // Вызываем событие изменения направления
        OnDirectionChanged?.Invoke(selectedDirection);

        HideDropdown(); // Скрываем выпадающий список после выбора
    }

    private void OnDestroy()
    {
        HideDropdown(); // Убедимся, что список скрыт, если объект уничтожается
    }
}
