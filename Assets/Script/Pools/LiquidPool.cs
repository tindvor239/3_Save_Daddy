using System.Collections;
using UnityEngine;
using UnityEngine.CustomComponents;

public class LiquidPool : ObstaclePool
{
    #region Properties
    public ObjectPool Liquids { get => party.Pools[0]; }
    public ObjectPool Smokes { get => party.Pools[1]; }
    #endregion
    public void CreateSmoke(Vector2 position)
    {
        GameObject newObject = Smokes.GetPooledObject();
        if(newObject == null)
        {
            newObject = Smokes.CreatePooledObject();
        }
        ParticleSystem particle = null;
        if (newObject.GetComponent<ParticleSystem>())
        {
            particle = newObject.GetComponent<ParticleSystem>();
            particle.Play();
        }
        else
        {
            if(newObject.transform.childCount > 0)
            {
                particle = newObject.GetComponentInChildren<ParticleSystem>();
                particle.Play();
            }
        }
        newObject.transform.position = position;
        StartCoroutine(Extinguish(Smokes, newObject, 4f));
    }
    public void GetAllLiquidToPool()
    {
        foreach (GameObject pooledObject in Liquids.PooledObjects)
        {
            Liquids.GetBackToPool(pooledObject);
        }
    }

    protected override IEnumerator SpawnObstacle()
    {
        for (int i = 0; i < amount; i++)
        {
            yield return new WaitForSeconds(0.01f);

            GameObject newObject = Obstacles.GetPooledObject();
            if (newObject == null)
            {
                newObject = Obstacles.CreatePooledObject();
            }
            newObject.transform.position = transform.position;
            newObject.GetComponent<Liquid>().liquidPool = this;
        }
}

    private void OnDisable()
    {
        foreach(ObjectPool pool in party.Pools)
        {
            foreach(GameObject gameObject in pool.PooledObjects)
            {
                pool.GetBackToPool(gameObject);
            }
        }
    }
}
