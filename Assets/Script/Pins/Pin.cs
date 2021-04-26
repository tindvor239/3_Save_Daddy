using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pin : Item
{
    [SerializeField]
    private Trap trap;
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
    #endregion
    protected delegate void OnUnpinTriggered();
    protected event OnUnpinTriggered onUnpinTrigger;

    protected override void Start()
    {
        player = GameController.PlayerController;
    }
    protected virtual void Update()
    {
        if(isLoopingPin && onUnpinTrigger != null)
        {
            triggerDuration += Time.deltaTime;
            onUnpinTrigger.Invoke();
        }
    }

    public virtual void Unpin()
    {
        if(trap != null)
        {
            trap.Disarmed();
        }
        isAlreadyUnpin = true;
        onUnpinTrigger += OnTrigger;
        StartCoroutine(StartMoveObject(0.8f));
    }

    protected virtual void OnTrigger()
    {
        if (triggerDuration >= unpinDelay)
        {
            isAlreadyUnpin = false;
            triggerDuration = 0;
            onUnpinTrigger = null;
        }
    }
    protected IEnumerator StartMoveObject(float duration)
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
}
