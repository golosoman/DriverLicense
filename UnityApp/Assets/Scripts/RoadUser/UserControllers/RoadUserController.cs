using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadUserController : MonoBehaviour
{
    private RoadUserMovement roadUserMovement;
    private RuleChecker ruleChecker;
    private RoadManager roadUserManager;
    [SerializeField]
    private Collider2D carCollider; 

    void Start()
    {
        ruleChecker = GetComponent<RuleChecker>();
        roadUserMovement = GetComponent<RoadUserMovement>();
        roadUserManager = FindObjectOfType<RoadManager>();

        if (roadUserMovement is null)
        {
            Debug.LogError("RoadUserMovement component is missing on the roadUser object.");
        }
    }

    void OnMouseDown()
    {
        // Проверка нажатия в пределах маленького колайдера
        if (carCollider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
        {
            if (roadUserMovement != null)
            {
                roadUserMovement.StartMovement();
            }
            else
            {
                Debug.LogError("RoadUserMovement or TrafficRulesManager is null. Cannot start movement.");
            }
        }
    }
}