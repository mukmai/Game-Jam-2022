using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Flags] public enum ColorCode
{
    Red = 1,
    Yellow = 2,
    Blue = 4
}
public class LightRay : MonoBehaviour
{
    public LightRay parent;
    public LightRay reflectionChild;
    public LightRay converterChild;
    public List<LightRay> refractionChildren;
    public LightRay refractionOutChild;
    public List<LightRay> slitChildren;
    [EnumFlag] public ColorCode colorCode;
    public int power;

    //constructors
    public LightRay()
    {
        parent = null;
        slitChildren = new List<LightRay>();
        refractionChildren = new List<LightRay>();
    }
    
    public virtual void SetColor(ColorCode colorCode)
    {
        
    }

    public void SetPower(int val)
    {
        power = val;
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

    public void RemoveConverterChild()
    {
        if (converterChild)
        {
            converterChild.Remove();
        }

        converterChild = null;
    }

    public void RemoveRefractionChildren()
    {
        foreach (LightRay i in refractionChildren)
        {
            i.Remove();
        }
        refractionChildren.Clear();
        
        if (refractionOutChild)
        {
            refractionOutChild.Remove();
        }
        refractionOutChild = null;
    }

    public void Remove()
    {
        RemoveSlitChildren();
        RemoveReflectionChild();
        RemoveConverterChild();
        RemoveRefractionChildren();
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
    
    public virtual void CreateOrUpdateConverterChild(Vector3 startPos, Vector3 direction)
    {
    }


    public virtual void CreateOrUpdateSlitChildren(Vector3 startPos, Vector3 direction)
    {
    }

    public virtual void CreateOrUpdateRefractionChildren(List<Vector3> hitPositions, Vector3 outDirection)
    {
    }

    // Update is called once per frame
    public virtual void UpdateLightRay()
    {
        //end point update
        //start point update
        foreach (LightRay i in slitChildren)
        {
            i.UpdateLightRay();
        }

        if (reflectionChild)
        {
            reflectionChild.UpdateLightRay();
        }
        if (converterChild)
        {
            converterChild.UpdateLightRay();
        }
        if (refractionOutChild)
        {
            refractionOutChild.UpdateLightRay();
        }
    }
}

public class EnumFlagAttribute : PropertyAttribute
{
    public string name;

    public EnumFlagAttribute() { }

    public EnumFlagAttribute(string name)
    {
        this.name = name;
    }
}