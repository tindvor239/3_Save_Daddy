﻿using UnityEngine;
using DG.Tweening;
using UnityEngine.CustomComponents;

public class HolderPin : Pin
{
    [SerializeField]
    private Transform locker;
    [SerializeField]
    private Transform[] destinations = new Transform[2];
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
            Vector2 destination = FlipFlop();
            if(GameManager.IsBlocked(transform.position, locker.position, 1 << LayerMask.NameToLayer("Pin")) == false)
            {
                GameController.Move(transform, destination, unpinDuration);
            }
            collider.enabled = true;
        }
    }

    private Vector2 FlipFlop()
    {
        for (int i = 0; i < destinations.Length; i++)
        {
            if (Mathf.Round(destinations[i].position.x) == Mathf.Round(transform.position.x) &&
                Mathf.Round(destinations[i].position.y) == Mathf.Round(transform.position.y))
            {
                if (i == destinations.Length - 1)
                {
                    return destinations[i - 1].position;
                }
                else
                {
                    return destinations[i + 1].position;
                }
            }
        }
        return new Vector2();
    }
}
