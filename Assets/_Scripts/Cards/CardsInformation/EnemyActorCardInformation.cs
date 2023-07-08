using UnityEngine;

namespace _Scripts.Cards.CardsInformation
{
    
    [CreateAssetMenu(fileName = "Card Information", menuName = "Card Information/Enemy Actor")]
    public class EnemyActorCardInformation : BaseCardInformation
    {
        public override void Awake()
        {
            WordCardType = WordCardType.Noun;
            Priority = 0;
            Name = "Enemy";
            Cost = 1;
            ParametersCount = 0;
            ResultCount = 1;
        }

        public override object[] Execute(object[] array)
        {
            return new object[] { CardExecutionManager.Instance.EnemyActors[0] };
        }
    }
}