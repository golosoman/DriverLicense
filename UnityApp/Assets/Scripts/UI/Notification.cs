using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification : MonoBehaviour
{
    [SerializeField]
    private GameObject notification; // Панель для уведомления
    private void Start()
    {
        // Скрываем панель уведомления при старте
        Debug.Log(notification);
        notification.SetActive(false);
    }
}
