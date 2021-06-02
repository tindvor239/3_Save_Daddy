using UnityEngine;
using System;
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
        GameController.Instance.Move(pinTransform, points[index].position, finishDuration / 1.25f, DG.Tweening.Ease.Linear);
        base.Unpin();
    }
    private float MoveDuration(Vector2 fromPosition, Vector2 toPosition)
    {
        return Vector2.Distance(fromPosition, toPosition) / moveSpeed;
    }
    private int FlipFlop()
    {
        index++;
        if(index > 1)
        {
            index = 0;
        }
        return index;
    }
    private int GetIndex()
    {
        for(int i = 0; i < points.Length; i++)
        {
            Vector2 currentPosition = transform.localPosition;
            Vector2 point = points[i].localPosition;
            double roundedCurX = Math.Round(currentPosition.x, 2);
            double roundedCurY = Math.Round(currentPosition.y, 2);
            double roundedPointX = Math.Round(point.x, 2);
            double roundedPointY = Math.Round(point.y, 2);
            if (roundedCurX == roundedPointX && roundedCurY == roundedPointY)
            {
                return index = i;
            }
        }
        return index = 0;
    }
    private void OnEnable()
    {
        Invoke("GetIndex", 0.1f);
    }
}
