using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyController : CharacterController
{
    [SerializeField]
    private List<Transform> destinations = new List<Transform>();
    [SerializeField]
    private List<Transform> passedDestinations = new List<Transform>();
    [SerializeField]
    private EnemyState state;
    [SerializeField]
    private float attackRange;
    private float startSize;
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        startSize = transform.localScale.x;
    }
    public void GetPlayer()
    {
        PlayerController player = GameManager.Instance.Player;
        if (!sequence.IsActive())
        {
            moveDuration = 0;
        }

        if (!CheckPathIsBlocked(transform.position, player.transform.position) && Vector2.Distance(transform.position, player.transform.position) <= attackRange)
        {
            bool isBlocked = GameManager.Instance.IsBlocked(transform.position, GameManager.Instance.Player.transform.position, 1 << LayerMask.NameToLayer("Player"));
            if (!isBlocked && gameObject.activeInHierarchy)
            {
                StartCoroutine(MoveToDestination(GameManager.Instance.Player.transform));
                StartCoroutine(Kill(moveDuration));
            }
        }
        else
        {
            if(state == EnemyState.aggressive)
            {
                Transform path = GetPath();

                if (path != null && path != transform)
                {
                    StartCoroutine(MoveToDestination(path));
                }
            }
        }
    }
    
    private Transform GetPath()
    {
        destinations = FindClosestDestinations();
        foreach(Transform destination in destinations)
        {
            bool pathIsBlocked = CheckPathIsBlocked(transform.position, destination.position);
            if (!pathIsBlocked)
            {
                return destination;
            }
        }
        return null;
    }
    private List<Transform> FindClosestDestinations()
    {
        //Get Closest Destination Is Closest To Player
        List<Transform> pathsToPlayer = GetPathToPlayer();
        List<Transform> inRangePaths = GetInRangePaths(pathsToPlayer);
        //Sort Closest Destination Is Closest To Enemy
        List<Transform> closestPathsToPlayer = SortClosestDestinationsToEnemy(inRangePaths);
        return closestPathsToPlayer;
    }
    private List<Transform> SortClosestDestinationsToEnemy(List<Transform> inRangePaths)
    {
        List<Transform> inRangeDestination = inRangePaths;
        Transform[] result = new Transform[inRangeDestination.Count];
        for(int i = 0; i < result.Length; i++)
        {
            Transform closestTransform = null;
            float distance = -1;
            foreach(Transform destination in inRangeDestination)
            {
                float currentDistance = Vector2.Distance(transform.position, destination.position);
                if (distance == -1 || distance > currentDistance)
                {
                    distance = currentDistance;
                    closestTransform = destination;
                }
            }
            result[i] = closestTransform;
            inRangeDestination.Remove(closestTransform);
        }
        return result.ToList();
    }
    private List<Transform> GetPathToPlayer()
    {
        List<Transform> result = new List<Transform>();
        List<Transform> paths = GameManager.Instance.Destinations;
        for (int i = 0; i < paths.Count; i++)
        {
            PlayerController player = GameManager.Instance.Player;
            if (paths[i] == player.transform)
            {
                for(int j = i; j < paths.Count; j++)
                {
                    result.Add(paths[j]);
                }
                return result;
            }
        }
        return result;
    }
    private List<Transform> GetInRangePaths(List<Transform> paths)
    {
        List<Transform> result = new List<Transform>();
        foreach(Transform destination in GameManager.Instance.Destinations)
        {
            float currentDistance = Vector2.Distance(transform.position, destination.position);
            if(currentDistance <= attackRange)
            {
                result.Add(destination);
            }
        }
        return result;
    }
    private IEnumerator MoveToDestination(Transform destination)
    {
        yield return new WaitForSeconds(moveDuration);
        int index = GameManager.GetDestinationIndex(destination, destinations);
        if (index != -1)
        {
            if (!passedDestinations.Contains(destinations[index]))
            {
                passedDestinations.Add(destinations[index]);
            }
            destinations[index] = transform;
            Move(destination.position, Ease.Linear);
            Rotate(destination.position);
            if (index > 0)
            {
                index = GameManager.GetDestinationIndex(transform, destinations);
                destinations[index] = passedDestinations[0];
                passedDestinations.Remove(destinations[index]);
            }
        }
    }
    private void Rotate(Vector2 destination)
    {
        if (destination.x > transform.position.x)
        {
            Vector3 negativeScale = new Vector3(-startSize, transform.localScale.y, transform.localScale.z);
            transform.localScale = negativeScale;
        }
        else
        {
            Vector3 negativeScale = new Vector3(startSize, transform.localScale.y, transform.localScale.z);
            transform.localScale = negativeScale;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
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

    private enum EnemyState { standing, aggressive, partrolling }
}
