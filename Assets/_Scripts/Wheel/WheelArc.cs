using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelArc
{

    public float ArcStartDegree;
    public float ArcEndDegree;
    public float WheelRange;
    public float ArcThickness;
    public float TextSize;
    
    public WheelArc(float arcStartDegree, float arcEndDegree, float wheelRange, float arcThickness, float textSize)
    {
        ArcStartDegree = arcStartDegree;
        ArcEndDegree = arcEndDegree;
        WheelRange = wheelRange;
        ArcThickness = arcThickness;
        TextSize = textSize;
    }
    
}
