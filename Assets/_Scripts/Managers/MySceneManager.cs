using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityUtilities;

public class MySceneManager : SingletonMonoBehaviour<MySceneManager>
{
    [SerializeField] private AudioClip _buttonClip;
    [SerializeField] private AudioClip _BGMClip;
    [SerializeField] GameObject SoundPanel;
    private void Start()
    {
        SoundManager.Instance.PlayBGM(_BGMClip);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Thuan Testing 1");
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void HowToPlay()
    {
        //SceneManager.LoadScene("How To Play");
    }
    public void BackToMenu()
    {
        //SceneManager.LoadScene("MainMenu");
    }
    public void Credit()
    {
        //SceneManager.LoadScene("Credit");
    }
    public void PlayButtonSound()
    {
        SoundManager.Instance.PlaySound(_buttonClip);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SoundPanel.SetActive(true);
        }
    }
}
