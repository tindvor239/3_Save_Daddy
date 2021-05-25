using UnityEngine;
using UnityEngine.CustomComponents;
public class Trap : Obstacle
{
    protected Collider2D hitBox;
    [SerializeField]
    protected ParticleSystem effect;
    [SerializeField]
    protected bool isDisarmed = false;
    protected virtual void Awake()
    {
        if (GetComponent<Collider2D>())
        {
            hitBox = GetComponent<Collider2D>();
        }
        if (GetComponent<ParticleSystem>())
        {
            effect = GetComponent<ParticleSystem>();
        }
        else
        {
            if(GetComponentInChildren<ParticleSystem>())
            {
                effect = GetComponentInChildren<ParticleSystem>();
            }
        }
    }
    protected virtual void Start()
    {
    }
    public override void OnBeingHit(GameObject hitObject)
    {
        Debug.Log("Destroy Self");
    }

    protected override void OnHit(GameObject beenHitObject)
    {
        if (beenHitObject.tag == "Player")
        {
            UIController.Instance.ShowGameOverUI(true);
        }
        else if(beenHitObject.tag == "Enemy")
        {
            ObjectPool pool = GetCharacterPool(beenHitObject);
            if(pool != null)
            {
                pool.GetBackToPool(beenHitObject);
            }
        }
    }
    private ObjectPool GetCharacterPool(GameObject gameObject)
    {
        foreach (ObjectPool pool in CharacterPoolParty.Instance.Party.Pools)
        {
            if (pool != CharacterPoolParty.Instance.PlayerPool)
            {
                foreach (GameObject pooledObject in pool.PooledObjects)
                {
                    if (pooledObject == gameObject && pooledObject.activeInHierarchy)
                    {
                        return pool;
                    }
                }
            }
        }
        return null;
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        OnHit(collision.gameObject);
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            OnBeingHit(collision.gameObject);
        }
        else
        {
            collision.GetComponent<PlayerController>().Interact();
        }
    }
    public virtual void Disarmed()
    {
        if(hitBox != null)
        {
            hitBox.enabled = false;
        }
        if(effect != null)
        {
            effect.Stop();
        }
        isDisarmed = true;
    }
}
