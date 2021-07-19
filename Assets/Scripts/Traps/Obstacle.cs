using System.Collections;
using UnityEngine;

public abstract class Obstacle : Model
{
    [SerializeField]
    protected Sound sound;
    #region Properties
    public Sound Sound { get => sound; }
    #endregion
    protected virtual void Start()
    {
        sound.Initiate(gameObject, SoundManager.Instance.SoundSource);
    }
    protected virtual void OnEnable()
    {
        if(sound.source == null && SoundManager.Instance != null)
        {
            sound.source = SoundManager.Instance.SoundSource;
        }
    }
    public virtual void OnBeingHit(GameObject hitObject)
    {
        
    }

    protected virtual void OnHit(GameObject beingHitObject)
    {

    }
}
