using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveReceiver : LightReceiver
{
    public override bool IsWaveSensor => true;
    public bool getHitThisFrame;

    public override void Init()
    {
        base.Init();
        getHitThisFrame = false;
    }

    public override void ReceivingLight(Transform fromTarget)
    {
        base.ReceivingLight(fromTarget);
        getHitThisFrame = true;
    }

    public override void UpdateReceiver()
    {
        base.UpdateReceiver();
        getHitThisFrame = false;
    }

    protected override bool ConditionFulfilledThisFrame()
    {
        return getHitThisFrame;
    }
}
