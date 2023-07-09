using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Actor;
using DG.Tweening;
using UnityEngine;
using UnityUtilities;
using Random = UnityEngine.Random;

namespace _Scripts.Cards
{
    public class CardExecutionManager : SingletonMonoBehaviour<CardExecutionManager>
    {
        [SerializeField] public List< ActorBehavior > Actors;
        
        [Header("Region and Button")]
        [SerializeField] private CardPlaceRegion _playerHandRegion;
        [SerializeField] private CardPlaceRegion _enemyPlaceRegion;
        [SerializeField] private CardExecutionButton _enemyButton;
        
        [Serializable]
        class PlayerHandCardDistribution
        {
            public int StartGameDrawCount;
            public int EveryTurnDrawCount;
            public BaseCardInformation[] Cards;
                     
        }

        [Serializable]
        class EnemySentenceDistribution
        {
            public List<BaseCardInformation> EnemyBaseCardInformation;
        }
        
        [SerializeField] private PlayerHandCardDistribution [] _playerHandCardDistributions;
        [SerializeField] private EnemySentenceDistribution[] _enemySentences;

        private Dictionary<PlayerHandCardDistribution, RandomBag<BaseCardInformation>> _playerDrawSet = new ();

        private void Awake()
        {
            foreach (var playerHandCardDistribution in _playerHandCardDistributions)
            {
                RandomBag<BaseCardInformation> bag = new RandomBag<BaseCardInformation>(playerHandCardDistribution.Cards,1);
                _playerDrawSet.Add(playerHandCardDistribution, bag);
            }

        }

        private void Start()
        {
            foreach (var (playerHandCardDistribution, bag) in _playerDrawSet)
            {
                for (int i = 0; i < playerHandCardDistribution.StartGameDrawCount; i++)
                {
                    DrawCard(_playerHandRegion,bag.PopRandomItem());
                }
            }
        }

        public void StartPlayerTurn()
        {
            foreach (var (playerHandCardDistribution, bag) in _playerDrawSet)
            {
                for (int i = 0; i < playerHandCardDistribution.EveryTurnDrawCount; i++)
                {
                    DrawCard(_playerHandRegion,bag.PopRandomItem());
                }
            }
        }
        
        public void StartEnemyTurn()
        {
            var enemySentence = _enemySentences[Random.Range(0, _enemySentences.Length)];

            foreach (var enemyBaseCard in enemySentence.EnemyBaseCardInformation)
            {
                DrawCard(_enemyPlaceRegion,enemyBaseCard);
            }
            
            
            DOVirtual.DelayedCall(1.75f, () =>
            {
                _enemyButton.SnapAnimation(false);
            });
            
            DOVirtual.DelayedCall(2f, () =>
            {
                ExecuteEnemyCardInformation(enemySentence.EnemyBaseCardInformation);
            });
            
            
        }

        private void ExecuteEnemyCardInformation(List<BaseCardInformation> enemySentence)
        {
            
            try
            {
                Execute(enemySentence);
                _enemyPlaceRegion.DestroyAllCard();
                GameManager.Instance.ShowEnemyBattleField();
                
            }
            catch (Exception e)
            {
                string debug = enemySentence.Aggregate("", (current, enemyBaseCardInformation) => current + enemyBaseCardInformation.Name);
                Debug.LogError("WRONG ENEMY SENTENCES " + debug);
                
                _enemyPlaceRegion.DestroyAllCard();
                _enemyButton.UnsnapAnimation();
            }
        }
        
        public void DrawCard(CardPlaceRegion placeRegion, BaseCardInformation baseCardInformation)
        {
            var cardPlaceHolder = placeRegion.FindEmptyCardPlaceHolder();
            if (cardPlaceHolder == null) return;
            BaseCard baseCard = Instantiate(ResourceManager.Instance.GetBaseCard(baseCardInformation), cardPlaceHolder.transform.position, Quaternion.identity);
            placeRegion.AddCard(baseCard, null);
        }
        
