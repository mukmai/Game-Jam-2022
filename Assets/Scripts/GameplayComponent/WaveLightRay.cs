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

    public Vector3[] GetPositions()
    {
        Vector3[] position = new Vector3[line.positionCount];
        line.GetPositions(position);
        return position;
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
        
        base.UpdateLightRay();
    }



}
