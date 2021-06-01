using System.Collections.Generic;
using System;
using UnityEngine;

public interface IPoolModel : IModel
{
    string PoolName { get; }
}

[Serializable]
public class Package : IPoolModel
{
    #region Field
    [SerializeField]
    protected string poolName;
    [SerializeField]
    protected string name;
    [SerializeField]
    protected Vector3 position;
    [SerializeField]
    protected Quaternion rotation;
    #endregion
    #region Properties
    public string PoolName { get => poolName; }
    public string Name { get => name; }
    public Vector2 Position { get => position; }
    public Quaternion Rotation { get => rotation; }
    #endregion
    public Package(string poolName, string name, Vector3 position, Quaternion rotation)
    {
        this.poolName = poolName;
        this.name = name;
        this.position = position;
        this.rotation = rotation;
    }
}

[Serializable]
public class PinPackage : Package
{
    #region Field
    [SerializeField]
    private Vector3 scale;
    [SerializeField]
    private List<Piece> pieces = new List<Piece>();
    #endregion
    #region Properies
    public Vector3 Scale { get => scale; }
    public List<Piece> Pieces { get => pieces; }
    #endregion
    public PinPackage(string poolName, string name, Vector3 position, Quaternion rotation, Vector3 scale, List<Piece> pieces) : base(poolName, name, position, rotation)
    {
        this.scale = scale;
        this.pieces = pieces;
    }
}

[Serializable]
public struct Piece
{
    [SerializeField]
    private string name;
    [SerializeField]
    private bool isActive;
    [SerializeField]
    private Vector3 position;
    [SerializeField]
    private Quaternion rotation;
    [SerializeField]
    private Vector3 scale;
    public Piece(string name, bool isActive, Vector3 position)
    {
        this.name = name;
        this.isActive = isActive;
        this.position = position;
        rotation = new Quaternion();
        scale = new Vector3();
    }
    public Piece(string name, bool isActive, Vector3 position, Quaternion rotation)
    {
        this = new Piece(name, isActive, position);
        this.rotation = rotation;
        scale = new Vector3();
    }
    public Piece(string name, bool isActive, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        this = new Piece(name, isActive, position, rotation);
        this.scale = scale;
    }
    public static Piece Pack(GameObject gameObject)
    {
        return new Piece(gameObject.name, gameObject.activeInHierarchy, gameObject.transform.localPosition, gameObject.transform.localRotation, gameObject.transform.localScale);
    }
    public static void Unpack(GameObject gameObject, Piece piece)
    {
        gameObject.name = piece.name;
        gameObject.SetActive(piece.isActive);
        gameObject.transform.localPosition = piece.position;
        gameObject.transform.localRotation = piece.rotation;
        gameObject.transform.localScale = piece.scale;
    }
}