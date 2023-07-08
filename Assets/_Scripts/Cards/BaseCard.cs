using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Cards;
using TMPro;
using UnityEngine;


public class BaseCard : MonoBehaviour
{
    public BaseCardInformation CardInformation;

    [SerializeField] private TMP_Text _wordText;
    private void Awake()
    {
        _wordText.text = CardInformation.Name;
    }
}
