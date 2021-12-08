using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddConveyorButton : Button
{
    public TextMeshPro textMesh;
    public SpriteRenderer sprite;
    public float price = 50.0F;
    public float priceIncrement = 50.0F;

    
    private void Update()
    {
        sprite.color = MoneyManager.INSTANCE.money >= price ? Color.green : Color.red;
        textMesh.text = price.ToString("C");
    }

    protected override void OnMouseDown()
    {
        if (MoneyManager.INSTANCE.money >= price)
        {
            onPressed.Invoke();
            Mouse.INSTANCE.Release();
            MoneyManager.INSTANCE.money -= price;
            price += priceIncrement;
        }
        
    }

    protected override void OnMouseEnter()
    {
        Mouse.INSTANCE.Point();
    }

    protected override void OnMouseExit()
    {
        Mouse.INSTANCE.Release();
    }
}
