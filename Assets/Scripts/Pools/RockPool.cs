using System.Collections;
using UnityEngine;
using UnityEngine.CustomComponents;

public class RockPool : ObstaclePool
{
    #region Properties
    public ObjectPool Rock2 { get => party.Pools[1]; }
    public ObjectPool Rock3 { get => party.Pools[2]; }
    public ObjectPool Rock4 { get => party.Pools[3]; }
    public ObjectPool Rock5 { get => party.Pools[4]; }
    #endregion
    protected override IEnumerator SpawnObstacle()
    {
        for (int i = 0; i < party.Pools.Count; i++)
        {
            yield return new WaitForSeconds(0.01f);

            GameObject newObject = party.Pools[i].GetPooledObject();
            if (newObject == null)
            {
                newObject = party.Pools[i].CreatePooledObject();
            }
            newObject.GetComponent<Rock>().rockPoolParty = this;
            newObject.transform.position = transform.position;
        }
    }
    private void OnDisable()
    {
        GetAllRockToPool();
    }
    public void GetAllRockToPool()
    {
        foreach (ObjectPool pool in party.Pools)
        {
            foreach (GameObject gameObject in pool.PooledObjects)
            {
                pool.GetBackToPool(gameObject);
            }
        }
    }
}
