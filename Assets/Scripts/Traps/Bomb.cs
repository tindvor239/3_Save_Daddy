using UnityEngine;
using UnityEngine.CustomComponents;

public class Bomb : Trap
{
    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnHit(GameObject beenHitObject)
    {
        base.OnHit(beenHitObject);
        if (beenHitObject.tag == "Player" || beenHitObject.tag == "Enemy" || beenHitObject.tag == "Stone")
        {
            foreach(ObjectPool pool in ObstaclePoolParty.Instance.Party.Pools)
            {
                if(pool.Name == poolName)
                {
                    pool.GetBackToPool(gameObject);
                }
            }
        }
    }
}
