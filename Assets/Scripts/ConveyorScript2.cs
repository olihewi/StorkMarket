using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorScript2 : MonoBehaviour
{
    private List<BodyPart> onConveyor = new List<BodyPart>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        BodyPart bodyPart = other.gameObject.GetComponent<BodyPart>();
        if (bodyPart != null)
        {
            onConveyor.Add(bodyPart);
            bodyPart.rb.gravityScale = 0.0F;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        BodyPart bodyPart = other.gameObject.GetComponent<BodyPart>();
        if (bodyPart != null)
        {
            onConveyor.Remove(bodyPart);
            bodyPart.rb.gravityScale = 1.0F;
        }
    }

    private void Update()
    {
        foreach (BodyPart bodyPart in onConveyor)
        {
            if (!bodyPart.held)
            {
                //bodyPart.transform.Translate(0.0F,-bodyPart.rb.velocity.y * 2 * Time.deltaTime, 0.0F);
                bodyPart.rb.velocity = Vector2.right * 2.0F;
            }
        }
    }

}
