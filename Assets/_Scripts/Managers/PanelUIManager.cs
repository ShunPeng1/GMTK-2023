using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelUIManager : MonoBehaviour
{
    [SerializeField] 
    private Scrollbar Soundbar;
    public void Volume()
    {
        SoundManager.Instance.ChangeVolume(Soundbar.value);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }
}
