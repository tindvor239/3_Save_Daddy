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
            liquidPool.Sound.PlayOnce(clearLiquid.Sound);
        }
        else
        {
            WaterSound(collision);
        }
    }
    protected virtual void WaterSound(Collision2D collision)
    {
        bool isAnimal = gameObject.GetComponent<CharacterController>();
        if (liquidPool != null && isAnimal)
        {
            liquidPool.Sound.PlayOnce(liquidPool.Sound.clip);
        }
    }
}
