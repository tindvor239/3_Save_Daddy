using System.Collections;
using UnityEngine;
using UnityEngine.CustomComponents;

public abstract class ObstaclePool : Model
{
    [SerializeField]
    protected sbyte amount;
    [SerializeField]
    protected PoolParty party;
    protected int count = 0;

    [SerializeField]
    protected AudioClip clip;
    protected AudioSource sound;
    #region Properties
    public PoolParty Party { get => party; }
    public ObjectPool Obstacles { get => party.Pools[0]; }
    public AudioSource AudioSource { get => sound; }
    #endregion
    protected void Start()
    {
        sound = SoundManager.Instance.Sound;
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
    protected void OnEnable()
    {
        count = 0;
    }

    public void PlaySound()
    {
        if (count <= 0)
        {
            sound.PlayOneShot(clip);
            count++;
        }
    }
}
