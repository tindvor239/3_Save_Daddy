using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    Component collider { get; }
    void Interact();
}
