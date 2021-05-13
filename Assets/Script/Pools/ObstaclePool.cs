﻿using System.Collections;
using UnityEngine;
using UnityEngine.CustomComponents;

public abstract class ObstaclePool : Model
{
    [SerializeField]
    protected sbyte amount;
    [SerializeField]
    protected PoolParty party;
    #region Properties
    public ObjectPool Obstacles { get => party.Pools[0]; }
    #endregion
    // Start is called before the first frame update
    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {
    }
    public void SpawnObstaclesOnLoad()
    {
        StartCoroutine(SpawnObstacle());
    }
    protected virtual IEnumerator SpawnObstacle()
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
        }
    }
    protected IEnumerator Extinguish(ObjectPool pool, GameObject gameObject, float duration)
    {
        yield return new WaitForSeconds(duration);
        pool.GetBackToPool(gameObject);
    }
}
