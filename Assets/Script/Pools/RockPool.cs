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
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }
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
            newObject.transform.position = transform.position;
        }
    }
}
