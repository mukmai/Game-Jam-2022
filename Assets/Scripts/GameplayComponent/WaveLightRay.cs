using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveLightRay : LightRay
{
    
    LineRenderer line;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    void setNewEnd (Vector3 newEnd)
    {
        line.SetPosition(1, newEnd);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, this.direction);
        RaycastHit hitData;
        if (Physics.Raycast(ray, out hitData))
        {
            //call hitObject function
        }

    }



}
