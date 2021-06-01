using System.Collections;
using UnityEngine;
using UnityEngine.CustomComponents;

public class FirePit : Trap
{
    [SerializeField]
    private Fire[] fires = new Fire[4];

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Liquid" && collision.gameObject.GetComponent<Water>())
        {
            foreach(Fire fire in fires)
            {
                fire.gameObject.SetActive(false);
            }
            GetBackToPool(4f);
        }
    }

    private void OnEnable()
    {
        foreach(Fire fire in fires)
        {
            fire.gameObject.SetActive(true);
        }
    }
    private IEnumerator GetBackToPool(float duration)
    {
        yield return new WaitForSeconds(duration);
        foreach (ObjectPool pool in ObstaclePoolParty.Instance.Party.Pools)
        {
            if (pool.Name == poolName)
            {
                pool.GetBackToPool(gameObject);
            }
        }
    }
}
