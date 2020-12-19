using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class Tween_UIColorFadeOut : MonoBehaviour
{

    [SerializeField]
    private float _moveDuration;
    [SerializeField]
    private Ease _moveEase = Ease.Linear;

    [Header("Child object to move")]
    [SerializeField]
    GameObject ObjectToMove;
    Vector3 originPos;
    [SerializeField] Vector3 _targetPos;

    private Button thisButton;
    private Image img;
    private void Awake()
    {
        thisButton = GetComponent<Button>();
        img = GetComponent<Image>();
        this.thisButton.onClick.AddListener(FadeOut);
        originPos = ObjectToMove.transform.localPosition;
    }
    public void FadeOut()
    {

        img.DOFade(0f, _moveDuration).SetUpdate(true);
        ObjectToMove.transform.DOLocalMove(_targetPos, _moveDuration).SetUpdate(true).SetDelay(_moveDuration);
        img.raycastTarget = false;
    }

    public void FadeIn()
    {
        img.DOFade(1f, _moveDuration).SetUpdate(true);
        ObjectToMove.transform.DOLocalMove(originPos, _moveDuration).SetUpdate(true).SetDelay(_moveDuration);
        img.raycastTarget = true;
    }
   
}
