using System.Collections;
using UnityEngine;

public class CarTurnIndicators : MonoBehaviour
{
    [SerializeField]
    private GameObject leftHandleIndicator;
    [SerializeField]
    private GameObject rightHandleIndicator;
    [SerializeField]
    private float blinkInterval = 0.3f; // Интервал мигания в секундах

    private Coroutine blinkCoroutine;
    private CarMovement carMovement;
    private bool isLeftIndicatorOn = false;
    private bool isRightIndicatorOn = false;
    private float blinkTimer = 0f;
    private string currentSignal;

    private void Start()
    {
        carMovement = GetComponent<CarMovement>();
        currentSignal = carMovement.RUD.MovementDirection;
    }

    private void Update()
    {
        // Включение и выключение поворотников в зависимости от направления движения
        if ( currentSignal == DirectionMovementTypes.LEFT || currentSignal == DirectionMovementTypes.BACKWARD)
        {
            TurnOnLeftIndicator();
            TurnOffRightIndicator();
        }
        else if (currentSignal == DirectionMovementTypes.RIGHT)
        {
            TurnOnRightIndicator();
            TurnOffLeftIndicator();
        }
        else
        {
            TurnOffLeftIndicator();
            TurnOffRightIndicator();
        }

        // Мигание поворотников
        if (blinkCoroutine == null)
        {
            blinkCoroutine = StartCoroutine(BlinkTurnIndicators());
        }
    }

    private IEnumerator BlinkTurnIndicators()
    {
        while (true)
        {
            blinkTimer += Time.deltaTime;

            if (isLeftIndicatorOn)
            {
                leftHandleIndicator.SetActive(blinkTimer < blinkInterval / 2);
            }

            if (isRightIndicatorOn)
            {
                rightHandleIndicator.SetActive(blinkTimer < blinkInterval / 2);
            }

            // Сброс таймера
            if (blinkTimer >= blinkInterval)
            {
                blinkTimer = 0f;
            }

            yield return null;
        }
    }

    private void TurnOnLeftIndicator()
    {
        isLeftIndicatorOn = true;
        leftHandleIndicator.SetActive(true);
    }

    private void TurnOffLeftIndicator()
    {
        isLeftIndicatorOn = false;
        leftHandleIndicator.SetActive(false);
    }

    private void TurnOnRightIndicator()
    {
        isRightIndicatorOn = true;
        rightHandleIndicator.SetActive(true);
    }

    private void TurnOffRightIndicator()
    {
        isRightIndicatorOn = false;
        rightHandleIndicator.SetActive(false);
    }
}
