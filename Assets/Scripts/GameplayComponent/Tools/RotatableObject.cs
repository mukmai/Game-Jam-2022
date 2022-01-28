using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatableObject : MonoBehaviour
{
    private Vector3 _mouseOffset;
    private float _initRotateOffset;
    [SerializeField] private Rigidbody rigidbody;
    private bool _isControlling;
    
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) && GameplayManager.Instance.TryGrabTool(transform, true))
        {
            _isControlling = true;
            _mouseOffset = HelperFunctions.GetMouseWorldPos() - transform.position;
            rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationX |
                                    RigidbodyConstraints.FreezeRotationZ;
            rigidbody.isKinematic = false;
            _initRotateOffset = Vector3.SignedAngle(transform.forward, _mouseOffset, Vector3.up);
        }
    }

    private void Update()
    {
        if (_isControlling && Input.GetMouseButtonUp(1))
        {
            if (GameplayManager.Instance.TryReleaseTool(transform, true))
            {
                _isControlling = false;
                rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                rigidbody.isKinematic = true;
            }
        }
        
        if (_isControlling)
        {
            var rotationDegree = Vector3.SignedAngle(transform.forward,
                HelperFunctions.GetMouseWorldPos() - transform.position, Vector3.up) - _initRotateOffset;
        
            float speed = 720;
            if (Mathf.Abs(rotationDegree) < 30)
            {
                speed = 20 * Mathf.Abs(rotationDegree) / 30;
            }

            rigidbody.angularVelocity = new Vector3(0, Mathf.Sign(rotationDegree) * speed, 0);
        }
    }
}
