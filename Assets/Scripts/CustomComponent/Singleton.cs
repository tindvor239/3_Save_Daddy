﻿using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; protected set; }

    protected virtual void Awake()
    {
        Instance = GetComponent<T>();
    }
}
