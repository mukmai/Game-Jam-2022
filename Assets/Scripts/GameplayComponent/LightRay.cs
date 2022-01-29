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
    public List<LightRay> slitChildren;
    [EnumFlag] public ColorCode colorCode;

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

    public virtual void CreateOrUpdateRefractionChildren(Vector3 startPos, Vector3 endPos, Vector3 direction)
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
        if (refractionChildren[1])
        {
            refractionChildren[1].UpdateLightRay();
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

[CustomPropertyDrawer(typeof(EnumFlagAttribute))]
public class EnumFlagDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EnumFlagAttribute flagSettings = (EnumFlagAttribute)attribute;
        Enum targetEnum = (Enum)fieldInfo.GetValue(property.serializedObject.targetObject);

        string propName = flagSettings.name;
        if (string.IsNullOrEmpty(propName))
            propName = ObjectNames.NicifyVariableName(property.name);

        EditorGUI.BeginProperty(position, label, property);
        Enum enumNew = EditorGUI.EnumMaskPopup(position, propName, targetEnum);
        property.intValue = (int)Convert.ChangeType(enumNew, targetEnum.GetType());
        EditorGUI.EndProperty();
    }
}