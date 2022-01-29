using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveLightRay : LightRay
{
    [SerializeField] private VolumetricLines.VolumetricLineBehavior line;

    private Vector3 _currEndPoint;

    //set new start
    public override void SetNewStart(Vector3 newStartPos)
    {
        base.SetNewStart(newStartPos);
        line.StartPos = transform.InverseTransformPoint(newStartPos);
    }

    public override void SetNewEnd (Vector3 newEnd)
    {
        line.EndPos = transform.InverseTransformPoint(newEnd);
        _currEndPoint = newEnd;
    }

    public override void SetColor(ColorCode colorCode)
    {
        this.colorCode = colorCode;
        line.LineColor = colorCode.ToColor();
    }

    public override void CreateOrUpdateReflectionChild(Vector3 startPos,Vector3 direction)
    {
        if (!reflectionChild)
        {
            if (power > 0)
            {
                reflectionChild = ObjectPool.Instance.CreateObject(
                    GameplayManager.Instance.waveLightRayGameObject).GetComponent<LightRay>();
            }
        }

        if (reflectionChild)
        {
            reflectionChild.SetColor(colorCode);
            reflectionChild.SetPower(power - 1);
            reflectionChild.SetNewStart(startPos);
            reflectionChild.SetNewDirection(direction);
        }
    }

    public override void CreateOrUpdateConverterChild(Vector3 startPos, Vector3 direction)
    {
        if (!converterChild)
        {
            if (power > 0)
            {
                converterChild = ObjectPool.Instance.CreateObject(
                    GameplayManager.Instance.particleLightRayGameObject).GetComponent<LightRay>();
            }
        }

        if (converterChild)
        {
            converterChild.SetColor(colorCode);
            converterChild.SetPower(power - 1);
            converterChild.SetNewStart(startPos);
            converterChild.SetNewDirection(direction);
        }
    }

    public override void CreateOrUpdateRefractionChildren(Vector3 inPosition, Vector3 outPosition, Vector3 outDirection)
    {
        if (refractionChildren.Count==0)
        {
            if (power > 0)
            {
                refractionChildren.Add(ObjectPool.Instance.CreateObject(
                    GameplayManager.Instance.waveLightRayGameObject).GetComponent<LightRay>());
                refractionChildren.Add(ObjectPool.Instance.CreateObject(
                    GameplayManager.Instance.waveLightRayGameObject).GetComponent<LightRay>());
            }
        }

        if (refractionChildren.Count > 0)
        {
            refractionChildren[0].SetColor(colorCode);
            refractionChildren[0].SetPower(power - 1);
            refractionChildren[0].SetNewStart(inPosition);
            refractionChildren[0].SetNewEnd(outPosition);
        
            refractionChildren[1].SetColor(colorCode);
            refractionChildren[1].SetPower(power - 2);
            refractionChildren[1].SetNewStart(outPosition);
            refractionChildren[1].SetNewDirection(outDirection);
        }
    }

    public override void CreateOrUpdateSlitChildren(Vector3 startPos, Vector3 direction)
    {
        if (slitChildren.Count == 0)
        {
            if (power > 0)
            {
                for (int i=0; i<3; i++)
                {
                    slitChildren.Add(ObjectPool.Instance.CreateObject(
                        GameplayManager.Instance.waveLightRayGameObject).GetComponent<LightRay>());
                }
            }
        }

        if (slitChildren.Count > 0)
        {
            for (int i = 0; i < 3; i++)
            {
                slitChildren[i].SetPower(power - 1);
                slitChildren[i].SetNewStart(startPos);
            }

            slitChildren[0].SetNewDirection(Quaternion.Euler(0, 45, 0) * direction);
            slitChildren[1].SetNewDirection(direction);
            slitChildren[2].SetNewDirection(Quaternion.Euler(0, -45, 0) * direction);

            slitChildren[0].SetColor(ColorCode.Red);
            slitChildren[1].SetColor(ColorCode.Yellow);
            slitChildren[2].SetColor(ColorCode.Blue);
        }
    }

    // Update is called once per frame
    public override void UpdateLightRay()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitData;
        int receiverMask = 1 << 10;
        int particleMask = 1 << 2;
        int gravityFieldMask = 1 <<11;
        if (Physics.Raycast(ray, out hitData, 100, ~(receiverMask | particleMask | gravityFieldMask)))
        {
            //call hitObject function
            hitData.transform.GetComponent<LightRayHitTarget>().
                HandleWaveInteraction(this,hitData.point,transform.forward, hitData.normal);
        }
        
        Ray rayCheckReceiver = new Ray(transform.position, transform.forward);
        RaycastHit[] hitReceivers;
        float distance = Vector3.Distance(transform.position, _currEndPoint);
        hitReceivers = Physics.RaycastAll(rayCheckReceiver, distance, receiverMask);
        for (int i = 0; i < hitReceivers.Length; i++)
        {
            RaycastHit hit = hitReceivers[i];
            LightReceiver hitReceiver = hit.transform.GetComponent<LightReceiver>();
            if (hitReceiver && hitReceiver.IsWaveSensor)
            {
                // Debug.Log("hit " + hitReceiver.name);
                hitReceiver.ReceivingLight(transform);
            }
        }
        
        base.UpdateLightRay();
    }

}
