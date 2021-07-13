using System.Collections;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField]
    private AudioSource music;
    [SerializeField]
    private AudioSource sound;
    #region Properties
    public AudioSource Music { get => music; }
    public AudioSource Sound { get => sound; }
    public static bool IsMuted
    {
        get
        {
            bool result = PlayerPrefs.GetInt("mute") == 1 ? true : false;
            if(result != Instance.Sound.enabled)
            {
                Instance.Sound.enabled = result;
            }
            return result;
        }
        set
        {
            int volume = value == true ? 1 : 0;
            PlayerPrefs.SetInt("mute", volume);
            Instance.Sound.enabled = value;
        }
    }
    public static bool IsMusicOn
    {
        get
        {
            bool result = PlayerPrefs.GetInt("musicOn") == 1 ? true : false;
            if(result != Instance.Music.enabled)
            {
                Instance.Music.enabled = result;
            }
            return result;
        }
        set
        {
            int volume = value == true ? 1 : 0;
            PlayerPrefs.SetInt("musicOn", volume);
            Instance.Music.enabled = value;
        }
    }
    #endregion
    protected override void Awake()
    {
        #region Singleton
        base.Awake();
        #endregion
    }
}
