using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    [SerializeField]
    private bool destroyObject = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == TagObjectNamesTypes.CAR)
        {
            if (destroyObject)
            {
                Destroy(other.gameObject);
                GlobalManager.DecrementCarCount(); // Уменьшаем счетчик машин
            }
            else
            {
                other.gameObject.SetActive(false);
                GlobalManager.DecrementCarCount(); // Уменьшаем счетчик машин
            }
        }
    }
}