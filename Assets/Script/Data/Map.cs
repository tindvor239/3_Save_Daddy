﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.CustomComponents;
using UnityEditor;

[CreateAssetMenu(fileName = "New Map", menuName = "Data/Map", order = 1)]
public class Map : Data
{
    public List<Package> packages;
    public List<PinPackage> pinPackages;
    [SerializeField]
    public bool isUnlocked;
    public void Save()
    {
        PackAllModels(packages, pinPackages);
        #if UNITY_EDITOR
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        #endif
        Debug.Log("Saved");
    }
    public void Load(List<PoolParty> poolParties)
    {
        if(isUnlocked)
        {
            UnpackAllModels(packages, pinPackages, poolParties);
            Debug.Log("Loaded");
        }
    }
}
