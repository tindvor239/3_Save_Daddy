using System.Collections;
using System.Collections.Generic;
using Coffee.UIEffects;
using UnityEngine;

public class UIAnimation : MonoBehaviour
{
    public UIFocusAnimation focusAnimation;
    public UIShiny shinyAnimation;
    public AnimationType type;
    public UIFocusAnimation lastFocusAnimation;
    public bool playOnStart;
    private void OnEnable()
    {
        if(focusAnimation != null && playOnStart)
        {
            Play();
        }
    }

    public void Play()
    {
        switch (type)
        {
            case AnimationType.focus:
                focusAnimation.Animate(this);
                break;
            case AnimationType.shiny:
                break;
        }
    }
    public void Stop()
    {
        switch(type)
        {
            case AnimationType.focus:
                focusAnimation.Stop();
                break;
            case AnimationType.shiny:
                break;
        }
    }
}
public enum AnimationType {focus, shiny}
