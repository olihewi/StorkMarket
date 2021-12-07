using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorAnimation : MonoBehaviour
{
    public ConveyorScript2 conveyor;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3((transform.position.x - 10 + conveyor.speed * Time.deltaTime)%20 + 10,transform.position.y,transform.position.z);
    }
}
