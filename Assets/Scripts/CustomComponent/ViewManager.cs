using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DoozyUI;
using Spine.Unity;
public class ViewManager : Singleton<ViewManager>
{
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
    public void SetSmoothPosition(in Transform moveObject, in Vector3 destination, ref Vector3 velocity, float smoothTime)
    {
        moveObject.position = Vector3.SmoothDamp(moveObject.position, destination, ref velocity, smoothTime);
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
    #region Scale
    public static void Scale(Transform transform, Vector3 targetScale, float duration)
    {
        transform.DOScale(targetScale, duration);
    }
    public static void Scale(Transform transform, Vector3 targetScale, float duration, Sequence sequence)
    {
        sequence.Append(transform.DOScale(targetScale, duration));
    }    
    #endregion
    #region Sprite Handler
    public static void Fading(SpriteRenderer sprite, float targetAlpha, float duration)
    {
        sprite.DOFade(targetAlpha, duration);
    }
    #endregion
    #region Animation Handler
    public static void Acting(SkeletonAnimation skeleton, AnimationReferenceAsset animation, bool isLoop, float timeScale)
    {
        skeleton.state.SetAnimation(0, animation, isLoop).TimeScale = timeScale;
    }
    #endregion
    #region Appearence Handler
    public static void SetSkin(Spine.Skeleton skeleton, Skin skin)
    {
        skeleton.SetSkin(skin.initialSkinName);
    }
    #endregion
    #region Math
    public float SmoothFloat(float from, float to, float factor)
    {
        return Mathf.Lerp(from, to, Time.deltaTime);
    }
    #endregion
    #region UI Handler
    public static void JoinAction(Sequence sequence, Tween tween)
    {
        sequence.Join(tween);
    }
    public static Tween DoColor(MaskableGraphic graphic, Color color, float duration)
    {
        return graphic.DOColor(color, duration);
    }
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
