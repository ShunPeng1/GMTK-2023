using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Cards;
using UnityEngine;
using UnityUtilities;

public class VisualManager : SingletonMonoBehaviour<VisualManager>
{

    [SerializeField] private Color _inOutJigsawColor;
    [SerializeField] private Color _outInJigsawColor;
    [SerializeField] private Color _inInJigsawColor;
    [SerializeField] private Color _outOutJigsawColor;

    [SerializeField] private Sprite _inOutJigsawColorSprite;
    [SerializeField] private Sprite _outInJigsawColorSprite;
    [SerializeField] private Sprite _inInJigsawColorSprite;
    [SerializeField] private Sprite _outOutJigsawColorSprite;


    [SerializeField] private AudioClip BGM;
    private void Awake()
    {
        //SoundManager.Instance.PlayBGM(BGM);
    }
    public Color GetColorCard(WordCardType wordCardType)
    {
        return wordCardType switch
        {
            WordCardType.Noun => _inInJigsawColor,
            WordCardType.VerbPrefixUnary => _outInJigsawColor,
            WordCardType.VerbPostfixUnary => _inOutJigsawColor,
            WordCardType.VerbBinary => _outOutJigsawColor,
            WordCardType.VerbTernary => _inInJigsawColor,
            WordCardType.Value => _inInJigsawColor,

            _ => Color.white
        };
    }
    
    public Sprite GetSpriteCard(WordCardType wordCardType)
    {
        return wordCardType switch
        {
            WordCardType.Noun => _inInJigsawColorSprite,
            WordCardType.VerbPrefixUnary => _outInJigsawColorSprite,
            WordCardType.VerbPostfixUnary => _inOutJigsawColorSprite,
            WordCardType.VerbBinary => _outOutJigsawColorSprite,
            WordCardType.VerbTernary => _inInJigsawColorSprite,
            WordCardType.Value => _inInJigsawColorSprite,

            _ => _inOutJigsawColorSprite
        };
    }
    
}
