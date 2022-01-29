using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveReceiver : LightReceiver
{
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
}
