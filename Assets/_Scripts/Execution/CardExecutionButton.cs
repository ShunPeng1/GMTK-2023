using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Cards;
using DG.Tweening;
using UnityEngine;

public class CardExecutionButton : MonoBehaviour
{
    [SerializeField] private CardPlaceRegion _cardPlaceRegion;

    [SerializeField] private float _moveDuration = 0.15f;
    [SerializeField] private Ease _movingEase = Ease.OutCubic;
    
    private Vector3 _initialPosition;

    private bool _isSnapping = false;
    private void Start()
    {

        _initialPosition = transform.position;
    }

    private void OnMouseDown()
    {
        if(!_isSnapping) SnapAnimation();
        else UnsnapAnimation();
    }


    private void SnapAnimation()
    {
        
        _isSnapping = true;
         Vector3 nextFitPosition = _cardPlaceRegion.SnapFitAllCard();
         transform.DOMove(nextFitPosition,_moveDuration).SetEase(_movingEase).OnComplete(
             ExecuteCardRegion);
    }

    private void UnsnapAnimation()
    {
        Vector3 nextFitPosition = _cardPlaceRegion.UnsnapFitAllCard();
        transform.DOMove(_initialPosition, _moveDuration).SetEase(_movingEase);
        _isSnapping = false;
    }
    
    private void ExecuteCardRegion()
    {
        try
        {
            Debug.Log("EXECUTE ");
            CardExecutionManager.Instance.Execute(_cardPlaceRegion.GetCardsInformation());
        }
        catch (Exception e)
        {
            UnsnapAnimation();
        }
    }

}
