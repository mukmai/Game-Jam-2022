using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefractionMedium : LightRayHitTarget
{
    public float refractionIndex;//1.2 or 2 or 5
    private float CriticalAngle => Mathf.Asin(1 / refractionIndex) * Mathf.Rad2Deg;


    public override void HandleWaveInteraction(WaveLightRay wave, Vector3 hitPosition, Vector3 hitDirection, Vector3 normalDirection)
    {
        wave.SetNewEnd(hitPosition);
        wave.RemoveReflectionChild();
        wave.RemoveSlitChildren();
        wave.RemoveConverterChild();
        
        List<Vector3> hitPoints = new List<Vector3>();
        hitPoints.Add(hitPosition);
        Vector3 outDirection = Vector3.zero;
        
        Vector3 refractedDir = Refraction(1, refractionIndex, normalDirection, hitDirection);

        bool withinBlock = true;

        for (int power = 0; power < wave.power && withinBlock; power++)
        {
            Ray ray = new Ray(hitPoints[hitPoints.Count-1] + refractedDir * 20, -refractedDir);
            RaycastHit[] hitObjects;
            int receiverMask = 1 << 10;
            int particleMask = 1 << 2;
            int gravityFieldMask = 1 <<11;
            hitObjects = Physics.RaycastAll(ray, 20, ~(receiverMask | particleMask | gravityFieldMask));
            for (int i = 0; i < hitObjects.Length; i++)
            {
                RaycastHit hit = hitObjects[i];
                if (hit.transform == transform)
                {
                    hitPoints.Add(hit.point);
                    float inAngle = Vector3.Angle(hit.normal, refractedDir);
                    if (inAngle >= CriticalAngle)
                    {
                        // total internal reflection
                        refractedDir = Vector3.Reflect(refractedDir, -hit.normal);
                    }
                    else
                    {
                        // refract out
                        refractedDir = Refraction(refractionIndex, 1, -hit.normal, refractedDir);
                        outDirection = refractedDir;

                        withinBlock = false;
                    }

                    break;
                }
            }
        }
        
        wave.CreateOrUpdateRefractionChildren(hitPoints, outDirection);
    }

    public override void HandleParticleInteraction(Particle particle, Vector3 normalDirection)
    {
        if (!particle.withinRefractionMedium)
        {
            Vector3 refractedDir = Refraction(1, refractionIndex, normalDirection, 
                particle.transform.forward);
            particle.UpdateMoveDirection(refractedDir);
            particle.withinRefractionMedium = true;
        }
        else
        {
            float inAngle = Vector3.Angle(normalDirection, particle.transform.forward);
            if (inAngle >= CriticalAngle)
            {
                // total internal reflection
                var reflectedDir = Vector3.Reflect(particle.transform.forward,
                    -normalDirection).RemoveY();
                particle.UpdateMoveDirection(reflectedDir);
            }
            else
            {
                // refract out
                Vector3 refractedDir = Refraction(refractionIndex, 1, -normalDirection, 
                    particle.transform.forward);
                particle.UpdateMoveDirection(refractedDir);
                particle.withinRefractionMedium = false;
            }
        }
    }
    
    public Vector3 Refraction(float ri1, float ri2, Vector3 surfNorm, Vector3 inDir)
    {
        surfNorm.Normalize();
        inDir.Normalize();

        return (ri1/ri2 * Vector3.Cross(surfNorm, 
            Vector3.Cross(-surfNorm, inDir)) - surfNorm * Mathf.Sqrt(1 - Vector3.Dot(
            Vector3.Cross(surfNorm, inDir)*(ri1/ri2*ri1/ri2), Vector3.Cross(surfNorm, inDir)))).
            normalized;
    }

}
