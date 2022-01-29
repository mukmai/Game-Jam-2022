using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightParticleSource : LightSource
{
    public LightRay lightRay;

    //add wave/particle to source
    void AddLightRay()
    {
        lightRay = ObjectPool.Instance.CreateObject(
            GameplayManager.Instance.particleLightRayGameObject).GetComponent<LightRay>();
        lightRay.SetColor(colorCode);
        lightRay.SetPower(_initialPower);
        lightRay.SetNewStart(transform.position);
        lightRay.SetNewDirection(transform.forward);
    }

    public override void UpdateLightSource()
    {
        if (!lightRay)
        {
            AddLightRay();
        }
        lightRay.UpdateLightRay();
    }
}
