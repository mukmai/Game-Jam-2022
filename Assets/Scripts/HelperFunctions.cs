using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperFunctions
{
    public static Vector3 RemoveY(this Vector3 vector)
    {
        return new Vector3(vector.x, 0, vector.z);
    }
    
    public static Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        return Camera.main.ScreenToWorldPoint(mousePoint).RemoveY();
    }

    public static Color ConvertToColor(this ColorCode colorCode)
    {
        switch (colorCode)
        {
            case ColorCode.Red:
                return Color.red;
            case ColorCode.Yellow:
                return Color.yellow;
            case ColorCode.Blue:
                return Color.blue;
            case ColorCode.Red | ColorCode.Yellow:
                return new Color(1, .5f, 0);
            case ColorCode.Yellow | ColorCode.Blue:
                return Color.green;
            case ColorCode.Red | ColorCode.Blue:
                return Color.magenta;
            default:
                return Color.white;
        }
    }
}
