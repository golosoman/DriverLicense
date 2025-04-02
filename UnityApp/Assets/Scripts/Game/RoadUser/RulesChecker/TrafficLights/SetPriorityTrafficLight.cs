using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SetPriorityTrafficLight : MonoBehaviour
{
    [SerializeField] private Sprite greenSprite;
    [SerializeField] private Sprite redSprite;
    private SpriteRenderer spriteRenderer;
    private List<CarMovement> carsInTrigger = new List<CarMovement>();
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GlobalManager.OnReverseTrafficLight += OnReverseTrafficLightHandler;
    }

    private void OnDestroy()
    {
        GlobalManager.OnReverseTrafficLight -= OnReverseTrafficLightHandler;
    }

    private void OnReverseTrafficLightHandler(string arg)
    {
        Debug.Log("Отработалос событие OnReverseTrafficLightHandler!");
        if (gameObject.name.Contains("Green"))
        {
            spriteRenderer.sprite = redSprite;
            gameObject.name = gameObject.name.Replace("Green", "Red");
        }
        else if (gameObject.name.Contains("Red"))
        {
            spriteRenderer.sprite = greenSprite;
            gameObject.name = gameObject.name.Replace("Red", "Green");
        }

        foreach (CarMovement car in carsInTrigger)
        {
            car.HasPriority = !car.HasPriority;
            Debug.Log(car.gameObject.name + " priority switched: " + car.HasPriority);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Car"))
        {
            CarMovement carMovement = other.gameObject.GetComponent<CarMovement>();
            if (!carsInTrigger.Contains(carMovement))
            {
                carsInTrigger.Add(carMovement);
            }

            if (gameObject.name.Contains("Green"))
            {
                carMovement.HasPriority = true;
                GlobalManager.IncrementCarHasPriorityCount();
            }

            Debug.Log(other.gameObject.name + "    " + carMovement.HasPriority);
        }
    }
}
