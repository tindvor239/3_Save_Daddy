using DG.Tweening;
using UnityEngine;
using UnityEngine.CustomComponents;
using Spine.Unity;
using System.Collections.Generic;
using System.Collections;

public class CharacterController : Controller, IInteractable
{
    protected Sequence sequence;
    protected float moveDuration;
    [Header("Movement")]
    [SerializeField]
    protected List<Transform> destinations = new List<Transform>();
    [Header("Animation")]
    [SerializeField]
    protected SkeletonAnimation skeletonAnimation;
    [SerializeField]
    protected List<AnimationReferenceAsset> animationSet;

    public CharacterState state;

    [Header("Sound")]
    [SerializeField]
    protected Sound sound;

    protected float actingDelay;
    protected int deathCount = 0;
    #region Properties
    public Component Collider { get; private set; }
    public Sequence Sequence { get => sequence; }
    public SkeletonAnimation SkeletonAnimation { get => skeletonAnimation; }
    #endregion
    protected virtual void Awake()
    {
        sequence = DOTween.Sequence();
        Collider = GetComponent<Collider2D>();
    }
    protected override void Start()
    {
        sound.Initiate(gameObject, SoundManager.Instance.SoundSource);
    }
    protected virtual void Update()
    {
        Mathf.Clamp(actingDelay, 0, 3);
        actingDelay -= Time.deltaTime;
    }
    protected virtual void OnEnable()
    {
        sequence = DOTween.Sequence();
        StartCoroutine(SwitchingState(CharacterState.idle, 0.01f));
        deathCount = 0;
    }
    public bool CheckPathIsBlocked(in Vector3 from, in Vector3 to)
    {
        bool isBlockedByTerrain = GameManager.Instance.IsBlocked(from, to, 1 << LayerMask.NameToLayer("Default"));
        bool isBlockedByPin = GameManager.Instance.IsBlocked(from, to, 1 << LayerMask.NameToLayer("Pin"));
        return isBlockedByTerrain == false && isBlockedByPin == false ? false : true;
    }
    public void Move(in Vector2 destination, in Ease ease)
    {
        if(state != CharacterState.die)
        {
            moveDuration = MoveDuration(transform.position, destination, moveSpeed);
            GameController.Instance.Move(in sequence, transform, destination, moveDuration, ease);
            Invoke("OnMove", actingDelay);
        }
    }
    public override void Move(in Vector3 destination)
    {
        if (state != CharacterState.die)
        {
            moveDuration = MoveDuration(transform.position, destination, moveSpeed);
            GameController.Instance.Move(in sequence, transform, destination, moveDuration);
            Invoke("OnMove", actingDelay);
        }
    }
    public void Stop()
    {
        transform.DOKill();
    }
    public void Pause()
    {
        transform.DOPause();
    }
    public virtual void Continue()
    {
        transform.DOPlay();
    }
    public virtual void Interact()
    {
        
    }

    protected virtual void Action()
    {
        
    }
    protected void OnMove()
    {
        state = CharacterState.move;
        Action();
    }
    protected virtual void SwitchAction(AnimationReferenceAsset startAnimation, AnimationReferenceAsset endAnimation)
    {
        actingDelay = 2;
        StartCoroutine(SwitchingAct(endAnimation, false, 1));
        StartCoroutine(SwitchingState(2));
        ViewManager.Acting(skeletonAnimation, startAnimation, false, 1);
    }
    protected virtual void Acting(AnimationReferenceAsset animation, bool isLooping)
    {
        ViewManager.Acting(skeletonAnimation, animation, isLooping, 1);
    }

    protected IEnumerator SwitchingAct(AnimationReferenceAsset animation, bool isLooping, float startSecond)
    {
        if(gameObject.activeInHierarchy)
        {
            yield return new WaitForSeconds(startSecond);
            ViewManager.Acting(skeletonAnimation, animation, isLooping, 1);
        }
    }
    protected IEnumerator SwitchingState(CharacterState state, float startSecond)
    {
        yield return new WaitForSeconds(startSecond);
        this.state = state;
        Action();
    }
    protected IEnumerator SwitchingState(float startSecond)
    {
        yield return new WaitForSeconds(startSecond);
        Action();
    }
    protected virtual IEnumerator OnBeenKilled(float duration)
    {
        yield return new WaitForSeconds(duration);
    }

    public enum CharacterState { idle, die, move}
    
}
