using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParticleReceiver : LightReceiver
{
    public override bool IsWaveSensor => false;
    
    private Dictionary<Transform, float> _hitCounter;
    private LightRay _convertedWaveLightRay;
    private float particleHitPerSecondThreshold => 0.4f / ParticleLightRay.shootParticleInterval;

    public override void Init()
    {
        base.Init();
        _hitCounter = new Dictionary<Transform, float>();
    }

    public override void ReceivingLight(Transform fromTarget)
    {
        base.ReceivingLight(fromTarget);
        _hitCounter[fromTarget] = Time.time;
    }

    public override void UpdateReceiver()
    {
        base.UpdateReceiver();
        _hitCounter = _hitCounter.Where(pair => pair.Value >= Time.time - 1)
            .ToDictionary(pair => pair.Key, pair => pair.Value);
    }
    
    protected override bool ConditionFulfilledThisFrame()
    {
        return _hitCounter.Count >= particleHitPerSecondThreshold;
    }
}
