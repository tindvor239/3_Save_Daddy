using UnityEngine;
using DG.Tweening;

public class GameController : Singleton<GameController>
{
    private PlayerController player;
    private static bool canTrigger = false;
    private static float unpinDelay = 1f;
    private static float unpinDelayTimer = 1f;
    protected override void Awake()
    {
        #region Singleton
        base.Awake();
        #endregion
    }
    private void Start()
    {
        //player.MovePlayerToNextDestination();
        player = GameManager.Instance.Player;
    }
    private void Update()
    {
        switch(GameManager.State)
        {
            case GameManager.GameState.play:
                UnpinDelayCountDown();
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject clickedObject = GetObjectByMouseRayCast();
                    if (clickedObject != null)
                    {
                        if (clickedObject.GetComponent<Pin>() != null && canTrigger)
                        {
                            Debug.Log(clickedObject.GetComponent<Pin>());
                            clickedObject.GetComponent<Pin>().Interact();
                            unpinDelayTimer = unpinDelay;
                        }
                        else if (clickedObject.transform.parent.GetComponent<Pin>() != null && canTrigger)
                        {
                            clickedObject.transform.parent.GetComponent<Pin>().Interact();
                            unpinDelayTimer = unpinDelay;
                        }
                    }
                }
                break;
        }
    }
    private void UnpinDelayCountDown()
    {
        if(unpinDelayTimer > 0)
        {
            unpinDelayTimer -= Time.deltaTime;
            canTrigger = false;
        }
        else
        {
            unpinDelayTimer = 0;
            canTrigger = true;
        }
        
    }
    #region Behaviours
    #region Inputs
    public static GameObject GetObjectByMouseRayCast()
    {
        return GameManager.RayCastObject(GetMousePosition(), Vector3.forward);
    }
    public static Vector3 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    public static Vector3 MousePositionOnScreen()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    #endregion
    #region Movement Handle
    public static void Move(in Sequence sequence, in Transform moveObject, in Vector2 destination, in float duration)
    {
        ViewManager.MoveOutput(sequence, moveObject, destination, duration);
    }
    public static void Move(in Sequence sequence, in Transform moveObject, in Vector2 destination, in float duration, in Ease ease)
    {
        ViewManager.MoveOutput(sequence, moveObject, destination, duration, ease);
    }
    public static void Move(in Transform moveObject, in Vector2 destination, in float duration)
    {
        ViewManager.MoveOutput(moveObject, destination, duration);
    }
    public static void Move(in Transform moveObject, in Vector2 destination, in float duration, in Ease ease)
    {
        ViewManager.MoveOutput(moveObject, destination, duration, ease);
    }
    #endregion
    #region Rotate Handle
    public static void Rotate(in Transform transform, in Quaternion target,in float duration)
    {
        ViewManager.Rotating(transform, target, duration);
    }
    public static void Rotate(in Transform transform, in Vector3 target, in float duration, Ease ease)
    {
        ViewManager.Rotating(transform, target, duration, ease);
    }
    public static void Rotate(Sequence sequence, in Transform transform, in Vector3 target, in float duration, Ease ease)
    {
        ViewManager.Rotating(sequence, transform, target, duration, ease);
    }
    public static void Rotate(in Transform transform, in Vector3 target)
    {
        ViewManager.Rotating(transform, target);
    }
    #endregion
    #region Calculation

    public static void LookRotation(Vector3 axis, GameObject gameObject, Vector3 lookDirection)
    {
        Quaternion lookRotation = Quaternion.LookRotation(axis, lookDirection);
        ViewManager.Rotating(gameObject.transform, lookRotation);
    }
    public static void LookRotation(Vector3 axis, GameObject gameObject, Vector3 lookDirection, float min, float max)
    {
        Quaternion lookRotation = Quaternion.LookRotation(axis, lookDirection);
        ViewManager.Rotating(gameObject.transform, lookRotation, min, max);
    }
    #endregion
    #endregion
}
