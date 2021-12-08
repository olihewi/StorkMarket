using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public UnityEvent onPressed = new UnityEvent();

    protected virtual void OnMouseDown()
    {
        onPressed.Invoke();
        Mouse.INSTANCE.Release();
    }

    protected virtual void OnMouseEnter()
    {
        Mouse.INSTANCE.Point();
    }

    protected virtual void OnMouseExit()
    {
        Mouse.INSTANCE.Release();
    }

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
