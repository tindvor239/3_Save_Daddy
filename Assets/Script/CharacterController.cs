using DG.Tweening;
using UnityEngine;

public class CharacterController : MonoBehaviour, IInteractable
{
    protected GameController gameController;
    protected ViewManager sceneOutput;
    protected GameManager gameManager;
    protected new Collider2D collider;
    protected Sequence sequence;
    [SerializeField]
    protected float moveSpeed = 8f;
    #region Properties
    public float MoveSpeed { get => moveSpeed; }
    public Sequence Sequence { get => sequence; }
    #endregion
    protected virtual void Start()
    {
        gameController = GameController.Instance;
        sceneOutput = ViewManager.Instance;
        gameManager = GameManager.Instance;
        sequence = DOTween.Sequence();
        collider = GetComponent<Collider2D>();

    }
    public void Move(in Vector2 destination, in Ease ease)
    {
        float duration = MoveDuration(transform.position, destination);
        GameController.Move(in sequence, transform, destination, duration, ease);
    }
    public void Move(in Vector2 destination)
    {
        float duration = MoveDuration(transform.position, destination);
        GameController.Move(in sequence, transform, destination, duration);
    }
    private float MoveDuration(Vector2 fromPosition, Vector2 toPosition)
    {
        float duration = Vector2.Distance(fromPosition, toPosition) / moveSpeed;
        return duration;
    }
    public virtual void Interact()
    {
        
    }
}
