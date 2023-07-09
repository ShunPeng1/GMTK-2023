using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Cards;
using UnityEngine;

public class CardExecutionButton : MonoBehaviour
{
    [SerializeField] private CardPlaceRegion _cardPlaceRegion;
    
    
    private void OnMouseDown()
    {
        try
        {
            Debug.Log("EXECUTE ");
            CardExecutionManager.Instance.Execute(_cardPlaceRegion.GetCardsInformation());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
