using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    [SerializeField] public Vector3 direction;

    public LightRay lightRay;

    //add wave/particle to source
    void addLightRay(Vector3 startPos, Vector3 lightDirection)
    {
        startPos = transform.position;
        lightDirection = this.direction;

    }

    public void UpdateLightSource()
    {
        if (!lightRay)
        {
            addLightRay(transform.position, transform.forward);
        }
        // TODO: call update function in lightray
    }
}
