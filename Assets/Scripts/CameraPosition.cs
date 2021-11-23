using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Position
{
    // Use either Transform or Vector3, the Vector3 will automatically update if a Transform is present
    public Transform posObject;
    public Vector3 position;

}



public class CameraPosition : MonoBehaviour
{
    public Position[] cameraPositions;

    public int positionIndex = 0;
    public bool moveCamera;
    bool stop;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var pos in cameraPositions)
        {
            if (pos.posObject != null)
            {
                pos.position = new Vector3(pos.posObject.position.x, pos.posObject.position.y, -10);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            MoveCamera();
        }

        if(positionIndex >= cameraPositions.Length)
        {
            positionIndex = 0;
        }

        if (moveCamera)
        {
            transform.position = Vector3.Lerp(transform.position, cameraPositions[positionIndex].position, speed * Time.deltaTime);
        }

        if(Vector3.Distance(transform.position, cameraPositions[positionIndex].position) <= 0.001f && !stop)
        {
            moveCamera = false;
            stop = true;
            positionIndex++;

        }
        
    }

    public void MoveCamera()
    {
        stop = false;
        moveCamera = true;
    }
}
