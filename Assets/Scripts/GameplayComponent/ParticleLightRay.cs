using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLightRay : LightRay
{
    public List<Particle> particles;
    public Vector3 startPos, endPos;
    private float _lastShootParticleTime;
    public static float shootParticleInterval = 0.2f;

    public ParticleLightRay()
    {
        particles = new List<Particle>();
    }
    
    public override void SetColor(ColorCode colorCode)
    {
        this.colorCode = colorCode;
    }

    //change end position of the particles
    public override void SetNewEnd(Vector3 newEndPos)
    {
        endPos = newEndPos;
    }

    //set new start
    public override void SetNewStart(Vector3 newStartPos)
    {
        base.SetNewStart(newStartPos);
    }

    // Update is called once per frame
    public override void UpdateLightRay()
    {
        if (_lastShootParticleTime + shootParticleInterval <= Time.time)
        {
            var currParticle = ObjectPool.Instance.CreateObject(GameplayManager.Instance.particleGameObject, 
                localPos: transform.position).GetComponent<Particle>();
            currParticle.Init(transform.forward, this);
            _lastShootParticleTime = Time.time;
        }
    }
}
