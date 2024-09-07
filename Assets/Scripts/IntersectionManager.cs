using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IntersectionManager : MonoBehaviour
{
    public GameObject crossIntersection;
    public GameObject tIntersection;

    void Start()
    {
        // Пример данных из БД
        string intersectionType = "Cross"; // или "T"
        
        if (intersectionType == "Cross")
        {
            Instantiate(crossIntersection, new Vector3(0, 0, 0), Quaternion.identity);
        }
        else if (intersectionType == "T")
        {
            Instantiate(tIntersection, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}