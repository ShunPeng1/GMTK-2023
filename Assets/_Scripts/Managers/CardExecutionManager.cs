using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Actor;
using UnityEngine;
using UnityUtilities;

namespace _Scripts.Cards
{
    public class CardExecutionManager : SingletonMonoBehaviour<CardExecutionManager>
    {
        [SerializeField] public ActorBehavior [] Actors;
        
        private List<object> _objectList = new();
        private List<BaseCardInformation> _prefixCardInformations = new();

        [SerializeField] private CardPlaceRegion _playerHandRegion;
        [SerializeField] private CardPlaceRegion _enemyPlaceRegion;

        [Serializable]
        class PlayerHandCardDistribution
        {
            public int StartGameDrawCount;
            public int EveryTurnDrawCount;
            public BaseCardInformation[] Cards;
                     
        }
        
        [SerializeField] private List<BaseCardInformation>[] _enemySentences;

        [SerializeField] private PlayerHandCardDistribution [] _playerHandCardDistributions;
        private Dictionary<PlayerHandCardDistribution, RandomBag<BaseCardInformation>> _playerDrawSet = new ();

        private RandomBag<BaseCardInformation> _enemyBag;

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
                    DrawCard(bag);
                }
            }
        }

        public void StartPlayerTurn()
        {
            foreach (var (playerHandCardDistribution, bag) in _playerDrawSet)
            {
                for (int i = 0; i < playerHandCardDistribution.EveryTurnDrawCount; i++)
                {
                    DrawCard(bag);
                }
            }
        }
        public void DrawCard(RandomBag<BaseCardInformation> bag)
        {
            var cardPlaceHolder = _playerHandRegion.FindEmptyCardPlaceHolder();
            if (cardPlaceHolder == null) return;
            BaseCard baseCard = Instantiate(ResourceManager.Instance.GetBaseCard(bag.PopRandomItem()), cardPlaceHolder.transform.position, Quaternion.identity);
            _playerHandRegion.AddCard(baseCard, null);
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


        public void Execute(List<BaseCardInformation> cardsInformation)
        {
            Stack<object []> prefixVariables = new Stack<object []>();
            Stack<BaseCardInformation> verbStack = new Stack<BaseCardInformation>();

            // Reverse 
            cardsInformation.Reverse();

            for (int i = 0; i < cardsInformation.Count; i++)
            {
                var cardInformation = cardsInformation[i];
            
                switch (cardInformation.WordCardType)
                {
                    case WordCardType.Noun:
                        if (verbStack.Count > 0 && verbStack.Peek().WordCardType == WordCardType.VerbPrefixUnary &&( i+1 >= cardsInformation.Count || cardsInformation[i+1].Priority > verbStack.Peek().Priority))
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

