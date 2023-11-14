using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class OnMouseEvents : MonoBehaviour
{
    public UnityEvent EventMouseDown;
    public UnityEvent EventMouseUp;
    public UnityEvent EventMouseEnter;
    public UnityEvent EventMouseExit;

    private void OnMouseDown()
    {
        EventMouseDown?.Invoke();
    }

    private void OnMouseUp()
    {
        EventMouseUp?.Invoke();
    }

    private void OnMouseEnter()
    {
        EventMouseEnter?.Invoke();
    }

    private void OnMouseExit()
    {
        EventMouseExit?.Invoke();
    }
}
