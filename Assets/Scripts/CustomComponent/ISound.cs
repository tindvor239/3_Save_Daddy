using System;
using System.Collections;
using UnityEngine;
public interface ISound
{
    void PlayOnce(AudioClip clip);
    void PlayLoop(AudioClip clip);
}
[Serializable]
public class Sound : ISound
{
    public AudioClip clip;
    public AudioSource source;
    public GameObject gameObject;

    public void PlayOnce(AudioClip clip)
    {
        if (source != null && clip != null)
        {
            source.PlayOneShot(clip);
        }
    }
    public void PlayLoop(AudioClip clip)
    {
        Debug.Log(source);
        if (source != null && clip != null)
        {
            Debug.Log("PlayLooping");
            source.PlayOneShot(clip);
            SoundManager.Instance.StartCoroutine(PlayClipLoop(clip));
        }
    }
    public void Initiate(GameObject gameObject, AudioSource source)
    {
        this.gameObject = gameObject;
        this.source = source;
    }

    private IEnumerator PlayClipLoop(AudioClip clip)
    {
        yield return new WaitForSeconds(clip.length);
        if(gameObject.activeInHierarchy || GameManager.State != GameManager.GameState.pause)
        {
            PlayLoop(clip);
        }
    }
}
