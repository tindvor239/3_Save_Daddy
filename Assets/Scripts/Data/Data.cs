using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.CustomComponents;

public abstract class Data : ScriptableObject
{
    public void PackAllModels(ref List<Package> packages, ref List<PinPackage> pinPackages)
    {
        packages = new List<Package>();
        pinPackages = new List<PinPackage>();
        Model[] models = FindObjectsOfType<Model>();
        foreach (Model model in models)
        {
            bool isCamera = model is CameraController;
            if (!isCamera && model.gameObject.activeInHierarchy)
            {
                bool isPin = model is Pin;
                if (isPin)
                {
                    Pin pin = (Pin)model;
                    pinPackages.Add(pin.Pack());
                }
                else
                {
                    packages.Add(model.Pack());
                }
            }
        }
    }
    public void UnpackAllModelsInstance(List<Package> packages, List<PinPackage> pinPackages, List<PoolParty> poolParties)
    {
        foreach (Package package in packages)
        {
            ObjectPool pool = GetPool(package, poolParties);
            if (pool != null)
            {
                SpawnPooledObject(package, pool);
            }
        }
        foreach (PinPackage pinPackage in pinPackages)
        {
            ObjectPool pool = GetPool(pinPackage, poolParties);
            if (pool != null)
            {
                SpawnPooledObject(pinPackage, pool);
            }
        }
    }
    public IEnumerator UnpackAllModels(List<Package> packages, List<PinPackage> pinPackages, List<PoolParty> poolParties, GameManager gameManager)
    {
        foreach (Package package in packages)
        {
            ObjectPool pool = GetPool(package, poolParties);
            if (pool != null)
            {
                SpawnPooledObject(package, pool);
                MapEditor.Instance.processValue++;
                yield return null;
            }
        }
        foreach(PinPackage pinPackage in pinPackages)
        {
            ObjectPool pool = GetPool(pinPackage, poolParties);
            if(pool != null)
            {
                SpawnPooledObject(pinPackage, pool);
                MapEditor.Instance.processValue++;
                yield return null;
            }
        }
        gameManager.GetDestinations();
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
    private void SpawnPooledObject(Package package, ObjectPool pool)
    {
        if(package is PinPackage)
        {
            PinPackage pinPackage = (PinPackage)package;
            Pin pin = GetPooledObject<Pin>(pool);
            if (pin != null)
            {
                pin.Unpack(pinPackage, pool);
            }
        }
        else
        {
            GetPooledObject<Model>(pool).Unpack(package, pool);
        }
    }
    private T GetPooledObject<T>(ObjectPool pool) where T : MonoBehaviour
    {
        GameObject gameObject = pool.GetPooledObject();
        if(gameObject == null)
        {
            return pool.CreatePooledObject<T>();
        }
        else
        {
            if(gameObject.GetComponent<T>() == null)
            {
                return gameObject.GetComponentInChildren<T>();
            }
            else
            {
                return gameObject.GetComponent<T>();
            }
        }
    }
}
