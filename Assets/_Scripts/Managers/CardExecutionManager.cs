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
        [SerializeField] public PlayerActor [] PlayerActors;
        [SerializeField] public EnemyActor [] EnemyActors;
        
        [SerializeField] private CardPlaceRegion _executeRegion;
        
        private List<object> _objectList = new();
        private List<BaseCardInformation> _prefixCardInformations = new();
        public void AddObject(object obj)
        {
            _objectList.Add(obj);
        }

        public T GetObjectByType<T>()
        {
            foreach (object obj in _objectList)
            {
                if (obj is T)
                {
                    return (T)obj;
                }
            }

            return default(T);
        }

        public void RemoveObject(object obj)
        {
            _objectList.Remove(obj);
        }
        private void Awake()
        {
            
        }

        public void Execute()
        {
            var cardsInformation = _executeRegion.GetCardsInformation();

            Stack<object []> prefixVariables = new Stack<object []>();
            Stack<BaseCardInformation> verbStack = new Stack<BaseCardInformation>();

            // Reverse 
            cardsInformation.Reverse();
            
            foreach (var cardInformation in cardsInformation) // loop in reverse 
            {
                switch (cardInformation.WordCardType)
                {
                    case WordCardType.Noun:
                        prefixVariables.Push(cardInformation.Execute(null));
                        break;
                    
                    
                    case WordCardType.VerbPrefixUnary:
                        while (verbStack.Count > 0 && cardInformation.Priority < verbStack.Peek().Priority)
                        {
                            prefixVariables.Push(ExecuteVerb(verbStack.Pop(), prefixVariables.Pop()));
                        }
                        
                        // Push the current operator onto the operator stack
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

