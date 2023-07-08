using Shapes;
using TMPro;
using UnityEngine;


public class WheelNumberSlot : MonoBehaviour
{
    [SerializeField] private NumberSlot _numberSlot;
    [SerializeField] private WheelArc _wheelArc;
    [SerializeField] private Disc _disc;
    [SerializeField] private TMP_Text _text;

    public void Init(NumberSlot numberSlot, WheelArc wheelArc)
    {
        _disc.transform.rotation = Quaternion.Euler(0,0,wheelArc.ArcStartDegree);
        _disc.AngRadiansEnd = wheelArc.ArcEndDegree * Mathf.Deg2Rad;
        _disc.Radius = wheelArc.WheelRange;
        _disc.Color = ColorManager.Instance.GetSlotColor(numberSlot.SlotColor);
        _disc.Thickness = wheelArc.ArcThickness;
        
        _text.text = numberSlot.Number.ToString();
        _text.transform.localPosition = new Vector3(wheelArc.WheelRange, wheelArc.WheelRange * Mathf.Tan( _disc.AngRadiansEnd/2) ,0);
        _text.transform.localRotation = Quaternion.Euler(0, 0, -90f + _disc.AngRadiansEnd/2);
    }
}
    