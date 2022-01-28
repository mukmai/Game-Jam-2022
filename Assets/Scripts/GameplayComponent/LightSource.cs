using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
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

    public void UpdateLightSource()
    {
        if (!lightRay)
        {
            AddLightRay();
        }
        lightRay.UpdateLightRay();
    }
}
