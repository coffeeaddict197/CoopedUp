using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAnimationController : MonoBehaviour
{
    public Sprite top;
    public Sprite topA;
    public Sprite topB;
    public Sprite topC;
    public Sprite topD;

    public SpriteRenderer rootSprite;


    //Luu y : khi co su thay doi sprite trong runtime ma animation dang chay , dat no trong LateUpdate , van de o Execute trong unity
    public void UpdateSprieFollowDirecion(Vector2 direc)
    {
        float Angle = Vector2.Angle(direc, Vector2.up);
        if(Angle<10f)
        {
            rootSprite.sprite = top;
        }
        else if (Angle >= 10f && Angle < 30f)
        {
            rootSprite.sprite = topA;
        }
        else if(Angle < 45f)
        {
            rootSprite.sprite = topB;
        }
        else if(Angle<60f)
        {
            rootSprite.sprite = topC;
        }
        else
        {
            rootSprite.sprite = topD;
        }
    }
}
