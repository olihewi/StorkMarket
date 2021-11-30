using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorAnimation : MonoBehaviour
{
    [SerializeField] private float speed = 2.0F;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3((transform.position.x - 10 + speed * Time.deltaTime)%20 + 10,transform.position.y,transform.position.z);
    }
}
