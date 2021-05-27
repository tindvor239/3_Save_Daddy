﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.CustomComponents;
using Spine.Unity;
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
    protected override void OnEnable()
    {
        base.OnEnable();
    }
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
                    SwitchAction(animationSet[1], animationSet[2]);
                    StartCoroutine(SwitchingState(CharacterState.idle, 1));
                    break;
                case CharacterState.die:
                    SwitchAction(animationSet[3], animationSet[4]);
                    break;
            }
        }
    }
    protected override IEnumerator OnBeenKilled(float duration)
    {
        yield return new WaitForSeconds(duration);
        UIController.Instance.ShowGameOverUI(true);
    }
    protected override void SwitchAction(AnimationReferenceAsset startAnimation, AnimationReferenceAsset endAnimation)
    {
        actingDelay = 2;
        StartCoroutine(SwitchingAct(endAnimation, false, 1));
        ViewManager.Acting(skeleton, startAnimation, false, 1);
    }

    public void MovePlayerToNextDestination()
    {
        if(state != CharacterState.die)
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
    }
    public override void Interact()
    {
        state = CharacterState.die;
        Action();
        StartCoroutine(OnBeenKilled(1.3f));
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
        if(IsDanger())
        {
            SwitchAction(animationSet[3], animationSet[4]);
            Invoke("ScareAnimation", actingDelay);
        }
    }
    private bool IsDanger()
    {
        foreach(ObjectPool pool in ObstaclePoolParty.Instance.Party.Pools)
        {
            foreach(GameObject pooledObject in pool.PooledObjects)
            {
                float currentDistance = Vector3.Distance(transform.position, pooledObject.transform.position);
                if(pooledObject.activeInHierarchy && currentDistance <= dangerRange)
                {
                    return true;
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
                        return true;
                    }
                }
            }
        }
        return false;
    }
    private IEnumerator CheckMoveCondition(float duration)
    {
        yield return new WaitForSeconds(duration);
        if(!GameManager.isWin())
        {
            MovePlayerToNextDestination();
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, dangerRange);
    }
}
