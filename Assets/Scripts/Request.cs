using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
public class Request : MonoBehaviour
{
    [System.Serializable]
    public struct RequestFormat
    {
        public string prefix;
        public string type;
        public string attribute;
        public string suffix;
    }
    private TextMeshPro textMesh;
    private static Attribute[] ATTRIBUTES;
    private static Type[] TYPES;
    public RequestFormat[] requestFormats = new RequestFormat[0];
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshPro>();
        ATTRIBUTES = Resources.LoadAll<Attribute>("Descriptors");
        TYPES = Resources.LoadAll<Type>("Descriptors");
        RequestFormat requestFormat = requestFormats[Random.Range(0, requestFormats.Length)];
        Type thisType = TYPES[Random.Range(0, TYPES.Length)];

        string text = requestFormat.prefix.Replace("\\n", "\n");
        text += requestFormat.type.Replace("\\type", thisType.name).Replace("\\n", "\n");
        for (int i = 0; i < Random.Range(0,4); i++)
        {
            Attribute thisAttribute = ATTRIBUTES[Random.Range(0, ATTRIBUTES.Length)];
            text += requestFormat.attribute.Replace("\\attribute", thisAttribute.name).Replace("\\n","\n");
        }
        text += requestFormat.suffix.Replace("\\n", "\n");
        textMesh.text = text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
