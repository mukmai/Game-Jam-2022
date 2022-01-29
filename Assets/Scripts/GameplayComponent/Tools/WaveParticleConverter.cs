using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveParticleConverter : LightRayHitTarget
{
    [SerializeField] private Transform childShootTransform;

    private List<float> _hitCounter;
    private LightRay _convertedWaveLightRay;
    private float WaveLightRayCreateThreshold => 0.75f / ParticleLightRay.shootParticleInterval;
    private int _hitParticlePower;
    private ColorCode _hitParticleColorCode;
    private LightRay _currWaveLightSource;
    private LightRay _currParticleLightSource;
    private bool _currLightSourceHitThisFrame;

    public void Init()
    {
        _hitCounter = new List<float>();
        _currWaveLightSource = null;
    }
    
    public override void HandleWaveInteraction(WaveLightRay wave, Vector3 hitPosition, Vector3 hitDirection, Vector3 normalDirection)
    {
        wave.SetNewEnd(hitPosition);
        wave.RemoveReflectionChild();
        wave.RemoveSlitChildren();
        wave.RemoveRefractionChildren();
        if ((!_currParticleLightSource && !_currWaveLightSource) || _currWaveLightSource == wave)
        {
            _currWaveLightSource = wave;
            _currLightSourceHitThisFrame = true;
            wave.CreateOrUpdateConverterChild(childShootTransform.position, childShootTransform.forward);
        }
        else
        {
            wave.RemoveConverterChild();
        }
    }

    public override void HandleParticleInteraction(Particle particle, Vector3 normalDirection)
    {
        particle.Remove();
        if ((!_currParticleLightSource && !_currWaveLightSource) || _currParticleLightSource == particle.sourceLightRay)
        {
            _currParticleLightSource = particle.sourceLightRay;
            _hitCounter.Add(Time.time);
            _hitParticlePower = particle.power;
            _hitParticleColorCode = particle.colorCode;
        }
    }
    
    private void CreateOrUpdateConvertedWaveLightRay(Vector3 startPos,Vector3 direction)
    {
        if (!_convertedWaveLightRay)
        {
            _convertedWaveLightRay = ObjectPool.Instance.CreateObject(
                GameplayManager.Instance.waveLightRayGameObject).GetComponent<LightRay>();
        }
        
        _convertedWaveLightRay.SetColor(_hitParticleColorCode);
        _convertedWaveLightRay.SetPower(_hitParticlePower - 1);
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

    public void PreUpdateConverter()
    {
        _currLightSourceHitThisFrame = false;
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

        if (_hitCounter.Count == 0)
        {
            _currParticleLightSource = null;
        }

        if (!_currLightSourceHitThisFrame)
        {
            _currWaveLightSource = null;
        }
    }
}
