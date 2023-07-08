using System.Collections;
using System.Collections.Generic;
using _Scripts.Cards;
using UnityEngine;


public class BaseCard : MonoBehaviour
{
    protected BaseCardInformation CardInformation;
    
    protected bool IsDragging = false;
    protected Vector3 Offset;

    private void OnMouseDown()
    {
        if (!IsDragging)
        {
            Offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            IsDragging = true;
        }
    }

    private void OnMouseDrag()
    {
        if (IsDragging)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Offset;
            transform.position = new Vector3(newPosition.x, newPosition.y, 0f);
        }
    }

    private void OnMouseUp()
    {
        IsDragging = false;
        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
    }
}
