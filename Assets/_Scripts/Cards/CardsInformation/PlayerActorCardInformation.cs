using UnityEngine;

namespace _Scripts.Cards.CardsInformation
{
    
    [CreateAssetMenu(fileName = "Card Information", menuName = "Card Information/Player Actor" )]
    public class PlayerActorCardInformation : BaseCardInformation
    {
        public override void Awake()
        {
             WordCardType = WordCardType.VerbUnary;
             Priority = 0;
             Name = " Damage ";
             Cost = 1;
             ParametersCount = 0;
             ResultCount = 1;
        }

        public override object[] Execute(object[] array)
        {
            return new object[] { CardExecutionManager.Instance.PlayerActor };
        }
    }
}