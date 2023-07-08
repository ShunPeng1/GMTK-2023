using System;
using _Scripts.Actor;
using UnityEngine;

namespace _Scripts.Cards.CardsInformation
{
    
    [CreateAssetMenu(fileName = "Card Information", menuName = "Card Information/Actor Health" )]
    public class ActorVariableCardInformation : BaseCardInformation
    {
        private enum Variable
        {
            Health,
            Strength,
            Gold
        }

        [SerializeField] private Variable _actorVariable;
        public override void Awake()
        {
            WordCardType = WordCardType.VerbUnary;
            Priority = 0;
            Name = "'s Health";
            Cost = 1;
            ParametersCount = 1;
            ResultCount = 0;
            
        }

        public override object[] Execute(object[] array)
        {
            var parameter1 = (object[]) array[0];
            ActorBehavior actorBehavior = (ActorBehavior) parameter1[0];

            switch (_actorVariable)
            {
                case Variable.Health:
                    return new object[] { actorBehavior.Health };
                    break;
                case Variable.Strength:
                    return new object[] { actorBehavior.Strength };
                    break;
                case Variable.Gold:
                    return new object[] { actorBehavior.Gold };
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}