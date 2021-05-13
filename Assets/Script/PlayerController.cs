﻿using System.Collections;
using UnityEngine;
using DG.Tweening;
public class PlayerController : CharacterController
{
    protected override void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        poolName = CharacterPoolParty.Instance.PlayerPool.Name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MovePlayerToNextDestination()
    {
        int index = GameManager.GetNextDestinationIndex(this);
        if (index != -1)
        {
            Vector3 pathPosition = GameManager.Instance.Destinations[index].position;
            bool isBlockedByTerrain = GameManager.IsBlocked(transform.position, pathPosition, 1 << LayerMask.NameToLayer("Default"));
            bool isBlockedByPin = GameManager.IsBlocked(transform.position, pathPosition, 1 << LayerMask.NameToLayer("Pin"));
            if (!isBlockedByTerrain && !isBlockedByPin)
            {
                bool isBlocked = GameManager.IsBlocked(transform.position, pathPosition, 1 << LayerMask.NameToLayer("Enemy"));
                if (!isBlocked)
                {
                    if(sequence != null && sequence.IsPlaying() == false)
                    {
                        moveDuration = 0;
                    }
                    StartCoroutine(MoveToDestination(GameManager.Instance.Destinations[index]));
                }
            }
        }
    }
    IEnumerator MoveToDestination(Transform destination)
    {
        yield return new WaitForSeconds(moveDuration);
        StartMoveToDestination(destination);
        Invoke("CheckMoveCondition", moveDuration);
    }
    private void StartMoveToDestination(Transform destination)
    {
        int index = GameManager.GetDestinationIndex(destination);
        if (index != -1)
        {
            GameManager.Instance.PassedDestinations.Add(GameManager.Instance.Destinations[index]);
            GameManager.Instance.Destinations[index] = transform;
            Move(destination.position, Ease.Linear);
            if (index > 0)
            {
                index = GameManager.GetDestinationIndex(transform);
                GameManager.Instance.Destinations[index] = GameManager.Instance.PassedDestinations[0];
                GameManager.Instance.PassedDestinations.Remove(GameManager.Instance.Destinations[index]);
            }
        }
    }
    private void CheckMoveCondition()
    {
        if(!GameManager.isWin())
        {
            MovePlayerToNextDestination();
        }

    }
    //private void CheckCo
    public override void Interact()
    {
        Debug.Log("Interact with player");
        CharacterPoolParty.Instance.PlayerPool.GetBackToPool(gameObject);
    }
}
