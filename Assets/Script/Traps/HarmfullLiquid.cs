using UnityEngine;
using UnityEngine.CustomComponents;

public class HarmfullLiquid : Liquid
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            foreach(ObjectPool pool in CharacterPoolParty.Instance.Party.Pools)
            {
                if (pool.PooledObjects.Contains(collision.gameObject))
                {
                    pool.GetBackToPool(collision.gameObject);
                }
            }
        }
        else if(collision.gameObject.tag == "Player")
        {
            if(CharacterPoolParty.Instance.PlayerPool.PooledObjects.Contains(collision.gameObject))
            {
                CharacterPoolParty.Instance.PlayerPool.GetBackToPool(collision.gameObject);
            }
        }
        base.OnCollisionEnter2D(collision);
    }
}
