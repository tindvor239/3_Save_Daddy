using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.CustomComponents;
using DG.Tweening;

public class EnemyController : CharacterController
{
    private CharacterController player;
    [SerializeField]
    private List<Transform> destinations = new List<Transform>();
    protected override void Start()
    {
        base.Start();
        player = GameManager.Instance.Player;
    }
    public void GetPlayer()
    {
        bool isBlocked = GameManager.IsBlocked(transform.position, player.transform.position, 1 << LayerMask.NameToLayer("Default"));
        if (sequence != null && sequence.IsPlaying() == false)
        {
            moveDuration = 0;
        }
        if (!isBlocked)
        {
            isBlocked = GameManager.IsBlocked(transform.position, player.transform.position, 1 << LayerMask.NameToLayer("Player"));
            if (!isBlocked && gameObject.activeInHierarchy)
            {
                StartCoroutine(MoveToDestination(player.transform.position));
                StartCoroutine(Kill(moveDuration));
            }
        }
        else
        {
            destinations = FindDestination();
            foreach(Transform destination in destinations)
            {
                isBlocked = GameManager.IsBlocked(transform.position, destination.position, 1 << LayerMask.NameToLayer("Default"));
                if(!isBlocked)
                {
                    StartCoroutine(MoveToDestination(destination.position));
                }
            }
        }
    }
    private List<Transform> FindDestination()
    {
        List<Transform> destinations = new List<Transform>();
        Vector2 direction = PlayerDirection();
        if(direction.x >= 0)
        {
            for (int i = gameManager.Destinations.Count - 1; i >= 0; i--)
            {
                if(gameManager.Destinations[i].position.x >= transform.position.x)
                {
                    destinations.Add(gameManager.Destinations[i]);
                }
            }
        }
        else if(direction.x < 0)
        {
            for(int i = 0; i < gameManager.Destinations.Count; i++)
            {
                if(gameManager.Destinations[i].position.x < transform.position.x)
                {
                    destinations.Add(gameManager.Destinations[i]);
                }
            }
        }
        return destinations;
    }
    private Vector2 PlayerDirection()
    {
        return GameManager.GetDirectionVector(transform.position, player.transform.position);
    }
    private IEnumerator MoveToDestination(Vector2 destination)
    {
        yield return new WaitForSeconds(moveDuration);
        Move(destination, Ease.Linear);
    }
    protected IEnumerator Kill(float duration)
    {
        yield return new WaitForSeconds(duration);
        CharacterPoolParty.Instance.PlayerPool.GetBackToPool(player.gameObject);
    }

    public override void Interact()
    {
        CharacterPoolParty.Instance.SharkPool.GetBackToPool(gameObject);
    }
}
