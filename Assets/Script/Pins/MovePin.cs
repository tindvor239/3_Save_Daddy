using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class MovePin : Pin
{
    [SerializeField]
    private Transform[] points = new Transform[2];
    [SerializeField]
    private Transform middle;
    private static float maxTime = 0.52f;
    private float timer = maxTime;
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
        if(timer <= 0)
        {
            Vector2 destination = new Vector2();
            for (int i = 0; i < points.Length; i++)
            {
                if (points[i].position == transform.position)
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
            GameController.Move(transform, destination, unpinDuration);
            base.Unpin();
            timer = maxTime;
        }
    }
}
