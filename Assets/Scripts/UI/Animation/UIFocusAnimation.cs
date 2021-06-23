using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

[Serializable]
public class UIFocusAnimation
{
    public bool particle;
    public ParticleSystem effect;
    public Vector3 toSize;
    public float duration;
    public int scalePerAction;
    public float delayTime;
    public bool animationLoop;
    public bool effectLoop;

    private Sequence sequence;

    public void Animate(UIAnimation animation)
    {
        Vector3 startSize = animation.transform.localScale;
        sequence = DOTween.Sequence();
        if(effect != null)
        {
            effect.gameObject.SetActive(true);
            effect.Play();
        }
        Scaling(animation.transform, startSize);
        //animation.StartCoroutine(Animating(animation, startSize));
    }

    public void Stop()
    {
        if (effect != null && effectLoop == false)
        {
            effect.Stop();
            effect.gameObject.SetActive(false);
        }
        sequence.Kill();
    }
    private void Scaling(Transform transform, Vector3 startSize)
    {
        for(int i = 0; i < scalePerAction; i++)
        {
            ViewManager.Scale(transform, toSize, duration / 2, sequence);
            ViewManager.Scale(transform, startSize, duration / 2, sequence);
        }
        int loop = animationLoop ? -1 : 0;
        sequence.SetDelay(delayTime);
        sequence.SetLoops(loop);
    }
}