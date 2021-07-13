using System.Collections;
using UnityEngine;

public class UISettings : UISection
{
    [SerializeField]
    private LeverUI sound;
    [SerializeField]
    private LeverUI music;
    [SerializeField]
    private LeverUI vibration;
    public void OnSoundLeverPulled(LeverUI lever)
    {
        bool isActive = SoundManager.IsMuted;
        lever.OnCLicked(out isActive);
        SoundManager.IsMuted = isActive;
        Debug.Log(SoundManager.IsMuted);
    }
    public void OnMusicLeverPulled(LeverUI lever)
    {
        bool isActive = SoundManager.IsMusicOn;
        lever.OnCLicked(out isActive);
        SoundManager.IsMusicOn = isActive;
        Debug.Log(SoundManager.IsMusicOn);
    }
    public void OnVibrationPulled(LeverUI lever)
    {
        bool isActive = GameManager.IsVibration;
        lever.OnCLicked(out isActive);
        GameManager.IsVibration = isActive;
        Debug.Log(GameManager.IsVibration);
    }

    private void Initiate()
    {
        sound.Initiate(SoundManager.IsMuted);
        music.Initiate(SoundManager.IsMusicOn);
        vibration.Initiate(GameManager.IsVibration);
    }
    private void Start()
    {
        Initiate();
    }
}
