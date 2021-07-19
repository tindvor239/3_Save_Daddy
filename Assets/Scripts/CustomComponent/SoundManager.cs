using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class SoundManager : Singleton<SoundManager>
{
    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource soundSource;
    #region Properties
    public AudioSource MusicSource { get => musicSource; }
    public AudioSource SoundSource { get => soundSource; }
    public static bool IsMuted
    {
        get
        {
            bool result = PlayerPrefs.GetInt("mute") == 1 ? true : false;
            return result;
        }
        set
        {
            int volume = value == true ? 1 : 0;
            PlayerPrefs.SetInt("mute", volume);
        }
    }
    public static bool IsMusicOn
    {
        get
        {
            bool result = PlayerPrefs.GetInt("musicOn") == 1 ? true : false;
            return result;
        }
        set
        {
            int volume = value == true ? 1 : 0;
            PlayerPrefs.SetInt("musicOn", volume);
        }
    }
    #endregion

    protected override void Awake()
    {
        #region Singleton
        base.Awake();
        #endregion
    }
    public void StopAllSound()
    {
        if(IsMuted)
        {
            soundSource.enabled = false;
        }
        if(IsMusicOn)
        {
            musicSource.enabled = false;
        }
        StopAllCoroutines();
    }

    public void Continue()
    {
        if(IsMuted)
        {
            soundSource.enabled = true;
        }
        if(IsMusicOn)
        {
            musicSource.enabled = true;
        }
        if (Sound.onContinue != null)
        {
            Sound.onContinue.Invoke();
        }
    }
}
