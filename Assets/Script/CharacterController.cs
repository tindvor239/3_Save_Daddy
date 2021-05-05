using DG.Tweening;
using UnityEngine;

public class CharacterController : Controller, IInteractable
{
    protected Sequence sequence;
    protected float moveDuration;
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
    public void Move(in Vector2 destination, in Ease ease)
    {
        moveDuration = MoveDuration(transform.position, destination);
        GameController.Move(in sequence, transform, destination, moveDuration, ease);
    }
    public override void Move(in Vector3 destination)
    {
        moveDuration = MoveDuration(transform.position, destination);
        GameController.Move(in sequence, transform, destination, moveDuration);
    }
    public virtual void Interact()
    {
        
    }
}
