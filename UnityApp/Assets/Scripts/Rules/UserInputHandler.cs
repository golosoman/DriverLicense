using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInputHandler : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                RoadUserMovement roadUser = hit.collider.GetComponent<RoadUserMovement>();
                if (roadUser != null)
                {
                    // Обработка клика пользователя
                    roadUser.StartMovement();
                }
            }
        }
    }
}
