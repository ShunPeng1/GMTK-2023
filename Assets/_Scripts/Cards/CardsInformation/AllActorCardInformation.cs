using System;
using _Scripts.Actor;
using UnityEngine;

namespace _Scripts.Cards.CardsInformation
{
    
    [CreateAssetMenu(fileName = "Card Information", menuName = "Card Information/All Actor" )]
    public class AllActorCardInformation : BaseCardInformation
    {
        public override void Awake()
        {
            WordCardType = WordCardType.VerbPostfixUnary;
            Priority = -3;
            Name = " All ";
            Cost = 1;
            ParametersCount = 1;
            ResultCount = 0;
            
        }

        public override object[] Execute(object[] array)
        {
            var parameters1 = (object[]) array[0];
            
            
            Type dataType1 = parameters1[0].GetType(); // Get the type of the object

            if (dataType1 == typeof(ActorBehavior))
            {
                return new object[] { CardExecutionManager.Instance.GetAllActorOfRole(((ActorBehavior) parameters1[0]).Role.Value) };
            }
            else
            {
                throw new ArgumentException("Invalid code");
            }
            return new object[] { };
            
        }
    }
}