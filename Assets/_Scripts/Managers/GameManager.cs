using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityUtilities;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public enum WhoseTurn
    {
        Ally,
        Enemy
    }

    private int _turn;
    
    
    public Sequence OnNextBattleFieldSequence;

    
    [Header("Craft Bench Animation")]
    [SerializeField] private GameObject _upperCraftBench;
    [SerializeField] private GameObject _lowerCraftBench;
    [SerializeField] private CardExecutionButton _cardExecutionButton;
    [SerializeField] private float _transitionDuration;
    [SerializeField] private Ease _transitionEase = Ease.OutCubic;
    [SerializeField] private Vector3 _transitionOffsetPosition = new Vector3(0, 10, 0);
    
    
    private void Start()
    {
        CreateSequence();
    }

    public void ShowBattleField()
    {
        HideCraftBench();
     
        DOVirtual.DelayedCall(_transitionDuration, () =>
        {
            OnNextBattleFieldSequence.AppendCallback(FinishBattleFieldAnimation).AppendInterval(2f);
        
            Debug.Log("Before Play");
            OnNextBattleFieldSequence.Play();
        });
        
    }

    private void CreateSequence()
    {
        
        OnNextBattleFieldSequence = DOTween.Sequence();
        OnNextBattleFieldSequence.Pause();

    }
    
    
    private void FinishBattleFieldAnimation()
    {
        ShowCraftBench();
        CreateSequence();

        _cardExecutionButton.UnsnapAnimation();
    }
    
    void ShowCraftBench()
    {
        _upperCraftBench.transform.DOMove(-_transitionOffsetPosition, _transitionDuration).SetRelative()
            .SetEase(_transitionEase);
        
        _lowerCraftBench.transform.DOMove(_transitionOffsetPosition, _transitionDuration).SetRelative()
            .SetEase(_transitionEase);
    }
    
    void ShowEnemiesCraftBench()
    {
        _upperCraftBench.transform.DOMove(-_transitionOffsetPosition, _transitionDuration).SetRelative()
            .SetEase(_transitionEase);
        
        _lowerCraftBench.transform.DOMove(_transitionOffsetPosition, _transitionDuration).SetRelative()
            .SetEase(_transitionEase);
    }

    void HideCraftBench()
    {
        _upperCraftBench.transform.DOMove(_transitionOffsetPosition, _transitionDuration).SetRelative()
            .SetEase(_transitionEase);
        
        _lowerCraftBench.transform.DOMove(-_transitionOffsetPosition, _transitionDuration).SetRelative()
            .SetEase(_transitionEase);
    }
    
}
