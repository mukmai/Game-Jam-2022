using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightWaveSource : LightSource
{
    public LightRay lightRay;

    //add wave/particle to source
    void AddLightRay()
    {
        lightRay = ObjectPool.Instance.CreateObject(
            GameplayManager.Instance.waveLightRayGameObject).GetComponent<LightRay>();
        lightRay.SetNewStart(transform.position);
        lightRay.SetNewDirection(transform.forward);
    }

    public override void UpdateLightSource()
    {
        if (!lightRay)
        {
            AddLightRay();
        }
        lightRay.SetNewStart(transform.position);
        lightRay.SetNewDirection(transform.forward);
        lightRay.UpdateLightRay();
    }
}
