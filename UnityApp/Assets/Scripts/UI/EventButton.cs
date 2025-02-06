using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventButton : MonoBehaviour
{
    public delegate void ShowExplanation(string message);
    public static event ShowExplanation OnShowExplanation;

    public delegate void ShowExitModal(string message);
    public static event ShowExitModal OnShowExitModal;

    public void ButtonClickShowExplanation()
    {
        OnShowExplanation?.Invoke("123");
    }

    public void ButtonClickShowExitModal()
    {
        OnShowExitModal?.Invoke("Вы точно хотите выйти?");
    }
}
