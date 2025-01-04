using UnityEngine;

public static class GlobalManager
{
    public static int carCount = 0;

    public static void IncrementCarCount()
    {
        carCount++;
        Debug.Log(carCount);
    }

    public static void DecrementCarCount()
    {
        carCount--;
        Debug.Log(carCount);
        if (carCount <= 0)
        {
            ShowSuccessModal();
        }
    }

    private static ModalManager modalManager; // Ссылка на ModalManager

    public static void Initialize(ModalManager manager)
    {
        modalManager = manager; // Инициализация ссылки на ModalManager
    }

    private static void ShowSuccessModal()
    {
        if (modalManager != null)
        {
            modalManager.ShowModal("Поздравляем с успешной сдачей вопроса."); // Вызов метода для отображения модального окна
        }
    }
}
