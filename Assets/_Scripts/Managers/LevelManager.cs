using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Cards;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityUtilities;

public class LevelManager : SingletonMonoBehaviour<LevelManager>
{
    public enum WhoseTurn
    {
        Ally,
        Enemy
    }

    [Serializable]
    public class EnemySentence
    {
    }
    
    private int _turn;
    
    [SerializeField] private BaseCardInformation[] _allyCards;
    [SerializeField] private List<BaseCardInformation>[] _enemySentences;
    
    private RandomBag<BaseCardInformation> _allyBag;
    private RandomBag<BaseCardInformation> _enemyBag;

    public Action NextBattleFieldAnimations;

    
    [Header("Craft Bench Animation")]
    [SerializeField] private GameObject _upperCraftBench;
    [SerializeField] private GameObject _lowerCraftBench;
    [SerializeField] private float _transitionDuration;
    [SerializeField] private Ease _transitionEase = Ease.OutCubic;
    [SerializeField] private Vector3 _transitionOffsetPosition = new Vector3(0, 10, 0);


    public void ShowBattleField()
    {
        HideCraftBench();
        DOVirtual.DelayedCall(_transitionDuration, () => {NextBattleFieldAnimations.Invoke(); });
        
    }

    
    void ShowCraftBench()
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
