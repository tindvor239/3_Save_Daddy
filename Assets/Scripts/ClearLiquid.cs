using UnityEngine;

public class ClearLiquid : MonoBehaviour
{
    public virtual void StartClear(LiquidPool liquidPool, Vector3 position)
    {
        bool canCreate = true;
        foreach (GameObject gameObject in liquidPool.Smokes.PooledObjects)
        {
            if (gameObject.activeInHierarchy == true)
            {
                canCreate = false;
                break;
            }
        }
        if(canCreate)
        {
            liquidPool.CreateSmoke(position);
        }
    }
}
