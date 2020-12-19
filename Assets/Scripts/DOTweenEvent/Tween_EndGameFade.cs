using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class Tween_EndGameFade : MonoBehaviour
{

    [SerializeField]
    private float _moveDuration;
    [SerializeField]
    private Ease _moveEase = Ease.Linear;

    [SerializeField] Vector3 _targetPos;
    [SerializeField] Vector3 _originPos;
    [SerializeField] Vector3 _belowPos;

    //Animation
    [SerializeField] Animator AnimationButton;
    const string a_shake = "Shake";

    [SerializeField] Transform Score;
    Vector3 originScore;

    private Image img;
    private void Awake()
    {
        img = GetComponent<Image>();
        _originPos = transform.localPosition;
        originScore = Score.localPosition;
    }

    private void OnEnable()
    {
        transform.localPosition = _originPos;
        img.raycastTarget = false;
    }

    public void FadeOut()
    {

        transform.DOLocalMove(_belowPos, _moveDuration).SetUpdate(true).OnComplete(() =>
        {
            gameObject.SetActive(false);
            Score.localPosition = originScore;
            
        });
        img.raycastTarget = false;
    }

    public void FadeIn()
    {
        img.DOFade(1f, _moveDuration).SetUpdate(true);
        transform.DOLocalMove(Vector3.zero, _moveDuration).SetEase(_moveEase).SetUpdate(true).SetDelay(1f).OnComplete(() =>
        {
            AnimationButton.Play(a_shake);
            Score.DOLocalMoveX(0, 0.3f).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
           {

           });
        });

        img.raycastTarget = true;
    }

}
