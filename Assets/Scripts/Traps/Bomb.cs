using System.Collections;
using UnityEngine;
using UnityEngine.CustomComponents;

public class Bomb : Trap
{
    protected override void OnHit(GameObject beenHitObject)
    {
        base.OnHit(beenHitObject);
        Debug.Log(beenHitObject.gameObject);
        if (beenHitObject.tag == "Player" || beenHitObject.tag == "Enemy" || beenHitObject.tag == "Stone")
        {
            sound.PlayOnce(sound.clip);
            effect.Play();
            Visible(false);
            StartCoroutine(OnDestroyed());
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
    private void Visible(bool isActive)
    {
        GetComponent<SpriteRenderer>().enabled = isActive;
        GetComponent<Collider2D>().enabled = isActive;
    }

}
