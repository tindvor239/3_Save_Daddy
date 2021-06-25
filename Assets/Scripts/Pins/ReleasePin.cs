using UnityEngine;
using System.Collections;

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
            finishDuration = unpinDuration + 0.1f;
            base.Unpin();
            collider.enabled = false;
            if (GameManager.Instance.IsBlocked(lockers[0].position, lockers[1].position, 1 << LayerMask.NameToLayer("Pin")) == false)
            {
                GameController.Instance.Move(pinTransform, forward.transform.position, unpinDuration);
                StartCoroutine(Disapear(1f));
            }
            collider.enabled = true;
        }
    }

    private void OnEnable()
    {
        StartCoroutine(SetVisible());
    }

    private IEnumerator Disapear(float duration)
    {
        yield return new WaitForSeconds(duration);
        VisiblePin(false);
    }
    private IEnumerator SetVisible()
    {
        if(MapEditor.Instance != null)
        {
            while(MapEditor.Instance.Process < 100)
            {
                yield return null;
            }
            VisiblePin(true);
            pinTransform.localPosition = new Vector3();
            isAlreadyUnpin = false;
        }
    }
    private void VisiblePin(bool isVisible)
    {
        Transform parent = gameObject.transform.parent;
        SpriteRenderer[] sprites = parent.GetComponentsInChildren<SpriteRenderer>();
        Collider2D[] colliders = parent.GetComponentsInChildren<Collider2D>();
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.enabled = isVisible;
        }
        foreach(Collider2D collider in colliders)
        {
            collider.enabled = isVisible;
        }
    }
}
