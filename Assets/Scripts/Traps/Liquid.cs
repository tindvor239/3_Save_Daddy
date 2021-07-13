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
            liquidPool.AudioSource.PlayOneShot(clearLiquid.Sound);
        }
        else
        {
            WaterSound(collision);
        }
    }
    protected virtual void WaterSound(Collision2D collision)
    {
        bool isAnimal = collision.gameObject.tag != "Untagged" && collision.gameObject.tag != "Liquid";
        if (liquidPool != null && isAnimal)
        {
            liquidPool.PlaySound();
        }
    }
}
