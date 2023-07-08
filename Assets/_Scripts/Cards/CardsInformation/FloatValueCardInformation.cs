﻿using _Scripts.DataWrapper;
using UnityEngine;

namespace _Scripts.Cards.CardsInformation
{
    [CreateAssetMenu(fileName = "Card Information", menuName = "Card Information/Float Value" )]
    public class FloatValueCardInformation : BaseCardInformation
    {
        [SerializeField] private ObservableData<float> _value;
        
        public override void Awake()
        {
            WordCardType = WordCardType.Noun;
            Priority = 0;
            Name = $" {_value.Value}";
            Cost = 0;
            ParametersCount = 0;
            ResultCount = 1;
        }

        public override object[] Execute(object[] array)
        {
            return new object[] { _value };
        }
    }
}