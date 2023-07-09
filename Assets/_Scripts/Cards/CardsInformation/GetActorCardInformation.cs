using _Scripts.Actor;
using UnityEngine;

namespace _Scripts.Cards.CardsInformation
{
    
    [CreateAssetMenu(fileName = "Card Information", menuName = "Card Information/Get Actor")]
    public class GetActorCardInformation : BaseCardInformation
    {
        [SerializeField] private ActorRole _actorRole;
       
        

        public override object[] Execute(object[] array)
        {
            return new object[] { CardExecutionManager.Instance.GetFirstActorOfRole(_actorRole) };
        }
    }
}