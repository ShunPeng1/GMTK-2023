using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelGenerator : MonoBehaviour
{
    [SerializeField] private Transform _centerParent;
    [SerializeField] private List<NumberSlot> _numberSlots;

    [SerializeField] private float _arcSpacing = 0.1f;
    [SerializeField] private float _numberSlotRange = 10f;
    [SerializeField] private float _numberSlotThickness = 3f;
    [SerializeField] private float _numberSlotTextSize = 36f;
    void Start()
    {
        
        
    }

    private void GenerateWheel()
    {
        float currentDegree = 0;
        float arcDegree = CalculateArcAngle(_numberSlots.Count, _arcSpacing); 
        foreach (var numberSlot in _numberSlots)
        {
            var disc = Instantiate(ResourceManager.Instance.WheelNumberSlot, transform);
            WheelArc wheelArc = new WheelArc(
                currentDegree,
                currentDegree + arcDegree,
                _numberSlotRange,
                _numberSlotThickness,
                _numberSlotTextSize);
            disc.Init(numberSlot, wheelArc);
            
            currentDegree += arcDegree;

        }
        
    }
    
    float CalculateArcAngle(int numArcs, float spacing)
    {
        // Calculate the total angle based on the number of arcs
        float totalAngle = (numArcs - 1) * spacing;

        // Calculate the angle for each arc
        float arcAngle = totalAngle / numArcs;

        return arcAngle;
    }
}
