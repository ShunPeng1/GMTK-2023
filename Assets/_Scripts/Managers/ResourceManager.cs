using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Cards;
using UnityEngine;
using UnityUtilities;
using Random = UnityEngine.Random;


public class ResourceManager : SingletonMonoBehaviour<ResourceManager>
{
    public CardPlaceHolder CardPlaceHolder;

    [SerializeField] private BaseCard [] _baseCards;

    public BaseCard GetBaseCard(BaseCardInformation cardInformation)
    {
        return _baseCards.FirstOrDefault(baseCard => baseCard.CardInformation == cardInformation);
    }
}
