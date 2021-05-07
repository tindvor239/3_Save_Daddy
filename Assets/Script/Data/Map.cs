using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.CustomComponents;
using UnityEditor;

[CreateAssetMenu(fileName = "New Map", menuName = "Data/Map", order = 1)]
public class Map : Data
{
    [SerializeField]
    public List<Package> packages;
    public void Save()
    {
        packages = PackAllModels();
        #if UNITY_EDITOR
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        #endif
        Debug.Log("Saved");
    }
    public void Load(List<PoolParty> poolParties)
    {
        UnpackAllModels(packages, poolParties);
        Debug.Log("Loaded");
    }
}
