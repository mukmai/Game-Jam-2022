using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    [SerializeField] public Vector3 direction;

    public LightRay lightRay;

    //add wave/particle to source
    void AddLightRay()
    {
        ObjectPool.Instance.CreateObject(gameObject);
    }

    public void UpdateLightSource()
    {
        if (!lightRay)
        {
            AddLightRay();
        }
        // TODO: call update function in lightray
        lightRay.UpdateLightRay();
    }
}
