using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
    float MoveSpeed { get; }
    void Move(in Vector3 destination);
}
