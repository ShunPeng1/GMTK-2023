using _Scripts.DataWrapper;
using UnityEngine;

namespace _Scripts.Cards.CardsInformation
{
    [CreateAssetMenu(fileName = "Card Information", menuName = "Card Information/Assign Verb" )]

    public class AssignVariableCardInformation : BaseCardInformation
    {
        public override void Awake()
        {
            WordCardType = WordCardType.VerbBinary;
            Priority = 0;
            Name = " Assign ";
            Cost = 1;
            ParametersCount = 2;
            ResultCount = 1;
        }

        public override object[] Execute(object[] array)
        {
            var parameters1 = (object[])array[0]; // Left
            var parameters2 = (object[])array[1]; // Right

            ObservableData<float> assigner = (ObservableData<float>) parameters1[0]; 
            ObservableData<float> value = (ObservableData<float>) parameters2[0];
            assigner.Value = value.Value;
            
            

            
            return new object[] { CardExecutionManager.Instance.PlayerActor };
        }



    }
}