        public ActorBehavior[] GetAllActorOfRole(ActorRole actorRole)
        {
            return Actors.Where(actor => actor.Role.Value == actorRole).ToArray();
        }
        
        public ActorBehavior GetFirstActorOfRole(ActorRole actorRole)
        {
            return Actors.FirstOrDefault(actor => actor.Role.Value == actorRole);
        }
        
        public ActorBehavior GetLastActorOfRole(ActorRole actorRole)
        {
            return Actors.LastOrDefault(actor => actor.Role.Value == actorRole);
        }

        public void RemoveActor(ActorBehavior actorBehavior)
        {
            Actors.Remove(actorBehavior);
        }

        public void Execute(List<BaseCardInformation> cardsInformation)
        {
            Stack<object []> prefixVariables = new Stack<object []>();
            Stack<BaseCardInformation> verbStack = new Stack<BaseCardInformation>();

            // Reverse 
            List<BaseCardInformation> reversedList = new List<BaseCardInformation>(cardsInformation);
            reversedList.Reverse();
            
            for (int i = 0; i < reversedList.Count; i++)
            {
                var cardInformation = reversedList[i];
            
                switch (cardInformation.WordCardType)
                {
                    case WordCardType.Noun:
                        if (verbStack.Count > 0 && verbStack.Peek().WordCardType == WordCardType.VerbPrefixUnary &&( i+1 >= reversedList.Count || reversedList[i+1].Priority > verbStack.Peek().Priority))
                        {
                            prefixVariables.Push(ExecuteVerb(verbStack.Pop(), cardInformation.Execute(null)));
                        }
                        else prefixVariables.Push(cardInformation.Execute(null));
                        break;
                    
                    
                    case WordCardType.VerbPrefixUnary:
                        verbStack.Push(cardInformation);
                        break;
                    
                    case WordCardType.VerbPostfixUnary:
                        prefixVariables.Push(ExecuteVerb(cardInformation, prefixVariables.Pop()));
                        break;
                        
                    case WordCardType.VerbBinary:
                        while (verbStack.Count > 0 && cardInformation.Priority < verbStack.Peek().Priority)
                        {
                            prefixVariables.Push(ExecuteVerb(verbStack.Pop(), prefixVariables.Pop(), prefixVariables.Pop()));
                        }
                        
                        // Push the current operator onto the operator stack
                        verbStack.Push(cardInformation);
                        break;
                    
                    case WordCardType.Value:
                        prefixVariables.Push(cardInformation.Execute(null));
                        break;

                        
                    case WordCardType.VerbTernary:
                    case WordCardType.Condition:
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
            }
            
            
            // Perform remaining operations in the stacks
            while (verbStack.Count > 0)
            {
                var verb = verbStack.Pop();
                switch (verb.WordCardType)
                {
                    case WordCardType.VerbPostfixUnary:
                    case WordCardType.VerbPrefixUnary:
                        var resultVerbUnary = ExecuteVerb(verb, prefixVariables.Pop());
                        prefixVariables.Push(resultVerbUnary);
                        break;
                    
                    case WordCardType.VerbBinary:
                        var resultVerbBinary = ExecuteVerb(verb, prefixVariables.Pop(), prefixVariables.Pop());

                        prefixVariables.Push(resultVerbBinary);
                        break;
                    case WordCardType.VerbTernary:
                    case WordCardType.Condition:
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
            }

            // The result will be the top value in the operand stack
            //return prefixVariables.Pop();
        
        }
        
        
        private object[] ExecuteVerb(BaseCardInformation verbCard, object [] noun1)
        {
            object[] parameter = { noun1 };
            return verbCard.Execute(parameter);
        }


        private object[] ExecuteVerb(BaseCardInformation verbCard, object [] noun1, object [] noun2)
        {
            object[] parameter = { noun1, noun2 };
            return verbCard.Execute(parameter);
        }
        
        
    }
    
}

