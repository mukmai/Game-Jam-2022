using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightReceiver : MonoBehaviour
{
    [SerializeField] private ParticleSystem _cloudParticles;
    Color _desirecolor = GetComponent<LightReceiver>().color;

    void Awake()
    {
        bool hit =false;
        gameObject.layer = 2;
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {

    }

    public virtual bool ReceivingLight()
    {
        return hit;
    }


}
