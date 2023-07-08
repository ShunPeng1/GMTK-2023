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
            Priority = 0;
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
                return new object[] { Increase((ActorBehavior)parameters1[0]) };
            }
            else if (dataType1 == typeof(PlayerActor []))
            {
                var actorBehaviors = (PlayerActor[])parameters1[0];
                object[] result = new object[actorBehaviors.Length];
                for (int i = 0; i < actorBehaviors.Length; i++)
                {
                    result[i] = Increase(actorBehaviors[i]);
                }

                return result;
            }
            if (dataType1 == typeof(EnemyActor []))
            {
                var actorBehaviors = (EnemyActor[])parameters1[0];
                ObservableData<float> [] result = new ObservableData<float>[actorBehaviors.Length];
                for (int i = 0; i < actorBehaviors.Length; i++)
                {
                    result[i] = Increase(actorBehaviors[i]);
                }

                return new object[] { result };
            }
            return new object[] { };
        }

        private ObservableData<float> Increase(ActorBehavior actorBehavior)
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