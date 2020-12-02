using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoundsUltility
{
    public static bool ContainCamera(this Bounds bounds, Camera cam)
    {
        return bounds.Contains(cam.BottomLeftPoint()) && bounds.Contains(cam.TopRightPoint());
    }

    public static bool CointainPoint(this Bounds bounds , Vector3 point)
    {
        return bounds.Contains(point);
    }

    public static Vector2 TopRightPoint(this Bounds bounds)
    {
        float x = bounds.center.x + bounds.extents.x;
        float y = bounds.center.y + bounds.extents.y;
        return new Vector2(x, y);
    }

    public static Vector2 TopLeftPoint(this Bounds bounds)
    {
        float x = bounds.center.x - bounds.extents.x;
        float y = bounds.center.y + bounds.extents.y;
        return new Vector2(x, y);
    }
    
    public static Vector2 TopMiddlePoint(this Bounds bounds)
    {
        float x = bounds.center.x;
        float y = bounds.center.y + bounds.extents.y;
        return new Vector2(x, y);
    }

    public static Vector2 MiddlePoint(this Bounds bounds)
    {
        float x = bounds.center.x;
        float y = bounds.center.y;
        return new Vector2(x, y);
    }

    public static Vector2 MiddleRightPoint(this Bounds bounds)
    {
        float x = bounds.center.x + bounds.extents.x;
        float y = bounds.center.y;
        return new Vector2(x, y);
    }

    public static Vector2 MiddleLeftPoint(this Bounds bounds)
    {
        float x = bounds.center.x - bounds.extents.x;
        float y = bounds.center.y;
        return new Vector2(x, y);
    }

    public static Vector2 BottomRightPoint(this Bounds bounds)
    {
        float x = bounds.center.x + bounds.extents.x;
        float y = bounds.center.y - bounds.extents.y;
        return new Vector2(x, y);
    }

    public static Vector2 BottomLeftPoint(this Bounds bounds)
    {
        float x = bounds.center.x - bounds.extents.x;
        float y = bounds.center.y - bounds.extents.y;
        return new Vector2(x, y);
    }


    public static Vector2 BottomMiddlePoint(this Bounds bounds)
    {
        float x = bounds.center.x;
        float y = bounds.center.y - bounds.extents.y;
        return new Vector2(x, y);
    }

}
