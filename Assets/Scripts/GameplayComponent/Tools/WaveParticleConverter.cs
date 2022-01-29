using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveParticleConverter : LightRayHitTarget
{
    [SerializeField] private Transform childShootTransform;

    private List<float> _hitCounter;
    private LightRay _convertedWaveLightRay;
    private float WaveLightRayCreateThreshold => 0.75f / ParticleLightRay.shootParticleInterval;

    public void Init()
    {
        _hitCounter = new List<float>();
    }
    
    public override void HandleWaveInteraction(WaveLightRay wave, Vector3 hitPosition, Vector3 hitDirection)
    {
        wave.SetNewEnd(hitPosition);
        wave.RemoveReflectionChild();
        wave.RemoveSlitChildren();
        wave.CreateOrUpdateConverterChild(childShootTransform.position, childShootTransform.forward);
    }

    public override void HandleParticleInteraction(Particle particle)
    {
        particle.Remove();
        // calculate particle/sec
        _hitCounter.Add(Time.time);
    }
    
    private void CreateOrUpdateConvertedWaveLightRay(Vector3 startPos,Vector3 direction)
    {
        if (!_convertedWaveLightRay)
        {
            _convertedWaveLightRay = ObjectPool.Instance.CreateObject(GameplayManager.Instance.waveLightRayGameObject).GetComponent<LightRay>();
        }
        
        _convertedWaveLightRay.SetNewStart(startPos);
        _convertedWaveLightRay.SetNewDirection(direction);
    }
    
    private void RemoveConvertedWaveLightRay()
    {
        if (_convertedWaveLightRay)
        {
            _convertedWaveLightRay.Remove();
        }

        _convertedWaveLightRay = null;
    }

    public void UpdateConverter()
    {
        if (_convertedWaveLightRay)
        {
            _convertedWaveLightRay.UpdateLightRay();
        }
        _hitCounter.RemoveAll(hitTime => hitTime < Time.time - 1);
        if (_hitCounter.Count < WaveLightRayCreateThreshold)
        {
            RemoveConvertedWaveLightRay();
        }
        else
        {
            CreateOrUpdateConvertedWaveLightRay(childShootTransform.position, transform.forward);
        }
    }
}
