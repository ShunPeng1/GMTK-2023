
using System;
using _Scripts.Actor;
using _Scripts.DataWrapper;
using UnityEngine;

namespace _Scripts.Cards.CardsInformation
{
    [CreateAssetMenu(fileName = "Card Information", menuName = "Card Information/Assign Verb" )]

    public class AssignVariableCardInformation : BaseCardInformation
    {
        

        public override object[] Execute(object[] array)
        {
            var parameters1 = (object[])array[0]; // Left
            var parameters2 = (object[])array[1]; // Right

            
            
            Type dataType1 = parameters1[0].GetType(); // Get the type of the object
            Type dataType2 = parameters2[0].GetType(); // Get the type of the object

            if (dataType1 == typeof(ObservableData<float> []))
            {
                foreach (var observableData in (ObservableData<float> [])  parameters1[0])
                {
                    observableData.Value -= ((ObservableData<float>)parameters2[0]).Value;
                }
            }
            else if (dataType1 == typeof(ObservableData<float>))
            {
                ((ObservableData<float>)parameters1[0]).Value = ((ObservableData<float>)parameters2[0]).Value;
            }
            else if (dataType1 == typeof(ObservableData<int>))
            {
                ((ObservableData<int>)parameters1[0]).Value = ((ObservableData<int>)parameters2[0]).Value;
            }
            else
            {
                throw new ArgumentException("Invalid code");
            }
            return new object[] { };
        }



    }
}