using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModalWindow : MonoBehaviour
{
    [SerializeField]
    private GameObject modalWindow; // Панель для уведомления
    private void Start()
    {
        // Скрываем панель уведомления при старте
        modalWindow.SetActive(false);

    }
}
