using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DoozyUI;

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
    public void MoveOutput(Sequence sequence, Transform moveObject, Vector2 destination, float duration)
    {
        sequence.Append(moveObject.DOMove(destination, duration));
    }
    public void MoveOutput(Sequence sequence, Transform moveObject, Vector2 destination, float duration, Ease ease)
    {
        sequence.Append(moveObject.DOMove(destination, duration).SetEase(ease));
    }
    public void MoveOutput(in Transform moveObject, in Vector2 destination, in float duration)
    {
        moveObject.DOMove(destination, duration);
    }
    public void MoveOutput(in Transform moveObject, in Vector2 destination, in float duration, in Ease ease)
    {
        moveObject.DOMove(destination, duration).SetEase(ease);
    }
    public void SetPosition(in Transform moveObject, in Vector3 destination)
    {
        moveObject.position = destination;
    }
    #endregion
    #region Rotate
    public void Rotating(in Transform transform, Quaternion target)
    {
        transform.rotation = target;
    }
    public void Rotating(in Transform transform, Vector3 target)
    {
        transform.eulerAngles = target;
    }
    public void Rotating(in Transform transform, Vector3 target, float duration)
    {
        transform.DORotate(target, duration);
    }
    public void Rotating(in Transform transform, Vector3 target, float duration, Ease ease)
    {
        transform.DORotate(target, duration, RotateMode.FastBeyond360).SetEase(ease);
    }
    public void Rotating(in Transform transform, Quaternion target, float min, float max)
    {
        transform.rotation = target;
        transform.eulerAngles = new Vector3(transform.transform.eulerAngles.x, transform.transform.eulerAngles.y, Mathf.Clamp(transform.transform.eulerAngles.z, min, max));
    }
    public void Rotating(in Transform transform, Quaternion target, float duration)
    {
        transform.DORotateQuaternion(target, duration);
    }
    public void Rotating(in Sequence sequence, in Transform transform, Vector3 target, float duration)
    {
        sequence.Append(transform.DORotate(target, duration));
    }
    public void Rotating(in Sequence sequence, in Transform transform, Vector3 target, float duration, Ease ease)
    {
        sequence.Append(transform.DORotate(target, duration)).SetEase(ease);
    }
    #endregion
    #region UI Handler
    public static void ShowUI(string UIname , bool isActive)
    {
        if(isActive)
        {
            UIElement.ShowUIElement(UIname);
        }
        else
        {
            UIElement.HideUIElement(UIname);
        }
    }
    public void ChangeSprite(Image image, Sprite changeToSprite)
    {
        image.sprite = changeToSprite;
    }
    #endregion
}
