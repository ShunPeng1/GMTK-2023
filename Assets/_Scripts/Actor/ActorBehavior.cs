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
        private static readonly int Atk = Animator.StringToHash("Attack");
        private static readonly int Die = Animator.StringToHash("Die");


        [Header("Text UI")] 
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private TMP_Text _strText;

        [SerializeField] private AudioClip AttackSFX;
        private void Start()
        {
            _animator = GetComponent<Animator>();
            Health.OnChangeValue += OnChangeHealth;
            Strength.OnChangeValue += OnChangeStrength;
        }
        
        private void OnChangeHealth(float f, float f1)
        {
            //Debug.Log(gameObject.name + " HEALTH CHANGE " + f + " To " + f1);

            
            GameManager.Instance.OnNextBattleFieldSequence.AppendCallback(
                () =>
                {
                    _animator.SetTrigger(Hit);
                    UpdateUI();
                    if(f1 <=0)
                    {
                        _animator.SetTrigger(Die);
                    }
                    
                    //Debug.Log(gameObject.name + " Finish add sequence ");
                }
            ).AppendInterval(_generalAnimationDuration);
            
        }
        
        private void OnChangeStrength(float f, float f1)
        {
            //Debug.Log(gameObject.name + " HEALTH CHANGE " + f + " To " + f1);

            
            GameManager.Instance.OnNextBattleFieldSequence.AppendCallback(
                () =>
                {
                    _animator.SetTrigger(Hit);
                    UpdateUI();
                    
                    //Debug.Log(gameObject.name + " Finish add sequence ");
                }
            ).AppendInterval(_generalAnimationDuration);
            
        }

        public void UpdateUI()
        {
            _healthText.text = Health.Value.ToString();
            _strText.text = Strength.Value.ToString();
        }

        public void Attack(ActorBehavior beingAttackedActor)
        {
            beingAttackedActor.Health.Value -= Strength.Value;
            _animator.SetTrigger(Atk);
            //SoundManager.Instance.PlaySound(AttackSFX);
        }
        
        
        
    }
}