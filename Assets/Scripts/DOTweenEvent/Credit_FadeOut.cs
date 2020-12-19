using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Credit_FadeOut : MonoBehaviour
{
    [SerializeField]
    private float _moveDuration;
    [SerializeField]
    private Ease _moveEase = Ease.Linear;

        [SerializeField] Vector3 _originPos;

    private Image img;
    private void Awake()
    {
        img = GetComponent<Image>();
        _originPos = transform.localPosition;
    }

    private void OnEnable()
    {
        transform.localPosition = _originPos;
        img.raycastTarget = false;
    }

    public void FadeOut()
    {
        transform.DOLocalMove(_originPos, _moveDuration).SetUpdate(true);
        img.raycastTarget = false;
    }

    public void FadeIn()
    {
        img.DOFade(1f, _moveDuration).SetUpdate(true);
        transform.DOLocalMove(Vector3.zero, _moveDuration).SetEase(_moveEase).SetUpdate(true);

        img.raycastTarget = true;
    }
}
