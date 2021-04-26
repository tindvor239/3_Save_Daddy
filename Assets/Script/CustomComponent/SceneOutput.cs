using UnityEngine;
using DG.Tweening;

public class SceneOutput : Singleton<SceneOutput>
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
    public static void MoveOutput(in Sequence sequence, in Transform moveObject, in  Vector2 destination,in float duration)
    {
        sequence.Append(moveObject.DOMoveX(destination.x, duration));
        sequence.Join(moveObject.DOMoveY(destination.y, duration));
    }
    public static void MoveOutput(in Sequence sequence, in Transform moveObject, in Vector2 destination, in float duration, in Ease ease)
    {
        sequence.Append(moveObject.DOMoveX(destination.x, duration).SetEase(ease));
        sequence.Join(moveObject.DOMoveY(destination.y, duration).SetEase(ease));
    }
    public static void MoveOutput(in Transform moveObject, in Vector2 destination, in float duration)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(moveObject.DOMoveX(destination.x, duration));
        sequence.Join(moveObject.DOMoveY(destination.y, duration));
    }
    public static void MoveOutput(in Transform moveObject, in Vector2 destination, in float duration, in Ease ease)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(moveObject.DOMoveX(destination.x, duration).SetEase(ease));
        sequence.Join(moveObject.DOMoveY(destination.y, duration).SetEase(ease));
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
