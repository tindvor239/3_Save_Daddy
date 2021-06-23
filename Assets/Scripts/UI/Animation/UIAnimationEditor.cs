using Coffee.UIEffects;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(UIAnimation))]
public class UIAnimationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        UIAnimation animation = (UIAnimation)target;
        animation.type = (AnimationType)EditorGUILayout.EnumPopup("Animation", animation.type);
        animation.playOnStart = EditorGUILayout.Toggle("Play On Start", animation.playOnStart);
        OnTypeChanged(animation);
        if(EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(animation);
            AssetDatabase.SaveAssets();
        }
    }

    private void OnTypeChanged(UIAnimation animation)
    {
        switch(animation.type)
        {
            case AnimationType.focus:
                if (animation.lastFocusAnimation == null)
                {
                    animation.focusAnimation = new UIFocusAnimation();
                }
                else
                {
                    animation.focusAnimation = animation.lastFocusAnimation;
                }
                InitializeFocusAnimation(animation.focusAnimation);
                break;
            case AnimationType.shiny:
                if(animation.focusAnimation != null)
                {
                    animation.lastFocusAnimation = animation.focusAnimation;
                }
                
                if(animation.GetComponent<UIShiny>())
                {
                    animation.shinyAnimation = animation.GetComponent<UIShiny>();
                }
                else
                {
                    animation.shinyAnimation = animation.gameObject.AddComponent<UIShiny>();
                    DestroyImmediate(animation);
                }
                break;
        }
    }
    private void InitializeFocusAnimation(UIFocusAnimation focusAnimation)
    {
        focusAnimation.toSize = EditorGUILayout.Vector3Field("To Size", focusAnimation.toSize);
        focusAnimation.duration = EditorGUILayout.FloatField("Duration", focusAnimation.duration);
        focusAnimation.delayTime = EditorGUILayout.FloatField("Delay Time", focusAnimation.delayTime);
        focusAnimation.scalePerAction = EditorGUILayout.IntField("Looping Time Per Delay", focusAnimation.scalePerAction);
        focusAnimation.animationLoop = EditorGUILayout.Toggle("Animation Loop", focusAnimation.animationLoop);
        focusAnimation.particle = EditorGUILayout.Toggle("Use Effect", focusAnimation.particle);
        if(focusAnimation.particle)
        {
            focusAnimation.effect = (ParticleSystem)EditorGUILayout.ObjectField("Effect", focusAnimation.effect, typeof(ParticleSystem), true);
            if (focusAnimation.effect != null)
            {
                focusAnimation.effectLoop = EditorGUILayout.Toggle("Effect Looping", focusAnimation.effectLoop);
            }
        }
    }
}
#endif