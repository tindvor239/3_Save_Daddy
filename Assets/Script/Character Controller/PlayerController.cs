using System;
using System.Collections;
using UnityEngine;
using UnityEngine.CustomComponents;
using DG.Tweening;
public class PlayerController : CharacterController
{
    private bool alreadyMoveCamera = false;
    [SerializeField]
    private float dangerRange;
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
    protected override void Update()
    {
        base.Update();
    }
    public void MovePlayerToNextDestination()
    {
        int index = GameManager.GetNextDestinationIndex(this);
        if (index != -1)
        {
            alreadyMoveCamera = false;
            CameraController.Instance.center = CameraController.Instance.RestartCenter();
            Vector3 pathPosition = GameManager.Instance.Destinations[index].position;
            if (!CheckPathIsBlocked(transform.position, pathPosition))
            {
                bool isBlocked = GameManager.Instance.IsBlocked(transform.position, pathPosition, 1 << LayerMask.NameToLayer("Enemy"));
                if (!isBlocked)
                {
                    ContinueMoving(GameManager.Instance.Destinations[index]);
                }
            }
        }
        else if(alreadyMoveCamera == false)
        {
            MoveCamera();
            Invoke("ScareAnimation", actingDelay);
            alreadyMoveCamera = true;
        }
    }

    private void ContinueMoving(Transform destination)
    {
        if (!sequence.IsActive())
        {
            moveDuration = 0;
        }
        StartCoroutine(MoveToDestination(destination));
    }

    private IEnumerator MoveToDestination(Transform destination)
    {
        yield return new WaitForSeconds(moveDuration);
        StartMoveToDestination(destination);
        StartCoroutine(CheckMoveCondition(moveDuration));
        Debug.Log("Checking");
    }
    private void StartMoveToDestination(Transform destination)
    {
        int index = GameManager.GetDestinationIndex(destination, GameManager.Instance.Destinations);
        if (index != -1)
        {
            GameManager.Instance.PassedDestinations.Add(GameManager.Instance.Destinations[index]);
            GameManager.Instance.Destinations[index] = transform;
            Move(destination.position, Ease.Linear);
            if (index > 0)
            {
                index = GameManager.GetDestinationIndex(transform, GameManager.Instance.Destinations);
                GameManager.Instance.Destinations[index] = GameManager.Instance.PassedDestinations[0];
                GameManager.Instance.PassedDestinations.Remove(GameManager.Instance.Destinations[index]);
            }
        }
    }
    private void MoveCamera()
    {
        foreach(Transform destination in GameManager.Instance.Destinations)
        {
            if(Math.Round(transform.position.x, 2) == Math.Round(destination.position.x, 2) &&
                Math.Round(transform.position.y, 2) == Math.Round(destination.position.y, 2))
            {
                CameraController.Instance.center = CameraController.Instance.GetCenterPoint();
                break;
            }
        }
    }

    private void ScareAnimation()
    {
        state = IsDanger();
        Action(state);
    }
    private CharacterState IsDanger()
    {
        foreach(ObjectPool pool in ObstaclePoolParty.Instance.Party.Pools)
        {
            foreach(GameObject pooledObject in pool.PooledObjects)
            {
                float currentDistance = Vector3.Distance(transform.position, pooledObject.transform.position);
                if(pooledObject.activeInHierarchy && currentDistance <= dangerRange)
                {
                    return CharacterState.die;
                }
            }
        }
        foreach (ObjectPool pool in CharacterPoolParty.Instance.Party.Pools)
        {
            if(pool != CharacterPoolParty.Instance.PlayerPool)
            {
                foreach (GameObject pooledObject in pool.PooledObjects)
                {
                    float currentDistance = Vector3.Distance(transform.position, pooledObject.transform.position);
                    if (pooledObject.activeInHierarchy && currentDistance <= dangerRange)
                    {
                        return CharacterState.die;
                    }
                }
            }
        }
        return state;
    }
    private IEnumerator CheckMoveCondition(float duration)
    {
        yield return new WaitForSeconds(duration);
        if(!GameManager.isWin())
        {
            MovePlayerToNextDestination();
        }

    }
    public override void Interact()
    {
        CharacterPoolParty.Instance.PlayerPool.GetBackToPool(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, dangerRange);
    }
}
