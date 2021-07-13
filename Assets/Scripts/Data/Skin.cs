using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skin", menuName = "Data/Skin", order = 2)]
public class Skin : ScriptableObject
{
    public bool isSold;
    public bool isEquiped;
    public Spine.Unity.SkeletonDataAsset skeletonDataAsset;
    #if UNITY_EDITOR
    [Spine.Unity.SpineSkin(dataField: "skeletonDataAsset", defaultAsEmptyString: true)]
    #endif
    public string initialSkinName;

    #if UNITY_EDITOR
    private void OnValidate()
    {
        if (name != initialSkinName)
        {
            string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
            name = initialSkinName;
            AssetDatabase.RenameAsset(assetPath, initialSkinName);
            AssetDatabase.SaveAssets();
        }
    }
    #endif
}