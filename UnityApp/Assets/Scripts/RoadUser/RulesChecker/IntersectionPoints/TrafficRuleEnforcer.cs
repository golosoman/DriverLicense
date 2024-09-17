using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class TrafficRuleEnforcer : MonoBehaviour
{
    private RoadUserManager roadUserManager;
    public RoadUserManager RoadUserManager { get => roadUserManager; set => roadUserManager = value; }

    private void Start()
    {
        roadUserManager = FindObjectOfType<RoadUserManager>();
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == TagObjectNamesTypes.CAR)
        {
            RoadUserMovement userMovement = other.gameObject.GetComponent<RoadUserMovement>();

            bool hasObstacleOnRight = CheckObstacleOnRight(other.gameObject, userMovement.RUD);

            if (hasObstacleOnRight)
            {
                userMovement.StopMovement();
                ShowUserDialog();
            }
        }
    }

    public abstract bool CheckObstacleOnRight(GameObject roadUserDataObject, RoadUserData roadUserData);

    public virtual void ShowUserDialog()
    {
        // Заменить это на диалоговое окно с опциями
        Debug.Log("Обнаружено препятствие справа. Пропустить или не пропустить?");
    }
}
