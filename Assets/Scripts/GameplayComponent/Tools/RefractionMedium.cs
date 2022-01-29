using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefractionMedium : LightRayHitTarget
{
    public float refractionIndex;//1.2 or 2 or 5
    public float mediumLength; 

    public override void HandleWaveInteraction(WaveLightRay wave, Vector3 hitPosition, Vector3 hitDirection, Vector3 normalDirection)
    {
        // float angle;
        // float moveDist;
        // Vector3 newEnd;
        // mediumLength = transform.localScale.z;
        // if (hitDirection.y > -transform.forward.y)//wave shooting downward
        // {
        //     angle = (int) Math.Sinh (Math.Sin(hitDirection.y + transform.forward.y)/refractionIndex);
        //     moveDist = (float) Math.Sqrt(Math.Pow(Math.Tan(angle) * mediumLength,2) + Math.Pow(mediumLength,2));
        //     newEnd = hitPosition + Quaternion.Euler(0, angle, 0) * (-transform.forward) * moveDist;
        //
        //     wave.SetNewEnd(hitPosition);
        //     wave.RemoveSlitChildren();
        //     wave.RemoveConverterChild();
        //     wave.RemoveReflectionChild();
        //     wave.CreateOrUpdateRefractionChildren(hitPosition, newEnd,hitDirection);
        //
        // } else if (hitDirection.y < -transform.forward.y)//wave shooting upward
        // {
        //     angle = (int) Math.Sinh(Math.Sin(hitDirection.y + transform.forward.y) / refractionIndex);
        //     moveDist = (float)Math.Sqrt(Math.Pow(Math.Tan(angle) * mediumLength, 2) + Math.Pow(mediumLength, 2));
        //     newEnd = hitPosition + Quaternion.Euler(0, -angle, 0) * (-transform.forward) * moveDist;
        //
        //     wave.SetNewEnd(hitPosition);
        //     wave.RemoveSlitChildren();
        //     wave.RemoveConverterChild();
        //     wave.RemoveReflectionChild();
        //     wave.CreateOrUpdateRefractionChildren(hitPosition, newEnd,hitDirection);
        // }
        
    }

    public override void HandleParticleInteraction(Particle particle)
    {
    }
}
