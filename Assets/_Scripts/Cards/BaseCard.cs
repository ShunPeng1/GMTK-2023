using System;
using _Scripts.Cards;
using TMPro;
using UnityEditor;
using UnityEngine;


public class BaseCard : MonoBehaviour
{
    public BaseCardInformation CardInformation;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private TMP_Text _wordText;
    private void OnValidate()
    {
        _wordText.text = CardInformation.Name;
        
        if (VisualManager.Instance == null) return;
        _spriteRenderer.color = VisualManager.Instance.GetColorCard(CardInformation.WordCardType);
        _spriteRenderer.sprite = VisualManager.Instance.GetSpriteCard(CardInformation.WordCardType);
    }
}
