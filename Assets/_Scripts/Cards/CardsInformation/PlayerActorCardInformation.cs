using UnityEngine;

namespace _Scripts.Cards.CardsInformation
{
    
    [CreateAssetMenu(fileName = "Card Information", menuName = "Card Information/Player Actor" )]
    public class PlayerActorCardInformation : BaseCardInformation
    {
        public override void Awake()
        {
             WordCardType = WordCardType.Noun;
             Priority = 0;
             Name = " Player ";
             Cost = 1;
             ParametersCount = 0;
             ResultCount = 1;
        }

        public override object[] Execute(object[] array)
        {
            return new object[] { CardExecutionManager.Instance.PlayerActors[0] };
        }
    }
}