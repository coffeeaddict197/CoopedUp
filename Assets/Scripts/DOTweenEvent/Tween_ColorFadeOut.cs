using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class Tween_ColorFadeOut : MonoBehaviour
{

    [SerializeField]
    private float _moveDuration;

    [SerializeField]
    private Ease _moveEase = Ease.Linear;
 
    public void FadeOut()
    {
        transform.GetComponent<CanvasGroup>().DOFade(0f, _moveDuration);
    }
}
