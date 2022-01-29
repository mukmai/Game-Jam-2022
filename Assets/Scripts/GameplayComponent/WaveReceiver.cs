using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveReceiver : LightReceiver
{
    public override bool IsWaveSensor => true;
    private float _lastNoHitTime;
    private float _requiredHitDuration = 3;
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

    private void LateUpdate()
    {
        if (!getHitThisFrame)
        {
            _lastNoHitTime = Time.time;
        }
    }

    public override bool IsCompleted()
    {
        return _lastNoHitTime + _requiredHitDuration <= Time.time;
    }
}
