using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAlongPathObject : MonoBehaviour
{
    private Vector3 _mouseOffset;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private List<Vector3> pathPoints;
    private LineRenderer _lr;

    private bool _isControlling;

    private void Start()
    {
        SetUpPath();
        MoveToClosetPointOnPath();
    }

    private void SetUpPath()
    {
        if (!_lr)
        {
            _lr = gameObject.AddComponent<LineRenderer>();
        }
        _lr.startWidth = 0.2f;
        _lr.endWidth = 0.2f;
        _lr.positionCount = pathPoints.Count;
        for (int i = 0; i < pathPoints.Count; i++)
        {
            _lr.SetPosition(i, pathPoints[i]);
        }
    }

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

    private void LateUpdate()
    {
        if (!rigidbody.isKinematic)
        {
            MoveToClosetPointOnPath();
        }
    }

    private void MoveToClosetPointOnPath()
    {
        float closestDist = 999;
        Vector3 closestPoint = Vector3.zero;
        for (int i = 0; i < pathPoints.Count - 1; i++)
        {
            Vector3 p1 = pathPoints[i];
            Vector3 p2 = pathPoints[i+1];
            Vector3 closestPointWithinP1P2 = GetClosestPoint(p1, p2, transform.position);
            float distToClosestPoint = Vector3.Distance(closestPointWithinP1P2, transform.position);
            if (distToClosestPoint < closestDist)
            {
                closestDist = distToClosestPoint;
                closestPoint = closestPointWithinP1P2;
            }
        }

        transform.position = closestPoint;

    }

    private Vector3 GetClosestPoint(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        var p1_p3 = p3 - p1;
        var p1_p2 = p2 - p1;
        float dot = Vector3.Dot(p1_p3, p1_p2.normalized);
        dot /= p1_p2.magnitude;
        return p1 + p1_p2 * Mathf.Clamp01(dot);
    }

    private void OnMouseUp()
    {
        if (GameplayManager.Instance.TryReleaseTool(transform, false))
        {
            _isControlling = false;
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            rigidbody.isKinematic = true;
            MoveToClosetPointOnPath();
        }
    }
    
    void OnDrawGizmosSelected()
    {
 
#if UNITY_EDITOR
        Gizmos.color = Color.red;
 
        for (int i = 0; i < pathPoints.Count - 1; i++)
        {
            Gizmos.DrawLine(pathPoints[i], pathPoints[i+1]);
        }
#endif
    }
}
