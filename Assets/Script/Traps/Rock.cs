using UnityEngine;
using UnityEngine.CustomComponents;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Rock : Obstacle
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.gameObject.tag == "Enemy" || collision.transform.gameObject.tag == "Player" || collision.transform.gameObject.tag == "Trap")
        {
            OnHit(collision.transform.gameObject);
        }
    }

    protected override void OnHit(GameObject beenHitObject)
    {
        if(beenHitObject.GetComponent<CharacterController>() != null)
        {
            ObjectPool pool = CharacterPoolParty.Instance.Party.GetPool(beenHitObject);
            pool.GetBackToPool(beenHitObject);
            if(IsChildRock())
            {
                ObjectPool childRockPool = GetRockSpawnerChildPool(gameObject);
                if(childRockPool != null)
                {
                    childRockPool.GetBackToPool(gameObject);
                }
            }
            else
            {
                ObstaclePoolParty.Instance.Party.GetPool(poolName).GetBackToPool(gameObject);
            }
        }
        else
        {
            if (beenHitObject.GetComponent<Trap>() != null)
            {
                beenHitObject.GetComponent<Trap>().OnBeingHit(gameObject);
            }
        }
    }

    private bool IsChildRock()
    {
        return poolName == "Stone 0" || poolName == "Stone 1" || poolName == "Stone 2" || poolName == "Stone 3" || poolName == "Stone 4" ? true : false;
    }
    private ObjectPool GetRockSpawnerChildPool(GameObject currentObject)
    {
        foreach(GameObject gameObject in ObstaclePoolParty.Instance.Party.GetPool("Rock Spawner Pool").PooledObjects)
        {
            if(gameObject.GetComponent<RockPool>())
            {
                foreach(ObjectPool pool in gameObject.GetComponent<RockPool>().Party.Pools)
                {
                    foreach(GameObject rock in pool.PooledObjects)
                    {
                        if(rock == currentObject)
                        {
                            return pool;
                        }
                    }
                }
            }
        }
        return null;
    }
}
