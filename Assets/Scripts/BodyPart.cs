using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    [Serializable]
    public class Joint
    {
        public enum JointType
        {
            Slot,
            Attachment,
            BaseSlot,
            BaseAttachment
        }
        public Transform transform;
        public Transform attachedTo;
        public JointType type;
    }
    public List<Joint> joints;
    private bool held = false;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        WhenHeld();
        foreach (Joint joint in joints)
        {
            if (joint.attachedTo != null)
            {
                WhenAttached(joint);
            }
        }
    }
    
    // Oli: Body Part Connection System
    private void OnMouseDown()
    {
        held = true;
        rb.angularDrag = 1.0F;
        bool wasJoined = false;
        foreach (Joint joint in joints)
        {
            joint.attachedTo = null;
        }
        transform.parent = null;
        rb.isKinematic = false;
    }
    private void OnMouseUp()
    {
        held = false;
        rb.angularDrag = 0.05F;
        
        BodyPart[] allParts = FindObjectsOfType<BodyPart>();
        foreach (BodyPart part in allParts)
        {
            if (part == this) continue;
            foreach (Joint thisJoint in joints)
            {
                foreach (Joint otherJoint in part.joints)
                {
                    if ((thisJoint.type == Joint.JointType.Attachment && otherJoint.type == Joint.JointType.Slot ||
                        thisJoint.type == Joint.JointType.BaseAttachment && otherJoint.type == Joint.JointType.BaseSlot) &&
                        (thisJoint.transform.position - otherJoint.transform.position).magnitude < 0.5F)
                    {
                        Debug.Log(thisJoint.transform.name + " joined to " + otherJoint.transform.name);
                        thisJoint.attachedTo = otherJoint.transform;
                        transform.position = otherJoint.transform.position + (transform.position - thisJoint.transform.position);
                        transform.parent = otherJoint.transform;
                        
                        rb.isKinematic = true;
                        rb.velocity = Vector2.zero;
                        rb.angularVelocity = 0.0F;
                        //otherJoint.attachedTo = thisJoint.transform;
                        return;
                    }
                }
            }
            
        }
    }
    private void WhenHeld()
    {
        if (!held)
        {
            return;
        }
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;
        rb.velocity = Vector2.zero;
        if (Input.GetMouseButton(1))
        {
            rb.AddTorque(-0.25F, ForceMode2D.Force);
        }
        //Vector2 currentPos = new Vector2(transform.position.x,transform.position.y);
        //Vector2 difference = mousePos - currentPos;
        //difference = Vector2.MoveTowards(currentPos, mousePos, 1.0F);
        //difference = difference.magnitude > 1.0F ? difference.normalized : difference;
        //rb.AddForce(difference * 5.0F,ForceMode2D.Force);
        //rb.AddForce(Vector2.MoveTowards(transform.position,mousePos,0.001F * Time.deltaTime),ForceMode2D.Force);
    }
    private void WhenAttached(Joint _joint)
    {
    }

}
