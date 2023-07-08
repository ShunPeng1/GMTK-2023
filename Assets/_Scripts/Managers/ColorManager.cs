using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Data;
using UnityEngine;
using UnityUtilities;

public class ColorManager : SingletonMonoBehaviour<ColorManager>
{

    [SerializeField] private Color _redSlotColor;
    [SerializeField] private Color _greenSlotColor;
    [SerializeField] private Color _blueSlotColor;
    [SerializeField] private Color _blackSlotColor;
    [SerializeField] private Color _yellowSlotColor;


    public Color GetSlotColor(SlotColor slotColor)
    {
        return slotColor switch
        {
            SlotColor.Red => _redSlotColor,
            SlotColor.Black => _blackSlotColor,
            SlotColor.Green => _greenSlotColor,
            _ => Color.white
        };
    }
}
