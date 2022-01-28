using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeDragObject : MonoBehaviour
{
    private Vector3 _mouseOffset;
    [SerializeField] private Rigidbody rigidbody;

    private bool _isControlling;
    
    void OnMouseDown()
    {
        if (GameplayManager.Instance.TryGrabTool(transform, false))
        {
            _isControlling = true;
            _mouseOffset = transform.position - HelperFunctions.GetMouseWorldPos();
            rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
            rigidbody.isKinematic = false;
        }
    }
    
    void OnMouseDrag()
    {
        if (_isControlling)
        {
            var dir = HelperFunctions.GetMouseWorldPos() - transform.position + _mouseOffset;
            float speed = 40;
            if (dir.magnitude < 0.05f)
            {
                speed = 0;
            }
            else if (dir.magnitude < 5)
            {
                speed *= dir.magnitude / 5;
            }

            rigidbody.velocity = dir.normalized * speed;
        }
    }

    private void OnMouseUp()
    {
        if (GameplayManager.Instance.TryReleaseTool(transform, false))
        {
            _isControlling = false;
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            rigidbody.isKinematic = true;
        }
    }
}
