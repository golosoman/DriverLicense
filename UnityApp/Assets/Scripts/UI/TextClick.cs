using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TextClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private UnityEvent onClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(name + " Game Object Clicked!", this);

        onClick.Invoke();
    }
}
