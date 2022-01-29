using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightReceiver : MonoBehaviour
{
    [SerializeField] private ParticleSystem _cloudParticles;
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

    public virtual bool IsCompleted()
    {
        return false;
    }
}
