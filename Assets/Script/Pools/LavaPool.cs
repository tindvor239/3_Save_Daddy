using UnityEngine;
using UnityEngine.CustomComponents;

public class LavaPool : ObstaclePool
{
    #region Properties
    public ObjectPool Smokes { get => party.Pools[1]; }
    #endregion
    protected override void Start()
    {
        base.Start();
    }
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
        StartCoroutine(Extinguish(Obstacles, newObject, 4f));
    }
}
