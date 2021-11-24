using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hopper : MonoBehaviour
{
    public List<BodyPart> pickedParts;
    public int maxParts = 10;
    public int curParts = 0;
    public CameraPosition camera;

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
           
            pickedParts.Add(other.gameObject.GetComponent<BodyPart>());
            curParts++;
            if(curParts == maxParts)
            {
                camera.MoveCameraForwards();
            }
        }
    }
}
