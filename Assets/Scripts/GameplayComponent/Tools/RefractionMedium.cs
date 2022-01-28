using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefractionMedium : LightRayHitTarget
{
    public override void HandleWaveInteraction(WaveLightRay wave, Vector3 hitPosition, Vector3 hitDirection)
    {
        throw new System.NotImplementedException();
    }

    public override void HandleParticleInteraction(Particle particle)
    {
        throw new System.NotImplementedException();
    }
}
