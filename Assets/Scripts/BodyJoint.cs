using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyJoint : MonoBehaviour
{
    public enum JointType
    {
        Slot,
        Attachment,
        BaseSlot,
        BaseAttachment
    }
    
    public JointType type;
    [HideInInspector] public bool isAttached = false;

    public bool IsCompatibleSlot(BodyJoint _joint)
    {
        return type == JointType.Attachment && _joint.type == JointType.Slot ||
               type == JointType.BaseAttachment && _joint.type == JointType.BaseSlot;
    }
    public bool CanAttach(BodyJoint _joint)
    {
        return !_joint.isAttached && IsCompatibleSlot(_joint) &&
               (transform.position - _joint.transform.position).magnitude < 0.5F;
    }
    public void Attach(BodyJoint _joint)
    {
        transform.parent.parent = _joint.transform;
        Rigidbody2D rb = transform.parent.GetComponent<Rigidbody2D>();
        if (rb == null) return;
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0.0F;
        _joint.isAttached = true;
        isAttached = true;
    }

    public void Detach()
    {
        if (transform.parent.parent != null)
        {
            BodyJoint _joint = transform.parent.parent.GetComponent<BodyJoint>();
            if (_joint != null)
            {
                _joint.isAttached = false;
            }
        }
        transform.parent.parent = null;
        isAttached = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = type == JointType.Attachment || type == JointType.BaseAttachment ? Color.red : Color.blue;
        Gizmos.DrawSphere(transform.position,0.25F);
    }
}
