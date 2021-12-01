using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorScript : MonoBehaviour
{
    public float speed;

    public Vector2 direction;

    public List<GameObject> onBelt;




    // Update is called once per frame
  /*  void Update()
    {
        for (int i = 0; i <= onBelt.Count - 1; i++)
        {
            onBelt[i].GetComponent<Rigidbody2D>().velocity = speed * direction * Time.deltaTime;
        }
    } */
    
    private void OnCollisionTrigger(Collision collision)
    {
        onBelt.Add(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        onBelt.Remove(collision.gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().velocity = speed * direction * Time.deltaTime;
    }
}
