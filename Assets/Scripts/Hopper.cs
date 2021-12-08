using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hopper : MonoBehaviour
{
    public List<BodyPart> pickedParts;
    public int maxParts = 10;
    public int curParts = 0;
    public CameraPosition camera;
    public TextMeshPro text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "Capacity:\n" + curParts + " / " + maxParts;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetText()
    {
        text.text = "Capacity:\n" + "0" + " / " + maxParts;
        curParts = 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("BodyPart"))
        {
            BodyPart bp = other.gameObject.GetComponent<BodyPart>();
            if (pickedParts.Contains(bp)) return;
            bp.Release();
            pickedParts.Add(other.gameObject.GetComponent<BodyPart>());
            curParts++;
            if (text != null)
            {
                text.text = "Capacity:\n" + curParts + " / " + maxParts;
            }
            if(curParts == maxParts)
            {
                camera.MoveCameraForwards();
            }
        }
    }
}
