﻿using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class ReleasePin : Pin
{
    [SerializeField]
    private Transform anchor;
    [SerializeField]
    private Transform forward;
    [SerializeField]
    private Transform locker;
    protected override void Start()
    {
        base.Start();
    }
    protected override void Unpin()
    {
        if (isAlreadyUnpin == false)
        {
            base.Unpin();
            collider.enabled = false;
            if(GameManager.IsBlocked(transform.position, locker.position) == false)
            {
                Vector2 pullDirection = (anchor.position - forward.position).normalized;
                GameController.Move(transform, (Vector2)forward.position + pullDirection * 10, unpinDuration);
            }
            collider.enabled = true;
        }
    }
}
