using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : LightRayHitTarget
{
    public override void HandleWaveInteraction(WaveLightRay wave, Vector3 hitPosition, Vector3 hitDirection, Vector3 normalDirection)
    {
        wave.SetNewEnd(hitPosition);
        wave.RemoveSlitChildren();
        wave.RemoveConverterChild();
        wave.RemoveRefractionChildren();
        wave.CreateOrUpdateReflectionChild(hitPosition, Vector3.Reflect(hitDirection, normalDirection).RemoveY());
    }

    public override void HandleParticleInteraction(Particle particle)
    {
        var reflectedDir = Vector3.Reflect(particle.transform.forward,
            transform.forward).RemoveY();
        particle.UpdateMoveDirection(reflectedDir);
    }
}
