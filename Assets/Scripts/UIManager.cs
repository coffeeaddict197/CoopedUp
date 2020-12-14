using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class UIManager : MonoSingleton<UIManager>
{
    // Start is called before the first frame update
    [SerializeField] Animator[] PointAnim;
    const string UP_POINT = "UpPoint";


    public void UpPointAnimation(int point)
    {
        PointAnim[point-1].SetTrigger(UP_POINT);
    }
}
