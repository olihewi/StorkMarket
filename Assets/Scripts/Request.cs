using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public TextMeshPro textMesh;
    public RequestFormat[] requestFormats = new RequestFormat[0];

    public Type requestedType;
    public List<Attribute> requestedAttributes = new List<Attribute>();
    public float value = 0.0F;

    public Transform point1;
    public Transform point2;
    public SpriteRenderer sprite;

    // Start is called before the first frame update
    private void Start()
    {
        GenerateRequest(Random.Range(0, 3));
        RequestFormat requestFormat = requestFormats[Random.Range(0, requestFormats.Length)];

        string text = requestFormat.prefix.Replace("\\n", "\n").Replace("\\an","aeiouAEIOU".IndexOf(requestedType.name[0])>=0 ? "an" : "a").Replace("\\price", value.ToString("c2"));
        text += requestFormat.type.Replace("\\type", requestedType.name).Replace("\\n", "\n").Replace("\\price", value.ToString("c2"));
        foreach (Attribute attribute in requestedAttributes)
        {
            text += requestFormat.attribute.Replace("\\attribute", attribute.name).Replace("\\n", "\n");
        }
        text += requestFormat.suffix.Replace("\\n", "\n").Replace("\\price", value.ToString("c2"));
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
    private bool mouseOver = false;

    private static readonly int OutlineThickness = Shader.PropertyToID("_OutlineThickness");
    private void OnMouseEnter()
    {
        mouseOver = true;
        sprite.material.SetFloat(OutlineThickness, 4.0F);
    }
    private void OnMouseExit()
    {
        mouseOver = false;
        sprite.material.SetFloat(OutlineThickness, 0.0F);
    }
    private void Update()
    {
        Transform target = mouseOver ? point2 : point1;
        sprite.transform.position = Vector3.Lerp(sprite.transform.position, target.position, Time.deltaTime * 5.0F);
        sprite.transform.rotation = Quaternion.Slerp(sprite.transform.rotation, target.rotation, Time.deltaTime * 5.0F);
        sprite.transform.localScale = Vector3.Lerp(sprite.transform.localScale, target.localScale, Time.deltaTime * 5.0F);
    }
}
