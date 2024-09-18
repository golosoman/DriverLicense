using System.Collections.Generic;
using UnityEngine;

public class VisibilityZoneTrigger: MonoBehaviour
{
    public List<GameObject> VisibleObjects {get; set;}

    private void Start()
    {
        VisibleObjects = new List<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == TagObjectNamesTypes.CAR)
        {
            VisibleObjects.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == TagObjectNamesTypes.CAR)
        {
            VisibleObjects.Remove(other.gameObject);
        }
    }
}