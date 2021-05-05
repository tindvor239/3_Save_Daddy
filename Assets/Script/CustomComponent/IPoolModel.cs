using System.Collections;
using System;
using UnityEngine;

public interface IPoolModel : IModel
{
    string PoolName { get; }
}
[Serializable]
public struct Package : IPoolModel
{
    [SerializeField]
    private string poolName;
    [SerializeField]
    private string name;
    [SerializeField]
    private Vector2 position;
    [SerializeField]
    private Quaternion rotation;
    public string PoolName { get => poolName; }
    public string Name { get => name; }
    public Vector2 Position { get => position; }
    public Quaternion Rotation { get => rotation; }

    public Package(string poolName, string name, Vector2 position, Quaternion rotation)
    {
        this.poolName = poolName;
        this.name = name;
        this.position = position;
        this.rotation = rotation;
    }
}