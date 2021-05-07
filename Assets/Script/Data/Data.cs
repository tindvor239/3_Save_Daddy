﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.CustomComponents;

public abstract class Data : ScriptableObject
{
    public List<Package> PackAllModels()
    {
        List<Package> result = new List<Package>();
        Model[] models = FindObjectsOfType<Model>();
        foreach (Model model in models)
        {
            bool isCamera = model is CameraController;
            if (!isCamera)
            {
                bool isPin = model is Pin;
                if (isPin)
                {
                    Debug.Log("In");
                    Pin pin = (Pin)model;
                    result.Add(pin.Pack());
                }
                else
                {
                    result.Add(model.Pack());
                }
            }
        }
        return result;
    }
    public void UnpackAllModels(List<Package> packages, List<PoolParty> poolParties)
    {
        foreach (Package package in packages)
        {
            ObjectPool pool = GetPool(package, poolParties);
            if (pool != null)
            {
                SpawnPooledObject(package, pool);
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