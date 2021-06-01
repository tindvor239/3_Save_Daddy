using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : Model
{
    public virtual void OnBeingHit(GameObject hitObject)
    {

    }

    protected virtual void OnHit(GameObject beingHitObject)
    {

    }
}
