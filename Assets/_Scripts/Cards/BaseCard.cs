using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Cards;
using TMPro;
using UnityEngine;


public class BaseCard : MonoBehaviour
{
    public BaseCardInformation CardInformation;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private TMP_Text _wordText;
    private void Awake()
    {
        _wordText.text = CardInformation.Name;
        _spriteRenderer.color = VisualManager.Instance.GetColorCard(CardInformation.WordCardType);
        _spriteRenderer.sprite = VisualManager.Instance.GetSpriteCard(CardInformation.WordCardType);
    }
}
