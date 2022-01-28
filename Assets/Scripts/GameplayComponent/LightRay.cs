using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRay : MonoBehaviour
{
    public LightRay parent;
    public List<LightRay> children;
    public Material color;
    public Vector3 direction;

    //constructors
    public LightRay()
    {
        parent = null;
        this.children = new List<LightRay>();
    }

    //add new node
    public void add(LightRay lightRay)
    {
        this.children.Add(lightRay);
    }

    //remove lightRay as node, set inactive
    public void remove(LightRay lightRay)
    {
        if (lightRay.children.Count == 0)
        {
            children.remove(lightRay);
            return;
        }
        else
        {
            foreach (LightRay i in lightRay.children){remove(i);}
        }
    }

    //traverses function
    public void traverse(LightSource lightSource)
    {
        traverse(lightSource.lightRay);
    }

    public void traverse(LightRay lightRay)
    {
        lightRay.Update();
        if (lightRay.children.Count == 0)
        {
            return;
        }
        else
        {
            foreach (LightRay i in lightRay.children){traverse(i);}
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
