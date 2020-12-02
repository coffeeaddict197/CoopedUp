using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScreenUltility
{
    public static Vector2 TopRightPoint(this Camera cam)
    {
        Vector2 screenBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));
        return screenBounds;
    }

    public static Vector2 TopLeftPoint(this Camera cam)
    {
        Vector2 screenBounds = cam.ScreenToWorldPoint(new Vector3(0, Screen.height, cam.transform.position.z));
        return screenBounds;
    }

    public static Vector2 TopMiddlePoint(this Camera cam)
    {
        Vector2 screenBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height, cam.transform.position.z));
        return screenBounds;
    }

    public static Vector2 MiddlePoint(this Camera cam)
    {
        Vector2 screenBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, cam.transform.position.z));
        return screenBounds;
    }

    public static Vector2 MiddleRightPoint(this Camera cam)
    {
        Vector2 screenBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2, cam.transform.position.z));
        return screenBounds;
    }

    public static Vector2 MiddleLeftPoint(this Camera cam)
    {
        Vector2 screenBounds = cam.ScreenToWorldPoint(new Vector3(0, Screen.height / 2, cam.transform.position.z));
        return screenBounds;
    }

    public static Vector2 BottomRightPoint(this Camera cam)
    {
        Vector2 screenBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, cam.transform.position.z));
        return screenBounds;
    }

    public static Vector2 BottomLeftPoint(this Camera cam)
    {
        Vector2 screenBounds = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.transform.position.z));
        return screenBounds;
    }


    public static Vector2 BottomMiddlePoint(this Camera cam)
    {
        Vector2 screenBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width/2, 0, cam.transform.position.z));
        return screenBounds;
    }
}
