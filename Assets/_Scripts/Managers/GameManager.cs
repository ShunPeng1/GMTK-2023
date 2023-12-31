using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Cards;
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
    public bool IsGameOver = false;
    
    [Header("Craft Bench Animation")]
    [SerializeField] private GameObject _upperCraftBench;
    [SerializeField] private GameObject _lowerCraftBench;
    [SerializeField] private GameObject _enemyCraftBench;
    [SerializeField] private CardExecutionButton _cardExecutionButton;
    [SerializeField] private CardExecutionButton _enemyExecutionButton;
    
    [SerializeField] private float _transitionDuration;
    [SerializeField] private Ease _transitionEase = Ease.OutCubic;
    [SerializeField] private Vector3 _transitionOffsetPosition = new Vector3(0, 10, 0);
    
    
    private void Start()
    {
        CreateSequence();
    }

    public void ShowPlayerBattleField()
    {
        HidePlayerCraftBench();
     
        DOVirtual.DelayedCall(_transitionDuration, () =>
        {
            OnNextBattleFieldSequence.AppendCallback(FinishPlayerBattleFieldAnimation).AppendInterval(2f);
        
            Debug.Log("Before Play");
            OnNextBattleFieldSequence.Play();
        });
        
    }
    
    public void ShowEnemyBattleField()
    {
        HideEnemyCraftBench();
     
        DOVirtual.DelayedCall(_transitionDuration, () =>
        {
            OnNextBattleFieldSequence.AppendCallback(FinishEnemyBattleFieldAnimation).AppendInterval(2f);
        
            Debug.Log("Before Play");
            OnNextBattleFieldSequence.Play();
        });
        
    }

    private void CreateSequence()
    {
        
        OnNextBattleFieldSequence = DOTween.Sequence();
        OnNextBattleFieldSequence.Pause();

    }
    
    
    private void FinishPlayerBattleFieldAnimation()
    {
        if (IsGameOver) return;
        ShowEnemiesCraftBench();
        CreateSequence();
        
        
        
        DOVirtual.DelayedCall(_transitionDuration, () =>
        {
            if (IsGameOver) return;
            CardExecutionManager.Instance.StartEnemyTurn();
        });
    }

    private void FinishEnemyBattleFieldAnimation()
    {
        if (IsGameOver) return;
        
        ShowCraftBench();
        CreateSequence();

        
        _enemyExecutionButton.UnsnapAnimation();
        _cardExecutionButton.UnsnapAnimation();
        
        DOVirtual.DelayedCall(_transitionDuration, () =>
        {
            if (IsGameOver) return;
            CardExecutionManager.Instance.StartPlayerTurn();
        });
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
        _enemyCraftBench.transform.DOMove(-_transitionOffsetPosition, _transitionDuration).SetRelative()
            .SetEase(_transitionEase);
    }

    void HidePlayerCraftBench()
    {
        _upperCraftBench.transform.DOMove(_transitionOffsetPosition, _transitionDuration).SetRelative()
            .SetEase(_transitionEase);
        
        _lowerCraftBench.transform.DOMove(-_transitionOffsetPosition, _transitionDuration).SetRelative()
            .SetEase(_transitionEase);
    }
    
    void HideEnemyCraftBench()
    {
        _enemyCraftBench.transform.DOMove(_transitionOffsetPosition, _transitionDuration).SetRelative()
            .SetEase(_transitionEase);
    }
}
