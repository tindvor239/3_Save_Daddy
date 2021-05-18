using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class MovePin : Pin
{
    [SerializeField]
    private Transform[] points = new Transform[2];
    [SerializeField]
    private Transform middle;
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
        Vector2 destination = new Vector2();
        for (int i = 0; i < points.Length; i++)
        {
            if (Mathf.RoundToInt(points[i].position.y) == Mathf.RoundToInt(transform.position.y))
            {
                if (i == points.Length - 1)
                {
                    destination = points[i - 1].position;
                }
                else
                {
                    destination = points[i + 1].position;
                }
            }
        }
        finishDuration = MoveDuration(transform.position, destination);
        GameController.Instance.Move(transform, destination, finishDuration - 0.1f);
        base.Unpin();
    }
    private float MoveDuration(Vector2 fromPosition, Vector2 toPosition)
    {
        return Vector2.Distance(fromPosition, toPosition) / moveSpeed;
    }
}
