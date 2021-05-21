using UnityEngine;
using UnityEngine.CustomComponents;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Rock : Obstacle
{
    public RockPool rockPoolParty;
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
        GameObject beenHitObject = collision.transform.gameObject;
        if (beenHitObject.tag == "Enemy" || beenHitObject.tag == "Player" || beenHitObject.tag == "Trap")
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
                bool isHitCharacter = beenHitObject.tag == "Enemy" || beenHitObject.tag == "Player";
                if (rockPoolParty != null && isHitCharacter)
                {
                    rockPoolParty.GetAllRockToPool();
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
}
