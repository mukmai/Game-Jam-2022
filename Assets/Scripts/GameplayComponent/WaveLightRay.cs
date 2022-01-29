using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveLightRay : LightRay
{
    
    LineRenderer line;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        
    }

    //set new start
    public override void SetNewStart(Vector3 newStartPos)
    {
        base.SetNewStart(newStartPos);
        line.SetPosition(0, newStartPos);
    }

    public override void SetNewEnd (Vector3 newEnd)
    {
        line.SetPosition(1, newEnd);
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
        if (Physics.Raycast(ray, out hitData))
        {
            //call hitObject function
            hitData.transform.GetComponent<LightRayHitTarget>().HandleWaveInteraction(this,hitData.point,transform.forward);

        }
        
        Ray rayCheckReceiver = new Ray(transform.position, transform.forward);
        RaycastHit[] hitReceivers;
        LayerMask  mask = LayerMask.GetMask("Default", "TransprntFX", "Water", "UI");
        float distance = Vector3.Distance(line.GetPosition(1), line.GetPosition(0));
        hitReceivers = Physics.RaycastAll(rayCheckReceiver, distance, mask);
        for (int i = 0; i < hitReceivers.Length; i++)
        {
            RaycastHit hit = hitReceivers[i];
            hit.transform.GetComponent<LightReceiver>().SetHit(true);
        }

        base.UpdateLightRay();
    }

}
