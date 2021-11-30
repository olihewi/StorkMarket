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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered Trigger");
        if(other.gameObject.CompareTag("BodyPart"))
        {
            Debug.Log("Its a body part");
            other.gameObject.GetComponent<BodyPart>().Release();
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
