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
    }
    public void OnMusicLeverPulled(LeverUI lever)
    {
        bool isActive = SoundManager.IsMusicOn;
        lever.OnCLicked(out isActive);
        SoundManager.IsMusicOn = isActive;
    }

    private void Initiate()
    {
        sound.Initiate(SoundManager.IsMuted);
        music.Initiate(SoundManager.IsMusicOn);
    }
    private void Start()
    {
        Initiate();
    }
}
