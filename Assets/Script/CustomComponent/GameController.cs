using UnityEngine;
using DG.Tweening;

public class GameController : Singleton<GameController>
{
    [SerializeField]
    private PlayerController playerController;
    private static PlayerController player;
    #region Properties
    public static PlayerController PlayerController { get => player; }
    #endregion
    protected override void Awake()
    {
        #region Singleton
        base.Awake();
        #endregion
        player = playerController;
    }
    private void Start()
    {
        player.MovePlayerToNextDestination();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject clickedObject = GetObjectByMouseRayCast();
            if (clickedObject != null)
            {
                if (clickedObject.GetComponent<Pin>() != null)
                {
                    clickedObject.GetComponent<Pin>().Unpin();
                }
                else if (clickedObject.transform.parent.GetComponent<Pin>() != null)
                {
                    clickedObject.transform.parent.GetComponent<Pin>().Unpin();
                }
            }
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
        SceneOutput.MoveOutput(sequence, moveObject, destination, duration);
    }
    public static void Move(in Sequence sequence, in Transform moveObject, in Vector2 destination, in float duration, in Ease ease)
    {
        SceneOutput.MoveOutput(sequence, moveObject, destination, duration, ease);
    }
    public static void Move(in Transform moveObject, in Vector2 destination, in float duration)
    {
        SceneOutput.MoveOutput(moveObject, destination, duration);
    }
    public static void Move(in Transform moveObject, in Vector2 destination, in float duration, in Ease ease)
    {
        SceneOutput.MoveOutput(moveObject, destination, duration, ease);
    }
    #endregion
    #region Rotate Handle
    public static void Rotate(in Transform transform, in Quaternion target,in float duration)
    {
        SceneOutput.Rotating(transform, target, duration);
    }
    #endregion
    #region Calculation
    public static Vector3 GetDirectionVector(Vector3 gameObject, Vector3 target)
    {
        return target - gameObject;
    }
    public static void LookRotation(Vector3 axis, GameObject gameObject, Vector3 lookDirection)
    {
        Quaternion lookRotation = Quaternion.LookRotation(axis, lookDirection);
        SceneOutput.Rotating(gameObject.transform, lookRotation);
    }
    public static void LookRotation(Vector3 axis, GameObject gameObject, Vector3 lookDirection, float min, float max)
    {
        Quaternion lookRotation = Quaternion.LookRotation(axis, lookDirection);
        SceneOutput.Rotating(gameObject.transform, lookRotation, min, max);
    }
    public static void MoveToDestination(CharacterController controller, Transform destination)
    {
        int index = GameManager.GetDestinationIndex(destination);
        if (index != -1)
        {
            GameManager.Instance.PassedDestinations.Add(GameManager.Instance.Destinations[index]);
            GameManager.Instance.Destinations[index].gameObject.SetActive(false);
            GameManager.Instance.Destinations[index] = controller.transform;
            CharacterController.Move(controller, destination.position, 3f, Ease.Linear);
            if (index > 0)
            {
                index = GameManager.GetDestinationIndex(controller.transform);
                GameManager.Instance.Destinations[index] = GameManager.Instance.PassedDestinations[0];
                GameManager.Instance.PassedDestinations.Remove(GameManager.Instance.Destinations[index]);
                //GameManager.Instance.Destinations[index].gameObject.SetActive(true);
            }
        }
    }
    #endregion
    #endregion
}
