using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignController : MonoBehaviour
{
    public bool IsYieldSign; // Установите в Unity, если знак "Уступи дорогу"

    public bool ShouldYield()
    {
        return IsYieldSign;
    }
}
