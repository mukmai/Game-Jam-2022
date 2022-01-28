using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRay : MonoBehaviour
{
    public LightRay parent;
    public LightRay reflectionChild;
    public List<LightRay> slitChildren;
    public Material mat;
    public Vector3 direction;

    //constructors
    public LightRay()
    {
        parent = null;
        slitChildren = new List<LightRay>();
    }

    //add new node
    public void Add(LightRay lightRay)
    {
        slitChildren.Add(lightRay);
    }

    //remove lightRay as node, set inactive
    public void RemoveSlitChildren()
    {
        foreach (LightRay i in slitChildren){
            i.RemoveSlitChildren();
            i.RemoveReflectionChild();
            i.Remove();
        }
        slitChildren.Clear();
    }

    public void RemoveReflectionChild()
    {
        if (reflectionChild)
        {
            reflectionChild.Remove();
        }

        reflectionChild = null;
    }

    public void Remove()
    {
        ObjectPool.Instance.DestroyObject(gameObject);
    }

    //set new end
    public virtual void SetNewEnd(Vector3 newEndPos)
    {
    }

    //set new start
    public virtual void SetNewStart(Vector3 newStartPos)
    {
        transform.position = newStartPos;
    }

    public virtual void SetNewDirection(Vector3 newDir)
    {
        transform.forward = newDir;
    }

    public virtual void CreateOrUpdateReflectionChild(Vector3 startPos, Vector3 direction)
    {
    }

    // Update is called once per frame
    public virtual void UpdateLightRay()
    {
        //end point update
        //start point update
        foreach (LightRay i in slitChildren)
        {
            UpdateLightRay();
        }

        if (reflectionChild)
        {
            reflectionChild.UpdateLightRay();
        }
        
    }
}
