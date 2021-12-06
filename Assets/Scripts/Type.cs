using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Type", menuName = "Stork Market/Type")]
public class Type : ScriptableObject
{
    public string description;
    public Sprite sprite;
    public float price;
}

