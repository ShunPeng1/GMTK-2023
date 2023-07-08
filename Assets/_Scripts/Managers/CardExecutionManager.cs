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
        [SerializeField] public PlayerActor PlayerActor;
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

                    case WordCardType.VerbUnary:
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
                    
                    case WordCardType.VerbUnary:
                        var result = ExecuteVerb(verb, prefixVariables.Pop(), prefixVariables.Pop());
                        prefixVariables.Push(result);
                        break;
                    
                    case WordCardType.VerbBinary:
                    case WordCardType.VerbTernary:
                    case WordCardType.Condition:
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
            }

            // The result will be the top value in the operand stack
            //return prefixVariables.Pop();
        
        }

        public object[] ExecuteVerb(BaseCardInformation verbCard, object [] noun1, object [] noun2)
        {
            object[] parameter = {
                noun1, noun2
            };
            return verbCard.Execute(parameter);
        }
        
    }
    
}

