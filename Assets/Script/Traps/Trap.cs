using UnityEngine;

public class Trap : Obstacle
{
    protected Collider2D hitBox;
    [SerializeField]
    protected ParticleSystem effect;
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
        if(beenHitObject.tag == "Player")
        {
            Debug.Log("Destroy Object");
        }
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
    }
}
