using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightReceiver : MonoBehaviour
{
    [SerializeField] private ParticleSystem _cloudParticles;
    bool hit;

    void Awake()
    {
        hit =false;
        gameObject.layer =  LayerMask.GetMask("Ignore Raycast");
    }

    public virtual void Reaction()
    {
        _cloudParticles.Play();
    }

    public virtual void SetHit(bool flag)
    {
        if (!hit && flag) Reaction();
        hit = flag;
    }

    public virtual bool ReceivingLight()
    {
        return hit;
    }

    private void UpdateReceiver()
    {

    }

}
