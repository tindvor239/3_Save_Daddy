using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class ReleasePin : Pin
{
    [SerializeField]
    private Transform anchor;
    [SerializeField]
    private Transform forward;
    [SerializeField]
    private Transform[] lockers = new Transform[2];
    protected override void Start()
    {
        base.Start();
    }
    protected override void Unpin()
    {
        if (isAlreadyUnpin == false)
        {
            base.Unpin();
            collider.enabled = false;
            if (GameManager.Instance.IsBlocked(lockers[0].position, lockers[1].position, 1 << LayerMask.NameToLayer("Pin")) == false)
            {
                Vector2 pullDirection = (anchor.position - forward.position).normalized;
                GameController.Instance.Move(transform, (Vector2)forward.position + pullDirection * 10 * mainTransform.localScale.x, unpinDuration);
            }
            collider.enabled = true;
        }
    }
}
