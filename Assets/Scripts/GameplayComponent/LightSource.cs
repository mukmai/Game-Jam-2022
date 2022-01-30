using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    [EnumFlag] public ColorCode colorCode = ColorCode.Blue | ColorCode.Red | ColorCode.Yellow;
    protected int _initialPower = 8;

    public virtual void UpdateLightSource()
    {
    }
}
