using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour, IInteractable
{
    [SerializeField]
    protected Sprite icon;

    protected virtual void Start()
    {

    }
    public virtual void Interact()
    {
        
    }
}
