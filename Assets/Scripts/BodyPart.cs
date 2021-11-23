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
        WhenAttached();
        // Testing scoring system, remove code at the end
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (BodyJoint joint in joints)
            {
                if (joint.type == BodyJoint.JointType.BaseSlot)
                {
                    List<PartAttributes> score = GetRating();
                    foreach (PartAttributes attribute in score)
                    {
                        Debug.Log(attribute.attribute.name + ": " + attribute.percent);
                    }
                }
            }
        }
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
        //transform.position = mousePos;
        rb.velocity = (mousePos - new Vector2(transform.position.x, transform.position.y)) * 10.0F;
        //rb.velocity = Vector2.zero;
        if (Input.GetMouseButton(1))
        {
            rb.AddTorque(-0.25F, ForceMode2D.Force);
        }
    }

    private void WhenAttached()
    {
        foreach (BodyJoint joint in joints)
        {
            if (joint.isAttached && (joint.type == BodyJoint.JointType.Attachment || joint.type == BodyJoint.JointType.BaseAttachment))
            {
                transform.position += (transform.parent.position - joint.transform.position) * (Time.deltaTime * 5.0F);
                transform.RotateAround(joint.transform.position,Vector3.forward, Mathf.DeltaAngle(joint.transform.rotation.eulerAngles.z,transform.parent.rotation.eulerAngles.z + 180.0F) * Time.deltaTime * 5.0F);
            }
        }
    }

    public List<PartAttributes> GetRating()
    {
        BodyPart[] children = GetComponentsInChildren<BodyPart>();
        Dictionary<Attribute, float> ratings = new Dictionary<Attribute, float>();
        foreach (BodyPart part in children)
        {
            foreach (PartAttributes partAttribute in part.attributes)
            {
                if (!ratings.ContainsKey(partAttribute.attribute))
                {
                    ratings.Add(partAttribute.attribute,0.0F);
                }
                ratings[partAttribute.attribute] += partAttribute.percent;
            }
        }
        List<PartAttributes> result = new List<PartAttributes>();
        foreach (KeyValuePair<Attribute, float> rating in ratings)
        {
            PartAttributes attribute = new PartAttributes {attribute = rating.Key, percent = rating.Value};
            result.Add(attribute);
        }
        return result;
    }

    public void SetAttributes(Attribute attributeAdded, float percentage)
    {
        PartAttributes newAttribute;
        newAttribute = new PartAttributes { attribute = attributeAdded, percent = percentage};
        attributes.Add(newAttribute);
    }
}
