using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : LightRayHitTarget
{
    public override void HandleWaveInteraction(WaveLightRay wave, Vector3 hitPosition, Vector3 hitDirection, Vector3 normalDirection)
    {
        // tell wave to remove all children
        wave.SetNewEnd(hitPosition);
        wave.RemoveReflectionChild();
        wave.RemoveSlitChildren();
        wave.RemoveConverterChild();
        wave.RemoveRefractionChildren();
    }

    public override void HandleParticleInteraction(Particle particle, Vector3 normalDirection)
    {
        particle.Remove();
    }
}
