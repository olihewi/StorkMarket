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
    public Color hoverColour;
    private List<KeyValuePair<BodyJoint,SpriteRenderer>> drawnJoints = new List<KeyValuePair<BodyJoint,SpriteRenderer>>();
    private void Start()
    {
        INSTANCE = this;
    }
    private void Update()
    {
        foreach (KeyValuePair<BodyJoint, SpriteRenderer> drawnJoint in drawnJoints)
        {
            drawnJoint.Value.color = drawnJoint.Key.type == BodyJoint.JointType.Attachment || drawnJoint.Key.type == BodyJoint.JointType.BaseAttachment ? attachmentColour : slotColour;
        }
        for (int x = 0; x < drawnJoints.Count; x++)
        {
            for (int y = 0; y < x; y++)
            {
                KeyValuePair<BodyJoint,SpriteRenderer> a = drawnJoints[x];
                KeyValuePair<BodyJoint,SpriteRenderer> b = drawnJoints[y];
                if (a.Key.CanAttach(b.Key))
                {
                    a.Value.color = hoverColour;
                    b.Value.color = hoverColour;
                }
            }
        }
    }

    public void DisplayValidJoints(BodyJoint _joint)
    {
        foreach (BodyPart part in BodyPart.BODY_PARTS)
        {
            foreach (BodyJoint otherJoint in part.joints)
            {
                if (otherJoint.transform.parent == _joint.transform.parent) continue;
                if (otherJoint.isAttached) continue;
                if (!_joint.IsCompatibleSlot(otherJoint)) continue;
                SpriteRenderer slotSprite = Instantiate(jointSpritePrefab, otherJoint.transform.position, Quaternion.identity, otherJoint.transform);
                slotSprite.color = slotColour;
                drawnJoints.Add(new KeyValuePair<BodyJoint, SpriteRenderer>(otherJoint,slotSprite));
            }
        }
        SpriteRenderer attachmentSprite = Instantiate(jointSpritePrefab, _joint.transform.position, Quaternion.identity, _joint.transform);
        attachmentSprite.color = attachmentColour;
        drawnJoints.Add(new KeyValuePair<BodyJoint, SpriteRenderer>(_joint,attachmentSprite));
    }

    public void Clear()
    {
        foreach (KeyValuePair<BodyJoint,SpriteRenderer> drawnJoint in drawnJoints)
        {
            Destroy(drawnJoint.Value.gameObject);
        }
        drawnJoints.Clear();
    }
}
