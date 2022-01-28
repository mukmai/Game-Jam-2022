using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightReceiver : MonoBehaviour
{
    [SerializeField] private ParticleSystem _cloudParticles;
    //Color _desirecolor = GetComponent<LightReceiver>().color;
    void Start()
    {
        bool _hitByWave =false;
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        WaveLightRay wave = collision.collider.GetComponent<WaveLightRay>();
        //Vector3[] collisionPoints = wave.GetPositions();
        if(wave != null) 
        {
            _cloudParticles.Play();
            _hitByWave =true;
        }
        else
        {
            _hitByWave = false;
        }
        return;
    }

    public virtual bool ReceivingLight()
    {
        return _hitByWave;
    }


}
