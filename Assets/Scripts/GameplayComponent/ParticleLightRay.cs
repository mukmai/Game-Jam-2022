using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLightRay : LightRay
{
    public List<Particle> particles;
    public float unitDist = 10;
    public Vector3 startPos, endPos;

    public ParticleLightRay()
    {
        particles = new List<Particle>();
    }

    //chang end position of the particles
    public override void SetNewEnd(Vector3 newEndPos)
    {
        endPos = newEndPos;

    }

    //set new start
    public override void SetNewStart(Vector3 newStartPos)
    {
        base.SetNewStart(newStartPos);
    }


    // Update is called once per frame
    public override void UpdateLightRay()
    {
        Ray ray = new Ray(transform.position, this.direction);
        RaycastHit hitData;
        if (Physics.Raycast(ray, out hitData))
        {

        }

    }
}
