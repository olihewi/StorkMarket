using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public UnityEvent onPressed = new UnityEvent();

    private void OnMouseDown()
    {
        onPressed.Invoke();
        Mouse.INSTANCE.Release();
    }

    private void OnMouseEnter()
    {
        Mouse.INSTANCE.Point();
    }

    private void OnMouseExit()
    {
        Mouse.INSTANCE.Release();
    }
}
