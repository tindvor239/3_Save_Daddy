using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using DG.Tweening;

public class EnemyController : CharacterController
{
    [SerializeField]
    protected EnemyState enemyState;
    [SerializeField]
    protected float attackRange;
    protected float startSize;
    protected float timer = 0;
    protected float maxTimer = 4f;
    [HideInInspector]
    public Vector2 startPatrol;
    [SerializeField]
    private AudioClip attackSound;
    [SerializeField]
    private AudioClip normalSound;
    [SerializeField]
    private AudioClip deathSound;
    #region Properties
    public EnemyState State { get => enemyState; }
    #endregion
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
        if (GameManager.State == GameManager.GameState.play)
        {
            switch (enemyState)
            {
                case EnemyState.patrolling:
                    GameManager.Instance.OnHitCallBack(ref timer, maxTimer, GetPlayer);
                    if (!sequence.IsActive())
                    {

                        if (transform.localScale.x <= -1 && destinations[0] != null)
                        {
                            if ((transform.position.x >= destinations[0].position.x || timer >= maxTimer) && destinations[1] != null)
                            {
                                Rotate(destinations[1].position);
                                timer = 0;
                            }
                        }
                        else if (transform.localScale.x >= 1 && destinations[1] != null)
                        {
                            if ((transform.position.x <= destinations[1].position.x || timer >= maxTimer) && destinations[0] != null)
                            {
                                Rotate(destinations[0].position);
                                timer = 0;
                            }
                        }
                    }
                    break;
            }
        }
    }
    protected void FixedUpdate()
    {
        if (GameManager.State == GameManager.GameState.play)
        {
            switch (enemyState)
            {
                case EnemyState.patrolling:
                    if(state == CharacterState.idle)
                    {
                        transform.position -= (transform.right * Time.deltaTime * moveSpeed) * transform.localScale.x;
                    }
                    break;
            }
        }
    }
    protected void OnDisable()
    {
        if(enemyState == EnemyState.patrolling)
        {
            foreach(Transform destination in destinations)
            {
                if(destination != null)
                {
                    Destroy(destination.gameObject);
                }
            }
            destinations = new List<Transform>();
        }
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        timer = 0;
    }
    public virtual void GetPlayer()
    {
        Transform path = GetPath();
        if (!sequence.IsActive())
        {
            moveDuration = 0;
        }
        if (path != null)
        {
            MoveToTarget(path);
        }
    }
    private void MoveToTarget(Transform path)
    {
        PlayerController player = GameManager.Instance.Player;
        if (path == player.transform)
        {
            StartCoroutine(MoveToDestination(player.transform));
            StartCoroutine(Kill(moveDuration));
        }
        else
        {
            if (enemyState == EnemyState.aggressive)
            {
                StartCoroutine(MoveToDestination(path));
                StartCoroutine(SwitchingState(CharacterState.idle, moveDuration));
            }
        }
    }
    public void Patroling()
    {
        if (enemyState == EnemyState.patrolling)
        {
            OnStartPatrol();
        }
    }
    private void OnStartPatrol()
    {
        destinations = new List<Transform>();
        if(enemyState == EnemyState.patrolling)
        {
            float patrol = 5;
            for(int i = 0; i < 2; i++)
            {
                GameObject newObject = new GameObject("patrol");
                patrol *= -1;
                newObject.transform.position = (Vector2)gameObject.transform.position + new Vector2(startPatrol.x - patrol, startPatrol.y);
                destinations.Add(newObject.transform);
            }
        }
    }
    private Transform GetPath()
    {
        bool pathIsBlocked;
        Transform player = CheckPathToPlayer();
        pathIsBlocked = player == null;
        if(!pathIsBlocked)
        {
            return player;
        }
        switch(enemyState)
        {
            case EnemyState.patrolling:
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
    private Transform CheckPathToPlayer()
    {
        PlayerController player = GameManager.Instance.Player;
        if (player != null)
        {
            bool pathIsBlocked = CheckPathIsBlocked(transform.position, player.transform.position);
            if (!pathIsBlocked)
            {
                sound.source.Stop();
                sound.PlayOnce(attackSound);
                return player.transform;
            }
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
        Debug.Log(index);
        if (index != -1)
        {
            destinations.RemoveAt(index);
            StartMoveToDestination(destination);
            Rotate(destination.position);
            //if (index > 0)
            //{
            //    index = GameManager.Instance.GetCurrentPosIndex(transform, destinations);
            //    destinations[index] = passedDestinations[0];
            //    passedDestinations.Remove(destinations[index]);
            //}
        }
    }
    private void StartMoveToDestination(Transform destination)
    {
        Move(destination.position, Ease.Linear);
        StartCoroutine(OnMovedToDestination(moveDuration));
    }
    private IEnumerator OnMovedToDestination(float duration)
    {
        yield return new WaitForSeconds(duration);
        foreach(var destination in destinations)
        {
            if(!CheckPathIsBlocked(transform.position, destination.position) && destination != transform)
            {
                MoveToTarget(destination);
                break;
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
        if(enemyState == EnemyState.patrolling)
        {
            if(!Application.isPlaying)
            {
                startPatrol = transform.position;
            }
            Gizmos.DrawWireSphere(new Vector2(startPatrol.x + 5, transform.position.y), 1);
            Gizmos.DrawWireSphere(new Vector2(startPatrol.x - 5, transform.position.y), 1);
        }
    }
#endif
    protected IEnumerator Kill(float duration)
    {
        yield return new WaitForSeconds(duration);
        Acting(animationSet[1], false);
        StartCoroutine(ShowGameOverUI());
    }
    private IEnumerator ShowGameOverUI()
    {
        yield return new WaitForSeconds(1);
        UIController.Instance.ShowGameOverUI(true);
    }
    protected override IEnumerator OnBeenKilled(float duration)
    {
        yield return new WaitForSeconds(duration);
        CharacterPoolParty.Instance.Party.GetPool(poolName).GetBackToPool(gameObject);
    }
    public enum EnemyState { standing, aggressive, patrolling }
    protected override void Action()
    {
        if (skeletonAnimation != null)
        {
            switch (state)
            {
                case CharacterState.idle:
                    Acting(animationSet[0], true);
                    //audioSource.clip = normalSound;
                    //audioSource.Play();
                    //audioSource.loop = true;
                    break;
                case CharacterState.move:
                    Acting(animationSet[0], true);
                    sound.source.Stop();
                    sound.PlayOnce(normalSound);
                    break;
                case CharacterState.die:
                    if(deathCount <= 0)
                    {
                        Acting(animationSet[2], false);
                        sound.PlayOnce(deathSound);
                        deathCount++;
                    }
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
