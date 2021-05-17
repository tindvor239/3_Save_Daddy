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
            ObstaclePoolParty.Instance.Party.GetPool(poolName).GetBackToPool(gameObject);
        }
        else
        {
            if (beenHitObject.GetComponent<Trap>() != null)
            {
                beenHitObject.GetComponent<Trap>().OnBeingHit(gameObject);
            }
        }
    }
}
