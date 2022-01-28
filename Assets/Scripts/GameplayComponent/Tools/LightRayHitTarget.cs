using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LightRayHitTarget : MonoBehaviour
{
    public abstract void HandleWaveInteraction(WaveLightRay wave, Vector3 hitPosition, Vector3 hitDirection);

    public abstract void HandleParticleInteraction(Particle particle);
}
