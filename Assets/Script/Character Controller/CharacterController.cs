using DG.Tweening;
using UnityEngine;
using Spine.Unity;
using System.Collections.Generic;
using System.Collections;

public class CharacterController : Controller, IInteractable
{
    protected Sequence sequence;
    protected float moveDuration;
    [SerializeField]
    protected SkeletonAnimation skeleton;
    [SerializeField]
    protected List<Character> characters;
    [SerializeField]
    protected CharacterState state;
    protected float actingDelay;

    #region Properties
    public new Component collider { get; private set; }
    public Sequence Sequence { get => sequence; }
    #endregion
    protected virtual void Awake()
    {
        sequence = DOTween.Sequence();
        collider = GetComponent<Collider2D>();
    }
    protected override void Start()
    {
        base.Start();

    }
    protected virtual void Update()
    {
        Mathf.Clamp(actingDelay, 0, 3);
        actingDelay -= Time.deltaTime;
    }
    public bool CheckPathIsBlocked(in Vector3 from, in Vector3 to)
    {
        bool isBlockedByTerrain = GameManager.Instance.IsBlocked(from, to, 1 << LayerMask.NameToLayer("Default"));
        bool isBlockedByPin = GameManager.Instance.IsBlocked(from, to, 1 << LayerMask.NameToLayer("Pin"));
        return isBlockedByTerrain == false && isBlockedByPin == false ? false : true;
    }
    public void Move(in Vector2 destination, in Ease ease)
    {
        moveDuration = MoveDuration(transform.position, destination);
        GameController.Instance.Move(in sequence, transform, destination, moveDuration, ease);
        Invoke("OnMove", actingDelay);
    }
    public override void Move(in Vector3 destination)
    {
        moveDuration = MoveDuration(transform.position, destination);
        GameController.Instance.Move(in sequence, transform, destination, moveDuration);
    }
    public void Stop()
    {
        transform.DOKill();
    }
    public virtual void Interact()
    {
        
    }

    protected void Action(CharacterState state)
    {
        if(characters.Count != 0)
        {
            switch(state)
            {
                case CharacterState.idle:
                    ActingLoop(characters[0].Data.Idle);
                    break;
                case CharacterState.die:
                    SwitchAction(characters[0].Data.Die, characters[0].Data.DieToMove);
                    break;
                case CharacterState.move:
                    SwitchAction(characters[0].Data.IdleToMove, characters[0].Data.Move);
                    StartCoroutine(SwitchingState(CharacterState.idle, 1));
                    break;
            }
        }
    }
    protected void OnMove()
    {
        state = CharacterState.move;
        Action(state);
    }
    protected virtual void SwitchAction(AnimationReferenceAsset startAnimation, AnimationReferenceAsset endAnimation)
    {
        actingDelay = 2;
        StartCoroutine(SwitchingAct(endAnimation, false, 1));
        StartCoroutine(SwitchingState(2));
        ViewManager.Acting(skeleton, startAnimation, false, 1);
    }
    protected virtual void ActingLoop(AnimationReferenceAsset animation)
    {
        ViewManager.Acting(skeleton, animation, true, 1);
    }
    private IEnumerator SwitchingAct(AnimationReferenceAsset animation, bool isLooping, float startSecond)
    {
        yield return new WaitForSeconds(startSecond);
        ViewManager.Acting(skeleton, animation, isLooping, 1);
    }
    private IEnumerator SwitchingState(CharacterState state, float startSecond)
    {
        yield return new WaitForSeconds(startSecond);
        this.state = state;
        Action(state);
    }
    private IEnumerator SwitchingState(float startSecond)
    {
        yield return new WaitForSeconds(startSecond);
        Action(state);
    }
    public enum CharacterState { idle, die, move}
    
}
