using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class MovePin : Pin
{
    [SerializeField]
    private Transform[] points = new Transform[2];
    [SerializeField]
    private Transform middle;
    private int index = 0;
    private static float moveSpeed = 5f;
    #region Properties
    public Transform[] Points { get => points; }
    #endregion
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        unpinDuration = 0.5f;
    }
    protected override void Unpin()
    {
        index = FlipFlop();
        finishDuration = MoveDuration(pinTransform.position, points[index].position);
        GameController.Instance.Move(pinTransform, points[index].position, finishDuration - 0.1f);
        base.Unpin();
    }
    private float MoveDuration(Vector2 fromPosition, Vector2 toPosition)
    {
        return Vector2.Distance(fromPosition, toPosition) / moveSpeed;
    }
    private int FlipFlop()
    {
        index++;
        if(index >= 2)
        {
            index = 0;
        }
        return index;
    }
}
