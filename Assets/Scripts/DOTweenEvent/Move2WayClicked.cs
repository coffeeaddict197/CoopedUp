using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2WayClicked : MonoBehaviour
{
    [SerializeField]
    private Vector3 _targetLocation = Vector3.zero;

    [SerializeField]
    private float _moveDuration;

    [SerializeField]
    private Ease _moveEase = Ease.Linear;
    void Start()
    {
        transform.DOLocalMove(_targetLocation, _moveDuration).SetEase(_moveEase);
    }

   
}
