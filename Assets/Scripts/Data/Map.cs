using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.CustomComponents;
using UnityEditor;

[CreateAssetMenu(fileName = "New Map", menuName = "Data/Map", order = 1)]
public class Map : Data
{
    public List<Package> packages;
    public List<PinPackage> pinPackages;
    public List<CameraPathPackage> cameraPathPackages;
    [SerializeField]
    public bool isUnlocked;
    #if UNITY_EDITOR
    public void Save()
    {
        PackAllModels( ref packages, ref pinPackages, ref cameraPathPackages);
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        Debug.Log("Saved");
    }
    #endif
    public void Load(List<PoolParty> poolParties, GameManager gameManager)
    {
        if(Application.isPlaying)
        {
            if(isUnlocked)
            {
                MapEditor.Instance.StartCoroutine(UnpackAllModels(packages, pinPackages, cameraPathPackages, poolParties, gameManager));
            }
        }
        else
        {
            UnpackAllModelsInstance(packages, pinPackages, cameraPathPackages, poolParties);
        }
    }
}
