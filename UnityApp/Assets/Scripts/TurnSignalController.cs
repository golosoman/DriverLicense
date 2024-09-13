using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSignalController : MonoBehaviour
  {
    [SerializeField]
    private GameObject leftHandleIndicator;
    [SerializeField]
    private GameObject rightHandleIndicator;
    [SerializeField]
    private float signalTime = 0.5f;
    private float blinkTimer = 0f;
    private bool isLeftOn = false;
    private bool isRightOn = false;

    public void TurnLeftOn()
    {
        isLeftOn = true;
        leftHandleIndicator.SetActive(true);
    }

    public void TurnLeftOff()
    {
        isLeftOn = false;
        leftHandleIndicator.SetActive(false);
    }

    public void TurnRightOn()
    {
        isRightOn = true;
        rightHandleIndicator.SetActive(true);
    }

    public void TurnRightOff()
    {
        isRightOn = false;
        rightHandleIndicator.SetActive(false);
    }
}
