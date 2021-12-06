using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PartAttributes
{
    public Attribute attribute;
    public float percent;
    public Type type;
}


[RequireComponent(typeof(Rigidbody2D))]
public class BodyPart : MonoBehaviour
{
    public static List<BodyPart> BODY_PARTS = new List<BodyPart>();
    public List<PartAttributes> attributes = new List<PartAttributes>();

    private AudioSource audioSource;

    public bool isInEvalScene;
    
    private void Start()
    {
        BODY_PARTS.Add(this);
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        joints.AddRange(GetComponentsInChildren<BodyJoint>());
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        WhenHeld();
        WhenAttached();



        if (EvalSceneManager.isInEvalScene)
        {
            enabled = false;
        }

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
    public bool held = false;
    [HideInInspector] public List<BodyJoint> joints = new List<BodyJoint>();
    private Camera mainCamera;
    [HideInInspector] public Rigidbody2D rb;
    private SpriteRenderer sprite;
    private static int maxOrderInLayer = 0;
    private static readonly int OutlineThickness = Shader.PropertyToID("_OutlineThickness");
    private static readonly int OutlineColour = Shader.PropertyToID("_OutlineColor");
    private void OnMouseDown()
    {
        held = true;
        rb.angularDrag = 1.0F;
        foreach (BodyJoint joint in joints)
        {
            if (joint.type == BodyJoint.JointType.Attachment || joint.type == BodyJoint.JointType.BaseAttachment)
            {
                   joint.Detach();
            }
            JointRenderer.INSTANCE.DisplayValidJoints(joint);
        }
        sprite.sortingOrder = ++maxOrderInLayer;
        rb.isKinematic = false;
        Mouse.INSTANCE.Grab();
        sprite.material.SetColor(OutlineColour,new Color(1.0F,1.0F,0.0F,0.5F));
    }
    private void OnMouseUp()
    {
        Release();
        Mouse.INSTANCE.Release();
    }

    private void OnMouseEnter()
    {
        if(sprite != null)
        {
            sprite.material.SetFloat(OutlineThickness, 10.0F);
        }
        
    }

    private void OnMouseExit()
    {
        if(sprite != null)
        {
            sprite.material.SetFloat(OutlineThickness, 0.0F);
        }
        
    }

    public void Release()
    {
        held = false;
        rb.angularDrag = 0.05F;
        JointRenderer.INSTANCE.Clear();
        sprite.material.SetColor(OutlineColour,new Color(1.0F,1.0F,1.0F,0.5F));
        
        foreach (BodyPart otherPart in BODY_PARTS)
        {
            if (otherPart == this) continue;
            foreach (BodyJoint thisJoint in joints)
            {
                foreach (BodyJoint otherJoint in otherPart.joints)
                {
                    if (!thisJoint.CanAttach(otherJoint)) continue;
                    thisJoint.Attach(otherJoint);
                    audioSource.Play();
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
        Vector2 difference = mousePos - new Vector2(transform.position.x, transform.position.y);
        rb.velocity = difference * 10.0F;
        //rb.velocity = Vector2.zero;
        if (Input.GetMouseButton(1))
        {
            rb.AddTorque(-0.25F, ForceMode2D.Force);
        }
        sprite.material.SetFloat(OutlineThickness,10.0F);
    }

    private void WhenAttached()
    {
        if (!EvalSceneManager.isInEvalScene)
        {
            foreach (BodyJoint joint in joints)
            {
                if (joint.isAttached && (joint.type == BodyJoint.JointType.Attachment || joint.type == BodyJoint.JointType.BaseAttachment))
                {
                    if(joint.enabled)
                    {
                        transform.position += (transform.parent.position - joint.transform.position) * (Time.deltaTime * 5.0F);
                        transform.RotateAround(joint.transform.position, Vector3.forward, Mathf.DeltaAngle(joint.transform.rotation.eulerAngles.z, transform.parent.rotation.eulerAngles.z + 180.0F) * Time.deltaTime * 5.0F);
                    }

                   
                }
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

    public void SetAttributes(Attribute attributeAdded, float percentage, Type typeAdded)
    {
        PartAttributes newAttribute;
        newAttribute = new PartAttributes { attribute = attributeAdded, percent = percentage, type = typeAdded};
        attributes.Add(newAttribute);
    }
}
