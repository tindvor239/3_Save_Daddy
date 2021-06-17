using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModel
{
    Vector3 Position { get; }
    Quaternion Rotation { get; }
}
