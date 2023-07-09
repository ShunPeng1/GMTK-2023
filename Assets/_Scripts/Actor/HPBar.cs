using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    // Start is called before the first frame update
    Transform Bar;
    void Start()
    {
        Bar = transform.Find("Bar");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
