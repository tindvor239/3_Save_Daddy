using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.CustomComponents;
using DG.Tweening;

public class EnemyController : CharacterController
{
    [SerializeField]
    private List<Transform> destinations = new List<Transform>();
    protected override void Start()
    {
        base.Start();
    }
    public void GetPlayer()
    {
        bool isBlockedByTerrain = GameManager.IsBlocked(transform.position, GameManager.Instance.Player.transform.position, 1 << LayerMask.NameToLayer("Default"));
        bool isBlockedByPin = GameManager.IsBlocked(transform.position, GameManager.Instance.Player.transform.position, 1 << LayerMask.NameToLayer("Pin"));
        if (sequence != null && sequence.IsPlaying() == false)
        {
            moveDuration = 0;
        }
        if (!isBlockedByTerrain && !isBlockedByPin)
        {
            Debug.Log("Get Player");
            bool isBlocked = GameManager.IsBlocked(transform.position, GameManager.Instance.Player.transform.position, 1 << LayerMask.NameToLayer("Player"));
            if (!isBlocked && gameObject.activeInHierarchy)
            {
                StartCoroutine(MoveToDestination(GameManager.Instance.Player.transform.position));
                StartCoroutine(Kill(moveDuration));
            }
        }
        else
        {
            destinations = FindDestination();
            foreach(Transform destination in destinations)
            {
                isBlockedByTerrain = GameManager.IsBlocked(transform.position, destination.position, 1 << LayerMask.NameToLayer("Default"));
                isBlockedByPin = GameManager.IsBlocked(transform.position, destination.position, 1 << LayerMask.NameToLayer("Pin"));
                if (!isBlockedByTerrain && !isBlockedByPin)
                {
                    Debug.Log("Get To Destination");
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
        return GameManager.GetDirectionVector(transform.position, GameManager.Instance.Player.transform.position);
    }
    private IEnumerator MoveToDestination(Vector2 destination)
    {
        yield return new WaitForSeconds(moveDuration);
        Move(destination, Ease.Linear);
    }
    protected IEnumerator Kill(float duration)
    {
        yield return new WaitForSeconds(duration);
        CharacterPoolParty.Instance.PlayerPool.GetBackToPool(GameManager.Instance.Player.gameObject);
    }

    public override void Interact()
    {
        CharacterPoolParty.Instance.SharkPool.GetBackToPool(gameObject);
    }
}
