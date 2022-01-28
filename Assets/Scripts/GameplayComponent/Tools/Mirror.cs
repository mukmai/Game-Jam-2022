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
        throw new System.NotImplementedException();
    }

    public override void HandleParticleInteraction(Particle particle)
    {
        throw new System.NotImplementedException();
    }
}
