using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public Transform[] route;
    private int currentPoint = 0;
    public float speed = 5f;
    private bool isMoving = false;

    public bool IsMoving => isMoving;

    public void SetRoute(Transform[] route, float speed)
    {
        // Debug.Log(route.Length);
        this.route = route;
        this.speed = speed;
    }

    public void StartMovement()
    {
        // Debug.Log(route.Length);
        if (!isMoving && route != null && route.Length > 0)
        {
            StartCoroutine(MoveAlongRoute());
        }
        else
        {
            Debug.LogError("Route is not set or is empty.");
        }
    }

    public void StopMovement()
    {
        StopAllCoroutines();
        speed = 0f;
        isMoving = false;
        Debug.Log("Car movement stopped.");
    }

    IEnumerator MoveAlongRoute()
    {
        if (route == null || route.Length == 0)
        {
            Debug.LogError("Route is not set or empty.");
            yield break;
        }

        isMoving = true;

        while (isMoving && currentPoint < route.Length)
        {
            Vector3 targetPosition = route[currentPoint].position;
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                yield return null;
            }

            currentPoint++;
            yield return null;
        }

        isMoving = false;
        Debug.Log("Car has finished the route.");
    }
}
