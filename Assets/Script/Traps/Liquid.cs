using UnityEngine;

public class Liquid : MonoBehaviour
{
    public LiquidPool liquidPool;
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        ClearLiquid clearLiquid = collision.gameObject.GetComponent<ClearLiquid>();
        if (clearLiquid != null)
        {
            clearLiquid.StartClear(liquidPool, transform.position);
            liquidPool.GetAllLiquidToPool();
        }
    }

}
