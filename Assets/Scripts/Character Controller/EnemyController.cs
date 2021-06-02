using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyController : CharacterController
{
    [SerializeField]
    protected List<Transform> destinations = new List<Transform>();
    [SerializeField]
    protected List<Transform> passedDestinations = new List<Transform>();
    [SerializeField]
    protected EnemyState enemyState;
    [SerializeField]
    protected float attackRange;
    protected float startSize;

    protected float timer = 0;
    protected float maxTimer = 4f;
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        startSize = transform.localScale.x;

    }
    protected override void Update()
    {
        switch(enemyState)
        {
            case EnemyState.partrolling:
                GameManager.Instance.OnHitCallBack(ref timer, maxTimer, GetPlayer);
                if (!sequence.IsActive())
                {

                    if (transform.localScale.x <= -1 && destinations[0] != null)
                    {
                        if((transform.position.x >= destinations[0].position.x || timer >= maxTimer) && destinations[1] != null)
                        {
                            Rotate(destinations[1].position);
                            timer = 0;
                        }
                    }
                    else if(transform.localScale.x >= 1 && destinations[1] != null)
                    {
                        if((transform.position.x <= destinations[1].position.x || timer >= maxTimer) && destinations[0] != null)
                        {
                            Rotate(destinations[0].position);
                            timer = 0;
                        }
                    }
                }
                break;
        }
    }
    protected void FixedUpdate()
    {
        switch(enemyState)
        {
            case EnemyState.partrolling:
                if(state == CharacterState.idle)
                {
                    transform.position -= (transform.right * Time.deltaTime * moveSpeed) * transform.localScale.x;
                }
                break;
        }
    }
    protected void OnDisable()
    {
        foreach(Transform destination in destinations)
        {
            if(enemyState == EnemyState.partrolling)
            {
                Destroy(destination.gameObject);
            }
        }
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        OnStartPartrol();
        timer = 0;
        passedDestinations = new List<Transform>();
    }

    public virtual void GetPlayer()
    {
        PlayerController player = GameManager.Instance.Player;
        Transform path = GetPath();
        if (!sequence.IsActive())
        {
            moveDuration = 0;
        }
        if (path != null)
        {
            if(path == player.transform)
            {
                StartCoroutine(MoveToDestination(player.transform));
                StartCoroutine(Kill(MoveDuration(transform.position, player.transform.position)));
            }
            else
            {
                if(enemyState == EnemyState.aggressive)
                {
                    StartCoroutine(MoveToDestination(path));
                    StartCoroutine(SwitchingState(CharacterState.idle, moveDuration));
                }
            }
        }
    }
    
    private void OnStartPartrol()
    {
        destinations = new List<Transform>();
        if(enemyState == EnemyState.partrolling)
        {
            float patrol = 5;
            for(int i = 0; i < 2; i++)
            {
                GameObject newObject = new GameObject("partrol");
                patrol *= -1;
                newObject.transform.position = new Vector2(transform.position.x - patrol, transform.position.y);
                destinations.Add(newObject.transform);
            }
        }
    }
    private Transform GetPath()
    {
        bool pathIsBlocked;
        switch(enemyState)
        {
            case EnemyState.partrolling:
                PlayerController player = GameManager.Instance.Player;
                if(player != null)
                {
                    pathIsBlocked = CheckPathIsBlocked(transform.position, player.transform.position);
                    if(!pathIsBlocked)
                    {
                        return player.transform;
                    }
                }
                break;
            default:
                destinations = FindClosestDestinations();
                if (destinations.Contains(transform))
                {
                    destinations.Remove(transform);
                }
                foreach (Transform destination in destinations)
                {
                    pathIsBlocked = CheckPathIsBlocked(transform.position, destination.position);
                    if (!pathIsBlocked)
                    {
                        return destination;
                    }
                }
                break;
        }
        return null;
    }
    private List<Transform> FindClosestDestinations()
    {
        //Get Closest Destination Is Closest To Player
        List<Transform> paths = GetPaths();
        List<Transform> inRangePaths = GetInRangePaths(paths);
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
    private List<Transform> GetPaths()
    {
        List<Transform> result = new List<Transform>();
        List<Transform> paths = GameManager.Instance.Destinations;
        for (int i = 0; i < paths.Count; i++)
        {
            result.Add(paths[i]);
        }
        return result;
    }
    private List<Transform> GetInRangePaths(List<Transform> paths)
    {
        List<Transform> result = new List<Transform>();
        foreach(Transform destination in paths)
        {
            float currentDistance = Vector2.Distance(transform.position, destination.position);
            if(currentDistance <= attackRange && destination.transform.position != transform.position)
            {
                result.Add(destination);
            }
        }
        return result;
    }
    private IEnumerator MoveToDestination(Transform destination)
    {
        yield return new WaitForSeconds(moveDuration);
        int index = GameManager.Instance.GetCurrentPosIndex(destination, destinations);
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
                index = GameManager.Instance.GetCurrentPosIndex(transform, destinations);
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

#if UNITY_EDITOR
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
#endif
    protected IEnumerator Kill(float duration)
    {
        yield return new WaitForSeconds(duration);
        Acting(animationSet[1], false);
        UIController.Instance.ShowGameOverUI(true);
    }
    protected override IEnumerator OnBeenKilled(float duration)
    {
        yield return new WaitForSeconds(duration);
        CharacterPoolParty.Instance.Party.GetPool(poolName).GetBackToPool(gameObject);
    }
    protected enum EnemyState { standing, aggressive, partrolling }
    protected override void Action()
    {
        if (skeleton != null)
        {
            switch (state)
            {
                case CharacterState.idle:
                    Acting(animationSet[0], true);
                    break;
                case CharacterState.move:
                    Acting(animationSet[0], true);
                    break;
                case CharacterState.die:
                    Acting(animationSet[2], false);
                    break;
            }
        }
    }

    public override void Interact()
    {
        state = CharacterState.die;
        Action();
        StartCoroutine(OnBeenKilled(1.3f));
    }

}
