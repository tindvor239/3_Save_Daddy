using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    Component Collider { get; }
    void Interact();
}
