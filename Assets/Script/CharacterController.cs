using DG.Tweening;
using UnityEngine;

public class CharacterController : MonoBehaviour, IInteractable
{
    protected GameController gameController;
    protected SceneOutput sceneOutput;
    protected GameManager gameManager;
    protected new Collider2D collider;

    protected virtual void Start()
    {
        gameController = GameController.Instance;
        sceneOutput = SceneOutput.Instance;
        gameManager = GameManager.Instance;
        collider = GetComponent<Collider2D>();

    }
    public static void Move(CharacterController controller, in Vector2 destination,in float duration,in Ease ease)
    {
        Sequence sequence = DOTween.Sequence();
        GameController.Move(sequence, controller.transform, destination, duration, ease);
    }
    public static void Move(CharacterController controller, in Vector2 destination, in float duration)
    {
        Sequence sequence = DOTween.Sequence();
        GameController.Move(sequence, controller.transform, destination, duration);
    }

    public virtual void Interact()
    {
        
    }
}
