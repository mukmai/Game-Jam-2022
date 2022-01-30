using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightReceiver : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> chargeParticles;
    [SerializeField] private SpriteRenderer colorRing;
    [EnumFlag] public ColorCode colorCode = ColorCode.Blue | ColorCode.Red | ColorCode.Yellow;
    protected float _lastNoHitTime;
    protected float _requiredHitDuration = 3;
    private LineRenderer _lr;

    public virtual bool IsWaveSensor => true;

    public virtual void Init()
    {
        gameObject.layer = 10;
        if (colorRing)
        {
            colorRing.color = colorCode.ToColor();
        }

        foreach (ParticleSystem particle in chargeParticles)
        {
            var startMain = particle.main;
            startMain.startColor = colorCode.ToColor();
        }

        HideChargeEffect();
        
        SetUpDecoTrail();
    }

    protected void HideChargeEffect()
    {
        foreach (ParticleSystem particle in chargeParticles)
        {
            particle.gameObject.SetActive(false);
        }
    }

    protected void ShowChargeEffect()
    {
        foreach (ParticleSystem particle in chargeParticles)
        {
            particle.gameObject.SetActive(true);
        }
    }

    private void SetUpDecoTrail()
    {
        if (!_lr)
        {
            _lr = gameObject.AddComponent<LineRenderer>();
        }
        _lr.startWidth = 0.2f;
        _lr.endWidth = 0.2f;
        float partSize = 0.25f;
        int parts = (int)((transform.position.z + GameplayManager.Instance._currGameLevel.zRadius) / partSize);
        _lr.positionCount = parts + 1;
        for (int i = 0; i < parts; i++)
        {
            _lr.SetPosition(i, new Vector3(transform.position.x, -0.45f, transform.position.z - partSize * i));
        }
        _lr.SetPosition(parts, new Vector3(transform.position.x, -0.45f, -GameplayManager.Instance._currGameLevel.zRadius));
        _lr.material = GameplayManager.Instance.receiverPathLineMaterial;
    }
    
    public virtual void ReceivingLight(Transform fromTarget)
    {
    }

    public virtual void UpdateReceiver()
    {
    }

    private void LateUpdate()
    {
        if (!ConditionFulfilledThisFrame())
        {
            _lastNoHitTime = Time.time;
        }

        float completeRatio = Mathf.Clamp01((Time.time - _lastNoHitTime) / _requiredHitDuration);
        if (completeRatio > 0)
        {
            ShowChargeEffect();
        }
        else
        {
            HideChargeEffect();
        }
        if (_lr)
        {
            GradientColorKey[] colorKey = new GradientColorKey[2];
            if (completeRatio == 0)
            {
                colorKey[0].color = colorCode.ToColor() * 0.2f;
                colorKey[0].time = 0.0f;
            }
            else if (completeRatio == 1)
            {
                colorKey[0].color = colorCode.ToColor();
                colorKey[0].time = 0.0f;
            }
            else
            {
                colorKey[0].color = colorCode.ToColor();
                colorKey[0].time = completeRatio;
                colorKey[1].color = colorCode.ToColor() * 0.2f;
                colorKey[1].time = completeRatio+0.01f;
            }

            GradientAlphaKey[] alphaKey = new GradientAlphaKey[2];
            alphaKey[0].alpha = 1.0f;
            alphaKey[0].time = 0.0f;
            alphaKey[1].alpha = 1.0f;
            alphaKey[1].time = 1.0f;
            Gradient lineGradient = new Gradient();
            lineGradient.SetKeys(colorKey, alphaKey);
            _lr.colorGradient = lineGradient;
        }
    }

    protected virtual bool ConditionFulfilledThisFrame()
    {
        return false;
    }

    public bool IsCompleted()
    {
        return _lastNoHitTime + _requiredHitDuration <= Time.time;
    }
}
