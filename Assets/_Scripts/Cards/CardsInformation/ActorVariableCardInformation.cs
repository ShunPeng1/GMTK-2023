using System;
using _Scripts.Actor;
using _Scripts.DataWrapper;
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
            WordCardType = WordCardType.VerbPrefixUnary;
            Priority = -2;
            Name = "'s Health";
            Cost = 1;
            ParametersCount = 1;
            ResultCount = 0;
            
        }

        public override object[] Execute(object[] array)
        {
            var parameters1 = (object[])array[0]; // Left

            Type dataType1 = parameters1[0].GetType(); // Get the type of the object
            

            if (dataType1 == typeof(ActorBehavior))
            {
                return new object[] { GetFloat((ActorBehavior)parameters1[0]) };
            }
            else if (dataType1 == typeof(ActorBehavior []))
            {
                var actorBehaviors = (ActorBehavior[])parameters1[0];
                ObservableData<float> [] result = new ObservableData<float>[actorBehaviors.Length];
                for (int i = 0; i < actorBehaviors.Length; i++)
                {
                    result[i] = GetFloat(actorBehaviors[i]);
                }
                return new object[] { result };
            }
            else
            {
                throw new ArgumentException("Invalid code");
            }
            return new object[] { };
        }

        private ObservableData<float> GetFloat(ActorBehavior actorBehavior)
        {
            return _actorVariable switch
            {
                Variable.Health => actorBehavior.Health,
                Variable.Strength => actorBehavior.Strength,
                Variable.Gold => actorBehavior.Gold,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}