using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeDragObject : MonoBehaviour
{
    private Vector3 _mouseOffset;
    
    void OnMouseDown()
    {
        _mouseOffset = transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        // mousePoint = new Vector3(mousePoint.x, 0, mousePoint.y);

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    
    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + _mouseOffset;
    }
}
