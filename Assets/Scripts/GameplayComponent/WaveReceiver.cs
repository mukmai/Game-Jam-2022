using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveReceiver : LightReceiver
{
    public override bool checkReceivingLight()
    {
        Vector3[] collisionPoints = wave.GetPositions();
        foreach(Vector3 wavePoint in collisionPoints)
        {
            if(this.position == wavePoint.position)
            {
                Reaction();
                return true;
            }
        }
    }

    public override void Reaction()
    {
        _cloudParticles.Play();
    }
}
