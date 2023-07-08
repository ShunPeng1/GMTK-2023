using System.Collections;
using System.Collections.Generic;
using _Scripts.Data;
using UnityEngine;


[CreateAssetMenu(fileName = "New Number Slot", menuName = "ScriptableObject/Number Slot")]
public class NumberSlot : ScriptableObject
{
    public int Number;
    public SlotColor SlotColor;
    
    public NumberSlot(int number, SlotColor color, SlotColor slotColor)
    {
        Number = number;
        SlotColor = slotColor;
    }
}
