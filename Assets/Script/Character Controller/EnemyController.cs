using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.CustomComponents;
using DG.Tweening;

public class EnemyController : CharacterController
{
    [SerializeField]
    private List<Transform> destinations = new List<Transform>();
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
    }
    public void GetPlayer()
    {
        if (!sequence.IsActive())
        {
            moveDuration = 0;
        }
        if (!CheckPathIsBlocked(transform.position, GameManager.Instance.Player.transform.position))
        {
            bool isBlocked = GameManager.Instance.IsBlocked(transform.position, GameManager.Instance.Player.transform.position, 1 << LayerMask.NameToLayer("Player"));
            if (!isBlocked && gameObject.activeInHierarchy)
            {
                StartCoroutine(MoveToDestination(GameManager.Instance.Player.transform.position));
                StartCoroutine(Kill(moveDuration));
            }
        }
        else
        {
            Transform closestDestination = GetClosestDestination();

            //If already find closestDestination
            if (closestDestination != transform)
            {
                if (!CheckPathIsBlocked(transform.position, GameManager.Instance.Player.transform.position))
                {
                    StartCoroutine(MoveToDestination(closestDestination.position));
                }
            }
        }
    }

    
    private Transform GetClosestDestination()
    {
        destinations = FindDestination();
        Transform closestDestination = transform;
        Vector3 playerPosition = GameManager.Instance.Player.transform.position;
        foreach (Transform destination in destinations)
        {
            if (closestDestination == transform ||
                Vector3.Distance(playerPosition, closestDestination.position) > Vector3.Distance(playerPosition, destination.position))
            {
                closestDestination = destination;
            }
        }

        return closestDestination;
    }

    private List<Transform> FindDestination()
    {
        List<Transform> destinations = new List<Transform>();
        Vector2 direction = PlayerDirection();
        if(direction.x >= 0)
        {
            for (int i = GameManager.Instance.Destinations.Count - 1; i >= 0; i--)
            {
                if(GameManager.Instance.Destinations[i].position.x >= transform.position.x)
                {
                    destinations.Add(GameManager.Instance.Destinations[i]);
                }
            }
        }
        else if(direction.x < 0)
        {
            for(int i = 0; i < GameManager.Instance.Destinations.Count; i++)
            {
                if(GameManager.Instance.Destinations[i].position.x < transform.position.x)
                {
                    destinations.Add(GameManager.Instance.Destinations[i]);
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
