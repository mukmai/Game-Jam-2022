using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveLightRay : LightRay
{
    [SerializeField] private VolumetricLines.VolumetricLineBehavior line;

    private Vector3 _currEndPoint;

    // private void Start()
    // {
    //     line.LineColor = new Color(Random.Range(0,1f), Random.Range(0,1f), Random.Range(0,1f));
    // }
    
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


    public override void CreateOrUpdateReflectionChild(Vector3 startPos,Vector3 direction)
    {
        if (!reflectionChild)
        {
            reflectionChild = ObjectPool.Instance.CreateObject(GameplayManager.Instance.waveLightRayGameObject).GetComponent<LightRay>();
        }
        
        reflectionChild.SetNewStart(startPos);
        reflectionChild.SetNewDirection(direction);
    }

    public override void CreateOrUpdateConverterChild(Vector3 startPos, Vector3 direction)
    {
        if (!converterChild)
        {
            converterChild = ObjectPool.Instance.CreateObject(GameplayManager.Instance.particleLightRayGameObject).GetComponent<LightRay>();
        }
        
        converterChild.SetNewStart(startPos);
        converterChild.SetNewDirection(direction);
    }

    public override void CreateOrUpdateSlitChildren(Vector3 startPos, Vector3 direction)
    {
        if (slitChildren.Count == 0)
        {
            for (int i=0; i<3; i++)
            {
                slitChildren.Add(ObjectPool.Instance.CreateObject(GameplayManager.Instance.waveLightRayGameObject).GetComponent<LightRay>());
            }
        }

        for (int i = 0; i < 3; i++)
        {
            slitChildren[i].SetNewStart(startPos);
        }

        slitChildren[0].SetNewDirection(Quaternion.Euler(0, 45, 0) * direction);
        slitChildren[1].SetNewDirection(direction);
        slitChildren[2].SetNewDirection(Quaternion.Euler(0, -45, 0) * direction);

        //TODO cases for different color
        
    }

    // Update is called once per frame
    public override void UpdateLightRay()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitData;
        int receiverMask = 1 << 10;
        int particleMask = 1 << 2;
        if (Physics.Raycast(ray, out hitData, 100, ~(receiverMask | particleMask)))
        {
            //call hitObject function
            hitData.transform.GetComponent<LightRayHitTarget>().HandleWaveInteraction(this,hitData.point,transform.forward);
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
