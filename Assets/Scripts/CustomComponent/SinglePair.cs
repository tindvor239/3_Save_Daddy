using System;
using UnityEngine;

[Serializable]
public abstract class SinglePair<TKey, TValue>
{
    [SerializeField]
    private TKey key;
    [SerializeField]
    private TValue value;

    #region Properties
    public TValue Value { get => value; }
    public TKey Key { get => key; }
    #endregion
}