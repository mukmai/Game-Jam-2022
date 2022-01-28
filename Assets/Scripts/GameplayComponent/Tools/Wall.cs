using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : LightRayHitTarget
{
    public override void HandleWaveInteraction(WaveLightRay wave, Vector3 hitPosition, Vector3 hitDirection)
    {
        // tell wave to remove all children
        wave.setNewEnd(hitPosition);
        wave.RemoveReflectionChild();
        wave.RemoveSlitChildren();
    }

    public override void HandleParticleInteraction(Particle particle)
    {
        throw new System.NotImplementedException();
    }
}
