using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightReceiver : MonoBehaviour
{
    

    void Awake()
    {
        bool hit =false;
        gameObject.layer = 2;
    }

    public virtual void Reaction()
    {

    }

    public virtual bool ReceivingLight()
    {
        return false;
    }


}
