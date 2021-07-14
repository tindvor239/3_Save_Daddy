using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.CustomComponents;

public class Bomb : Trap
{
    [SerializeField]
    private float radius = 1f;
    protected override void OnHit(GameObject beenHitObject)
    {
        if (beenHitObject.tag == "Player" || beenHitObject.tag == "Enemy" || beenHitObject.GetComponent<Rock>() != null)
        {
            CheckHit();
            sound.PlayOnce(sound.clip);
            effect.Play();
            Visible(false);
            StartCoroutine(OnDestroyed());
        }
    }
    private void CheckHit()
    {
        List<GameObject> beenHitObjects = RayCastingToObject();
        foreach(var gameObject in beenHitObjects)
        {
            if (gameObject != null && (gameObject.tag == "Player" || gameObject.tag == "Enemy"))
            {
                if (gameObject.GetComponent<PlayerController>() != null)
                {
                    base.OnHit(gameObject);
                }
            }
            else if(gameObject.GetComponent<Rock>() != null)
            {
                Rock rock = gameObject.GetComponent<Rock>();
                if(rock.PoolName == "Round Boulder Pool")
                {
                    ObstaclePoolParty.Instance.Party.GetPool(rock.PoolName).GetBackToPool(rock.gameObject);
                }
                else if(rock.PoolName == "Spike Ball Pool")
                {
                    Vector2 direction = SpikePushDirection(rock.gameObject);
                    Vector2 pushDirection = new Vector2(10 * direction.x, 5 * direction.y);
                    rock.GetComponent<Rigidbody2D>().AddForce(pushDirection, ForceMode2D.Impulse);
                }
                else if(rock.rockPoolParty != null)
                {
                    rock.rockPoolParty.GetAllRockToPool();
                }
            }
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Visible(true);
    }

    private IEnumerator OnDestroyed()
    {
        yield return new WaitForSeconds(1.1f);
        foreach (ObjectPool pool in ObstaclePoolParty.Instance.Party.Pools)
        {
            if (pool.Name == poolName)
            {
                pool.GetBackToPool(gameObject);
            }
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
#endif
    private Vector2 SpikePushDirection(GameObject spikeBall)
    {
        float x = transform.position.x - spikeBall.transform.position.x;
        float y = transform.position.y - spikeBall.transform.position.y;
        return new Vector2(Mathf.Sign(x),Mathf.Sign(y));
    }
    private List<GameObject> RayCastingToObject()
    {
        return GameManager.Instance.CircleCastAll(transform.position, radius, transform.forward, 0);
    }
    private void Visible(bool isActive)
    {
        GetComponent<SpriteRenderer>().enabled = isActive;
        GetComponent<Collider2D>().enabled = isActive;
    }

}
