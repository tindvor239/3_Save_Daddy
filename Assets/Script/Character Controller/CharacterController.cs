﻿using DG.Tweening;
using UnityEngine;
using UnityEngine.CustomComponents;

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
}