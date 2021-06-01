using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.CustomComponents;

public class Model : MonoBehaviour
{
    [SerializeField]
    protected string poolName;
    #region Properties
    public string PoolName { get => poolName;}
    #endregion

    public virtual Package Pack()
    {
        Package result = new Package(poolName, gameObject.name, transform.position, transform.rotation);
        return result;
    }
    public virtual void Unpack(Package package, ObjectPool pool)
    {
        transform.position = package.Position;
        transform.rotation = package.Rotation;
        name = package.Name;
    }
}
