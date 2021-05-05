using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : Model, IMoveable
{
    [Header("Movement")]
    [SerializeField]
    protected float moveSpeed;
    protected GameController gameController;
    protected ViewManager sceneOutput;
    protected GameManager gameManager;
    #region Properties
    public float MoveSpeed { get => moveSpeed; }
    #endregion
    protected virtual void Start()
    {
        gameController = GameController.Instance;
        sceneOutput = ViewManager.Instance;
        gameManager = GameManager.Instance;
    }
    public virtual void Move(in Vector3 destination)
    {
        float duration = MoveDuration(transform.position, destination);
        GameController.Move(transform, destination, in duration);
    }

    protected float MoveDuration(Vector2 fromPosition, Vector2 toPosition)
    {
        return  Vector2.Distance(fromPosition, toPosition) / moveSpeed;
    }
}
