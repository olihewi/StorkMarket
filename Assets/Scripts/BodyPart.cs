using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PartAttributes
{
    public Attribute attribute;
    public float percent;
}


[RequireComponent(typeof(Rigidbody2D))]
public class BodyPart : MonoBehaviour
{
    public static List<BodyPart> BODY_PARTS = new List<BodyPart>();
    public List<PartAttributes> attributes = new List<PartAttributes>();
    
    private void Start()
    {
        BODY_PARTS.Add(this);
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        joints.AddRange(GetComponentsInChildren<BodyJoint>());
    }
    private void Update()
    {
        WhenHeld();
    }
    private void OnDestroy()
    {
        BODY_PARTS.Remove(this);
    }
    
    // [Oli]: Joint Connection System //
    private bool held = false;
    [HideInInspector] public List<BodyJoint> joints = new List<BodyJoint>();
    private Camera mainCamera;
    private Rigidbody2D rb;
    private void OnMouseDown()
    {
        held = true;
        rb.angularDrag = 1.0F;
        foreach (BodyJoint joint in joints)
        {
            joint.Detach();
            JointRenderer.INSTANCE.DisplayValidJoints(joint);
        }
        rb.isKinematic = false;
    }
    private void OnMouseUp()
    {
        held = false;
        rb.angularDrag = 0.05F;
        JointRenderer.INSTANCE.Clear();
        
        foreach (BodyPart otherPart in BODY_PARTS)
        {
            if (otherPart == this) continue;
            foreach (BodyJoint thisJoint in joints)
            {
                foreach (BodyJoint otherJoint in otherPart.joints)
                {
                    if (!thisJoint.CanAttach(otherJoint)) continue;
                    thisJoint.Attach(otherJoint);
                    return;
                }
            }
            
        }
    }
    private void WhenHeld()
    {
        if (!held) return;
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;
        rb.velocity = Vector2.zero;
        if (Input.GetMouseButton(1))
        {
            rb.AddTorque(-0.25F, ForceMode2D.Force);
        }
    }
}
