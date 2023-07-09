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

    [SerializeField] private Color _selectHighlightColor = new Color(0.15f, 0.15f, 0.15f);

    [SerializeField] private bool _activeValidate = true;
    
    private void OnValidate()
    {
        if(!_activeValidate) return;
        _wordText.text = CardInformation.Name;
        
        if (VisualManager.Instance == null) return;
        _spriteRenderer.color = VisualManager.Instance.GetColorCard(CardInformation.WordCardType);
        _spriteRenderer.sprite = VisualManager.Instance.GetSpriteCard(CardInformation.WordCardType);
    }

    private void Start()
    {
        _wordText.text = CardInformation.Name;
        
        _spriteRenderer.color = VisualManager.Instance.GetColorCard(CardInformation.WordCardType);
        _spriteRenderer.sprite = VisualManager.Instance.GetSpriteCard(CardInformation.WordCardType);
    }

    public void OnSelect()
    {
        _spriteRenderer.color += _selectHighlightColor;
        
    }
    
    public void OnDeselect()
    {
        _spriteRenderer.color -= _selectHighlightColor;
        
    }
    
}
