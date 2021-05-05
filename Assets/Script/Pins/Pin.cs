using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pin : Model, IInteractable
{
    [SerializeField]
    private Trap trap;
    [SerializeField]
    protected Transform mainTransform;
    [SerializeField]
    private bool isLoopingPin = false;
    private PlayerController player;

    [SerializeField]
    protected float unpinDuration = 0.8f;
    protected bool isAlreadyUnpin = false;
    protected float unpinDelay = 1f;
    protected float triggerDuration = 0;
    #region Properties
    public bool IsLoopingPin { protected get => isLoopingPin; set => isLoopingPin = value; }
    public new Behaviour collider { get; private set; }
    #endregion
    protected delegate void OnUnpinTriggered();
    protected event OnUnpinTriggered onUnpinTrigger;

    protected virtual void Start()
    {
        collider = GetComponent<Collider2D>();
    }
    protected virtual void Update()
    {
        if(player == null)
        {
            player = GameManager.Instance.Player;
        }
        if(isLoopingPin && onUnpinTrigger != null)
        {
            triggerDuration += Time.deltaTime;
            onUnpinTrigger.Invoke();
        }
    }

    protected virtual void Unpin()
    {
        if(trap != null)
        {
            trap.Disarmed();
        }
        isAlreadyUnpin = true;
        onUnpinTrigger += OnTrigger;
        StartCoroutine(StartMoveObject(0.8f));
    }
    private void OnTrigger()
    {
        if (triggerDuration >= unpinDelay)
        {
            isAlreadyUnpin = false;
            triggerDuration = 0;
            onUnpinTrigger = null;
        }
    }
    private IEnumerator StartMoveObject(float duration)
    {
        yield return new WaitForSeconds(duration);
        EnemyController enemy = GameManager.GetClosestEnemy();
        if(enemy != null)
        {
            enemy.GetPlayer();
        }
        GetDestination();
    }
    protected virtual void GetDestination()
    {
        player.MovePlayerToNextDestination();
    }

    public void Interact()
    {
        Unpin();
    }
    public override Package Pack()
    {
        Package result = new Package(poolName, mainTransform.name, mainTransform.position, mainTransform.rotation);
        return result;
    }
}
