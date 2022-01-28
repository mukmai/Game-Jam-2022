using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLightRay : LightRay
{
    public List<Particle> particles;
    public float unitDist;
    public Vector3 startPos, endPos;

    public ParticleLightRay()
    {
        particles = new List<Particle>();
    }

    public void setNewEnd(Vector3 newEndPos)
    {
        endPos = newEndPos;
    }
     
    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, this.direction);
        RaycastHit hitData;
        if (Physics.Raycast(ray, out hitData))
        {

        }

    }
}
