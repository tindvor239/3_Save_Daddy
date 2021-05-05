using System.Collections.Generic;
using UnityEngine;
using UnityEngine.CustomComponents;

public abstract class Data : ScriptableObject
{
    public List<Package> PackAllModels()
    {
        List<Package> result = new List<Package>();
        Model[] models = FindObjectsOfType<Model>();
        foreach(Model model in models)
        {
            bool isCamera = model is CameraController;
            if (!isCamera)
            {
                result.Add(model.Pack());
            }
        }
        return result;
    }
    public void UnPackAllModels(List<Package> packages, List<PoolParty> poolParties)
    {
        foreach (Package package in packages)
        {
            ObjectPool pool = null;
            pool = GetPool(package, poolParties);
            if (pool != null)
            {
                Model newModel = pool.CreatePooledObject<Model>();
                if (newModel != null)
                {
                    newModel.Unpack(package, pool);
                }
            }
        }
    }

    private ObjectPool GetPool(Package package, List<PoolParty> poolParties)
    {
        foreach (PoolParty poolParty in poolParties)
        {
            if (GetPool(package, poolParty.Pools) != null)
            {
                return GetPool(package, poolParty.Pools);
            }
        }

        return null;
    }

    private ObjectPool GetPool(Package package, List<ObjectPool> pools)
    {
        ObjectPool result = null;
        foreach (ObjectPool pool in pools)
        {
            if (package.PoolName == pool.Name)
            {
                result = pool;
                return result;
            }
        }
        return result;
    }
}
