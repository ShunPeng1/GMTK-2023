using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityUtilities;

public class SoundManager : SingletonMonoBehaviour<SoundManager> 
{
    [SerializeField] 
    private AudioSource BGM_AudioSource;
    [SerializeField]
    private AudioSource SFX_AudioSource;
    static SoundManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlaySound(AudioClip clip)
    {
        SFX_AudioSource.PlayOneShot(clip);
    }
    public void PlayBGM(AudioClip clip)
    {
        BGM_AudioSource.Stop();
        BGM_AudioSource.clip = clip;
        BGM_AudioSource.Play();
    }
    public void ToggleBGM()
    {
        BGM_AudioSource.mute = !BGM_AudioSource.mute;
    }
    public void ToggleSFX()
    {
        SFX_AudioSource.mute = !SFX_AudioSource.mute;
    }
    public void ChangeVolume(float volume)
    {
        BGM_AudioSource.volume = volume;
        SFX_AudioSource.volume = volume;
    }

}
