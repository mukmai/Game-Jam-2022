using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightReceiver : MonoBehaviour
{
    [SerializeField] private ParticleSystem _cloudParticles;
    protected float _lastNoHitTime;
    protected float _requiredHitDuration = 3;

    public virtual bool IsWaveSensor => true;

    public virtual void Init()
    {
        gameObject.layer = 10; 
    }
    
    public virtual void ReceivingLight(Transform fromTarget)
    {
    }

    public virtual void UpdateReceiver()
    {
    }

    private void LateUpdate()
    {
        if (!ConditionFulfilledThisFrame())
        {
            _lastNoHitTime = Time.time;
        }
    }

    protected virtual bool ConditionFulfilledThisFrame()
    {
        return false;
    }

    public bool IsCompleted()
    {
        return _lastNoHitTime + _requiredHitDuration <= Time.time;
    }
}
