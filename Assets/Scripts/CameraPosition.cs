using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Position
{
    // Use either Transform or Vector3, the Vector3 will automatically update if a Transform is present
    public Transform posObject;
    public Vector3 position;
    public float cameraSize = 10.0F;
    public bool ignoreNext;

}



public class CameraPosition : MonoBehaviour
{
    public Position[] cameraPositions;

    public int positionIndex = 0;
    public bool moveCamera;
    bool stop;
    public float speed;
    private Camera thisCamera;

    // Start is called before the first frame update
    void Start()
    {
        thisCamera = GetComponent<Camera>();
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
            MoveCameraForwards();
        }


        if (Input.GetKeyDown(KeyCode.Q))
        {
            MoveCameraBackwards();
        }

        EdgeCases();
        MoveCamera();

    }

    void EdgeCases()
    {
        if (positionIndex >= cameraPositions.Length)
        {
            positionIndex = 0;
        }

        if (positionIndex < 0)
        {
            positionIndex = cameraPositions.Length - 1;
        }
    }

    void MoveCamera()
    {
        if (moveCamera)
        {
            transform.position = Vector3.Lerp(transform.position, cameraPositions[positionIndex].position, speed * Time.deltaTime);
            thisCamera.orthographicSize = Mathf.Lerp(thisCamera.orthographicSize, cameraPositions[positionIndex].cameraSize, speed * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, cameraPositions[positionIndex].position) <= 0.01f && !stop)
        {
            moveCamera = false;
            stop = true;

        }
    }

    public void MoveCameraForwards()
    {
        if (!moveCamera && !cameraPositions[positionIndex].ignoreNext)
        {
            positionIndex += 1;
        }
        else
        {
            positionIndex += 2;
        }

        stop = false;
        moveCamera = true;
    }

    public void MoveCameraBackwards()
    {
        if (!moveCamera)
        {
            positionIndex -= 1;
        }
        
        stop = false;
        moveCamera = true;
    }

    public void MoveCameraTo(int pos)
    {
        if (!moveCamera)
        {
            positionIndex = pos;
        }

        stop = false;
        moveCamera = true;
    }


}
