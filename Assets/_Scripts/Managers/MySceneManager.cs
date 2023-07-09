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
        Application.OpenURL("https://rgsdev.itch.io/free-cc0-modular-animated-vector-characters-2d");
        Application.OpenURL("https://jdsherbert.itch.io/minigame-music-pack");
        Application.OpenURL("https://penzilla.itch.io/handdrawn-vector-icon-pack");
    }
    public void PlayButtonSound()
    {
        SoundManager.Instance.PlaySound(_buttonClip);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OpenSoundPanel();
        }
    }
    public void OpenSoundPanel()
    {
        if(SoundPanel.activeInHierarchy == true)
        {
            SoundPanel.SetActive(false);
        }
        else
        {
            SoundPanel.SetActive(true);
        }
    }
    
    public void GetNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneIndex);
    }

}
