using UnityEngine;
using DG.Tweening;
using UnityEngine.CustomComponents;

public class HolderPin : Pin
{
    [SerializeField]
    private Transform[] lockers;
    [SerializeField]
    private Transform[] destinations = new Transform[2];
    protected override void Start()
    {
        base.Start();
    }
    protected override void Unpin()
    {
        bool isBlocked = GameManager.Instance.IsBlocked(lockers[0].position, lockers[1].position, 1 << LayerMask.NameToLayer("Pin"));
        Debug.Log(isBlocked);
        if (isAlreadyUnpin == false && isBlocked == false)
        {
            Vector2 destination = FlipFlop();
            finishDuration = unpinDuration + 0.1f;
            base.Unpin();
            collider.enabled = false;
            GameController.Instance.Move(pinTransform, destination, unpinDuration);
            collider.enabled = true;
        }
    }

    private void OnEnable()
    {
        pinTransform.localPosition = destinations[1].localPosition;
    }
    private Vector2 FlipFlop()
    {
        for (int i = 0; i < destinations.Length; i++)
        {
            if (Mathf.Round(destinations[i].position.x) == Mathf.Round(pinTransform.position.x) &&
                Mathf.Round(destinations[i].position.y) == Mathf.Round(pinTransform.position.y))
            {
                if (i == destinations.Length - 1)
                {
                    return destinations[i - 1].position;
                }
                else
                {
                    return destinations[i + 1].position;
                }
            }
        }
        return new Vector2();
    }
}
