using UnityEngine;
using DG.Tweening;

public class ViewManager : Singleton<ViewManager>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Movement
    public static void MoveOutput(Sequence sequence, Transform moveObject, Vector2 destination, float duration)
    {
        sequence.Append(moveObject.DOMove(destination, duration));
    }
    public static void MoveOutput(Sequence sequence, Transform moveObject, Vector2 destination, float duration, Ease ease)
    {
        sequence.Append(moveObject.DOMove(destination, duration).SetEase(ease));
    }
    public static void MoveOutput(in Transform moveObject, in Vector2 destination, in float duration)
    {
        moveObject.DOMove(destination, duration);
    }
    public static void MoveOutput(in Transform moveObject, in Vector2 destination, in float duration, in Ease ease)
    {
        moveObject.DOMove(destination, duration).SetEase(ease);
    }
    public static void SetPosition(in Transform moveObject, in Vector3 destination)
    {
        moveObject.position = destination;
    }
    #endregion
    #region Rotate
    public static void Rotating(in Transform transform, Quaternion target)
    {
        transform.rotation = target;
    }
    public static void Rotating(in Transform transform, Vector3 target)
    {
        transform.eulerAngles = target;
    }
    public static void Rotating(in Transform transform, Vector3 target, float duration)
    {
        transform.DORotate(target, duration);
    }
    public static void Rotating(in Transform transform, Vector3 target, float duration, Ease ease)
    {
        transform.DORotate(target, duration).SetEase(ease);
    }
    public static void Rotating(in Sequence sequence, in Transform transform, Vector3 target, float duration)
    {
        sequence.Append(transform.DORotate(target, duration));
    }
    public static void Rotating(in Sequence sequence, in Transform transform, Vector3 target, float duration, Ease ease)
    {
        sequence.Append(transform.DORotate(target, duration)).SetEase(ease);
    }
    public static void Rotating(in Transform transform, Quaternion target, float min, float max)
    {
        transform.rotation = target;
        transform.eulerAngles = new Vector3(transform.transform.eulerAngles.x, transform.transform.eulerAngles.y, Mathf.Clamp(transform.transform.eulerAngles.z, min, max));
    }
    public static void Rotating(in Transform transform, Quaternion target, float duration)
    {
        transform.DORotateQuaternion(target, duration);
    }
    #endregion
}
