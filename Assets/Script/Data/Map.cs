using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.CustomComponents;

[CreateAssetMenu(fileName = "New Map", menuName = "Data/Map", order = 1)]
public class Map : Data
{
    [SerializeField]
    public List<Package> packages;
    public void Save()
    {
        packages = PackAllModels();
        Debug.Log("Saved");
    }
    public void Load(List<PoolParty> poolParties)
    {
        UnPackAllModels(packages, poolParties);
        Debug.Log("Loaded");
    }
}
