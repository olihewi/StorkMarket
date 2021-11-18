using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointRenderer : MonoBehaviour
{
    public static JointRenderer INSTANCE;
    public SpriteRenderer jointSpritePrefab;
    public Color slotColour;
    public Color attachmentColour;
    private List<SpriteRenderer> drawnJoints = new List<SpriteRenderer>();
    private void Start()
    {
        INSTANCE = this;
    }
    private void Update()
    {
        // Change colour when hovered over
    }

    public void DisplayValidJoints(BodyJoint _joint)
    {
        foreach (BodyPart part in BodyPart.BODY_PARTS)
        {
            foreach (BodyJoint otherJoint in part.joints)
            {
                if (_joint.IsCompatibleSlot(otherJoint))
                {
                    SpriteRenderer slotSprite = Instantiate(jointSpritePrefab, otherJoint.transform.position, Quaternion.identity, otherJoint.transform);
                    slotSprite.color = slotColour;
                    drawnJoints.Add(slotSprite);
                }
            }
        }
        SpriteRenderer attachmentSprite = Instantiate(jointSpritePrefab, _joint.transform.position, Quaternion.identity, _joint.transform);
        attachmentSprite.color = attachmentColour;
        drawnJoints.Add(attachmentSprite);
    }

    public void Clear()
    {
        foreach (SpriteRenderer drawnJoint in drawnJoints)
        {
            Destroy(drawnJoint.gameObject);
        }
        drawnJoints.Clear();
    }
}
