using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : Model, IMoveable
{
    [Header("Movement")]
    [SerializeField]
    protected float moveSpeed;
    #region Properties
    public float MoveSpeed { get => moveSpeed; }
    #endregion
    protected virtual void Start()
    {
    }
    public virtual void Move(in Vector3 destination)
    {
        float duration = MoveDuration(transform.position, destination);
        GameController.Instance.Move(transform, destination, in duration);
    }

    public float MoveDuration(Vector2 fromPosition, Vector2 toPosition)
    {
        return  Vector2.Distance(fromPosition, toPosition) / moveSpeed;
    }
}
