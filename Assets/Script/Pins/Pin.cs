using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.CustomComponents;

public abstract class Pin : Model, IInteractable
{
    [SerializeField]
    private List<GameObject> pieces;
    [SerializeField]
    protected Transform pinTransform;
    [SerializeField]
    protected Transform mainTransform;
    [SerializeField]
    private bool isLoopingPin = false;

    [SerializeField]
    protected float unpinDuration = 0.8f;
    [SerializeField]
    protected new Collider2D collider;
    protected bool isAlreadyUnpin = false;
    protected float finishDuration = 0.8f;
    protected float unpinDelay = 1f;
    protected float triggerDuration = 0;
    #region Properties
    public Transform MainTransform { get => mainTransform; }
    public bool IsLoopingPin { protected get => isLoopingPin; set => isLoopingPin = value; }
    public Component Collider { get => collider; }
    #endregion
    protected delegate void OnUnpinTriggered();
    protected event OnUnpinTriggered onUnpinTrigger;

    protected virtual void Start()
    {
        collider = GetComponent<Collider2D>();
    }
    protected virtual void Update()
    {
        if(isLoopingPin && onUnpinTrigger != null)
        {
            triggerDuration += Time.deltaTime;
            onUnpinTrigger.Invoke();
        }
    }

    protected virtual void Unpin()
    {
        isAlreadyUnpin = true;
        onUnpinTrigger += OnTrigger;
        StartCoroutine(StartMoveObject(finishDuration));
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
        if(enemy != null && enemy.gameObject.activeInHierarchy && enemy.GetComponent<ManEaterPlantController>() == null)
        {
            enemy.GetPlayer();
        }
        GameManager.Instance.Player.MovePlayerToNextDestination();
    }
    public void Interact()
    {
        Unpin();
    }
    public new virtual PinPackage Pack()
    {
        List<Piece> pieces = new List<Piece>();
        foreach(GameObject gameObject in this.pieces)
        {
            pieces.Add(Piece.Pack(gameObject));
        }
        PinPackage result = new PinPackage(poolName, mainTransform.name, mainTransform.position, mainTransform.rotation, pinTransform.localScale, pieces);
        return result;
    }
    public override void Unpack(Package package, ObjectPool pool)
    {
        poolName = package.PoolName;
        mainTransform.name = package.Name;
        mainTransform.position = package.Position;
        mainTransform.rotation = package.Rotation;
        if(package is PinPackage)
        {
            PinPackage pinPackage = (PinPackage)package;
            pinTransform.localScale = pinPackage.Scale;
            for(int i = 0; i < pinPackage.Pieces.Count; i++)
            {
                Piece.Unpack(pieces[i], pinPackage.Pieces[i]);
            }
        }
    }
}
