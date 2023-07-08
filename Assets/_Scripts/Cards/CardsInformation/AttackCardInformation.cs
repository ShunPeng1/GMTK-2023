using System;
using _Scripts.Actor;
using _Scripts.DataWrapper;
using UnityEngine;

namespace _Scripts.Cards.CardsInformation
{
    [CreateAssetMenu(fileName = "Card Information", menuName = "Card Information/Attack Verb" )]
    public class AttackCardInformation : BaseCardInformation
    {
        public override void Awake()
        {
            WordCardType = WordCardType.VerbBinary;
            Priority = 0;
            Name = " Is Reduced by ";
            Cost = 1;
            ParametersCount = 2;
            ResultCount = 1;
        }
        
        public override object[] Execute(object[] array)
        {
            var parameters1 = (object[])array[0]; // Left
            var parameters2 = (object[])array[1]; // Right

            Type dataType1 = parameters1[0].GetType(); // Get the type of the object
            Type dataType2 = parameters2[0].GetType(); // Get the type of the object


            if (dataType1 == typeof(ActorBehavior) && dataType2 == typeof(ActorBehavior))
            {
                var actorBehavior1 = (ActorBehavior)parameters1[0];
                var actorBehavior2 = (ActorBehavior)parameters2[0];
                actorBehavior1.Attack(actorBehavior2);
            }
            else if (dataType1 == typeof(ActorBehavior []) && dataType2 == typeof(ActorBehavior))
            {
                var actorBehaviors1 = (ActorBehavior[])parameters1[0];
                var actorBehavior2 = (ActorBehavior)parameters2[0];
                foreach (var actor in actorBehaviors1)
                {
                    actor.Attack(actorBehavior2);
                }
            }
            else if (dataType1 == typeof(ActorBehavior) && dataType2 == typeof(ActorBehavior []))
            {
                var actorBehavior1 = (ActorBehavior)parameters1[0];
                var actorBehaviors2 = (ActorBehavior [])parameters2[0];
                foreach (var actor in actorBehaviors2)
                {
                    actorBehavior1.Attack(actor);
                }
            }
            else if (dataType1 == typeof(ActorBehavior []) && dataType2 == typeof(ActorBehavior []))
            {
                var actorBehaviors1 = (ActorBehavior[])parameters1[0];
                var actorBehaviors2 = (ActorBehavior[])parameters2[0];
                foreach (var actor1 in actorBehaviors1)
                {
                    foreach (var actor2 in actorBehaviors2)
                    {
                        actor1.Attack(actor2);
                        actor2.Attack(actor1);
                    }
                }
            }
            
            return new object[] { };
        }

    
    }
}