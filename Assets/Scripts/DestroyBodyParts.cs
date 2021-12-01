using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBodyParts : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BodyPart"))
        {
            Destroy(other.gameObject);
        }
    }
}
