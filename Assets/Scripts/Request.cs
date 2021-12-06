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
    public RequestFormat[] requestFormats = new RequestFormat[0];

    public Type requestedType;
    public List<Attribute> requestedAttributes = new List<Attribute>();
    public float value = 0.0F;
    // Start is called before the first frame update
    private void Start()
    {
        textMesh = GetComponent<TextMeshPro>();
        GenerateRequest(Random.Range(0, 4));
        RequestFormat requestFormat = requestFormats[Random.Range(0, requestFormats.Length)];

        string text = requestFormat.prefix.Replace("\\n", "\n").Replace("\\an","aeiouAEIOU".IndexOf(requestedType.name[0])>=0 ? "an" : "a");
        text += requestFormat.type.Replace("\\type", requestedType.name).Replace("\\n", "\n").Replace("\\price", value.ToString("c2"));
        foreach (Attribute attribute in requestedAttributes)
        {
            text += requestFormat.attribute.Replace("\\attribute", attribute.name).Replace("\\n", "\n");
        }
        text += requestFormat.suffix.Replace("\\n", "\n");
        textMesh.text = text;
    }

    public void GenerateRequest(int numAttributes)
    {
        List<Attribute> ATTRIBUTES = new List<Attribute>();
        ATTRIBUTES.AddRange(Resources.LoadAll<Attribute>("Descriptors"));
        List<Type> TYPES = new List<Type>();
        TYPES.AddRange(Resources.LoadAll<Type>("Descriptors"));

        requestedType = TYPES[Random.Range(0, TYPES.Count)];
        for (int i = 0; i < numAttributes; i++)
        {
            requestedAttributes.Add(ATTRIBUTES[Random.Range(0, ATTRIBUTES.Count)]);
            ATTRIBUTES.Remove(requestedAttributes[i]);
        }
        value = (requestedType.price + numAttributes) * Random.Range(0.8F, 1.2F);
    }
}
