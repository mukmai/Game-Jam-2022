using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : LightRayHitTarget
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void HandleWaveInteraction(WaveLightRay wave, Vector3 hitPosition, Vector3 hitDirection)
    {
        wave.SetNewEnd(hitPosition);
        wave.RemoveReflectionChild();
        wave.CreateOrUpdateReflectionChild(hitPosition, Vector3.Reflect(hitDirection, transform.forward).RemoveY());

        // tell wave to create or change child light ray color, start point, then update
    }

    public override void HandleParticleInteraction(Particle particle)
    {
        throw new System.NotImplementedException();
    }
}
