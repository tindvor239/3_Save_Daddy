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
    [SerializeField]
    private AudioClip afraid;
    [SerializeField]
    private AudioClip[] laughs;

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
        if (skeletonAnimation != null && gameObject.activeInHierarchy && GameManager.State == GameManager.GameState.play)
        {
            switch (state)
            {
                case CharacterState.idle:
                    Acting(animationSet[0], true);
                    break;
                case CharacterState.move:
                    SwitchAction(animationSet[1], animationSet[2]);
                    int randomIndex = Random.Range(0, laughs.Length);
                    sound.PlayOnce(laughs[randomIndex]);
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
        if(gameObject.activeInHierarchy)
        {
            actingDelay = 2;
            StartCoroutine(SwitchingAct(endAnimation, false, 1));
            ViewManager.Acting(skeletonAnimation, startAnimation, false, 1);
        }
    }

    public void MovePlayerToNextDestination()
    {
        if(state != CharacterState.die)
        {
            int index = GameManager.Instance.GetNextDestinationIndex(this);
            if (index != -1)
            {
                alreadyMoveCamera = false;
                Vector3 pathPosition = GameManager.Instance.Destinations[index].position;
                if (!CheckPathIsBlocked(transform.position, pathPosition))
                {
                    CameraController.Instance.Move();
                    bool isBlocked = GameManager.Instance.IsBlocked(transform.position, pathPosition, 1 << LayerMask.NameToLayer("Enemy"));
                    if (!isBlocked)
                    {
                        ContinueMoving(GameManager.Instance.Destinations[index]);
                    }
                }
            }
            else if(alreadyMoveCamera == false)
            {
                CameraController.Instance.ZoomCenterPoint();
                TutorialHandler.Instance.ShowTutorial(true, GameManager.Instance.CurrentLevelIndex() - 1);
                Invoke("ScareAnimation", actingDelay);
                alreadyMoveCamera = true;
            }
            else
            {
                CameraController.Instance.ZoomCenterPoint();
            }
        }
    }
    public override void Interact()
    {
        state = CharacterState.die;
        Action();
        StartCoroutine(OnBeenKilled(1.3f));
    }
    public override void Continue()
    {
        base.Continue();
        MovePlayerToNextDestination();
    }

    private void ContinueMoving(Transform destination)
    {
        if(GameManager.State == GameManager.GameState.play)
        {
            if (!sequence.IsActive())
            {
                moveDuration = 0;
            }
            StartCoroutine(MoveToDestination(destination));
        }
    }
    private IEnumerator MoveToDestination(Transform destination)
    {
        yield return new WaitForSeconds(moveDuration);
        StartMoveToDestination(destination);
    }
    private void StartMoveToDestination(Transform destination)
    {
        Move(destination.position, Ease.Linear);
        StartCoroutine(OnMovedToDestination(moveDuration));
    }
    private void ScareAnimation()
    {
        if(IsDanger() && gameObject.activeInHierarchy)
        {
            SwitchAction(animationSet[3], animationSet[4]);
            Invoke("ScareSound", 0.4f);
        }
    }
    private void ScareSound()
    {
        sound.PlayOnce(afraid);
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
    private IEnumerator OnMovedToDestination(float duration)
    {
        yield return new WaitForSeconds(duration);
        if(!GameManager.Instance.isWin())
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
