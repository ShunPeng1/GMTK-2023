using System;
using System.Collections;
using _Scripts.DataWrapper;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

namespace _Scripts.Actor
{
    public enum ActorRole
    {
        Ally,
        Enemy,
        Other
    }
    
    public class ActorBehavior : MonoBehaviour
    {
        public ObservableData<ActorRole> Role = new ObservableData<ActorRole>(ActorRole.Ally);
        public ObservableData<float> Health = new ObservableData<float>(10);
        public ObservableData<float> Strength = new (5); 
        public ObservableData<float> Gold = new (5);


        [Header("Animations")] 
        [SerializeField] private Animator _animator;

        [SerializeField] private float _generalAnimationDuration = 1f;
        private static readonly int Hit = Animator.StringToHash("Hit");


        [Header("Text UI")] 
        [SerializeField] private TMP_Text _healthText;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            Health.OnChangeValue += OnChangeHealth;

        }
        
        private void OnChangeHealth(float f, float f1)
        {
            Debug.Log(gameObject.name + " HEALTH CHANGE " + f + " To " + f1);

            
            GameManager.Instance.OnNextBattleFieldSequence.AppendCallback(
                () =>
                {
                    _animator.SetTrigger(Hit);
                    UpdateUI();
                }
            ).AppendInterval(_generalAnimationDuration);
            
            Debug.Log(gameObject.name + " Finish add sequence ");
        }

        public void UpdateUI()
        {
            Debug.Log("Update UI");
        }

        public void Attack(ActorBehavior beingAttackedActor)
        {
            beingAttackedActor.Health.Value -= Strength.Value;
        }
        
        
        
    }
}