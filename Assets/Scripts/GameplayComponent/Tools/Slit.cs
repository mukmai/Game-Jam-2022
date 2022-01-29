using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slit : LightRayHitTarget
{
    [SerializeField] private Transform outTransform;
    public override void HandleWaveInteraction(WaveLightRay wave, Vector3 hitPosition, Vector3 hitDirection)
    {
        wave.SetNewEnd(hitPosition);
        wave.CreateOrUpdateSlitChildren(outTransform.position, -transform.forward);
        wave.RemoveReflectionChild();
        wave.RemoveConverterChild();
    }

    public override void HandleParticleInteraction(Particle particle)
    {
    }
}
