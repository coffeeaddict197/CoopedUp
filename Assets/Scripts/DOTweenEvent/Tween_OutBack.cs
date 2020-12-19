using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Tween_OutBack : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 originPos;
    Image curImg;

    private void Awake()
    {
        curImg = GetComponent<Image>();
        originPos = transform.localPosition;
        curImg.raycastTarget = false;
    }
    //private void OnEnable()
    //{
    //    transform.localPosition = originPos;
    //    transform.DOLocalMove(Vector3.zero, 0.5f).SetEase(Ease.OutBack).SetUpdate(true);
    //}

    public void RollBack()
    {
        transform.DOLocalMove(originPos, 0.5f).SetEase(Ease.OutBack).SetUpdate(true);
        curImg.raycastTarget = false;
    }

    public void Execute()
    {
        
        transform.DOLocalMove(Vector3.zero, 0.5f).SetEase(Ease.OutBack).SetUpdate(true);
        curImg.raycastTarget = true;
    }
    
}
