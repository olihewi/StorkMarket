using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
public class Request : MonoBehaviour
{
    private TextMeshPro text;
    private static Attribute[] ATTRIBUTES;
    private static Type[] TYPES;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        ATTRIBUTES = Resources.LoadAll<Attribute>("Descriptors");
        TYPES = Resources.LoadAll<Type>("Descriptors");
        text.text = TYPES[Random.Range(0, TYPES.Length)].name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
