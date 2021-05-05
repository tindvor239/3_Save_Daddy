using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Trap : Obstacle
{
    protected Collider2D hitBox;
    protected ParticleSystem effect;
    protected virtual void Awake()
    {
        hitBox = GetComponent<Collider2D>();
        if (GetComponent<ParticleSystem>())
        {
            effect = GetComponent<ParticleSystem>();
        }
    }
    protected virtual void Start()
    {
    }
    public override void OnBeingHit(GameObject hitObject)
    {
        Debug.Log("Destroy Self");
        Destroy(gameObject);
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
        hitBox.enabled = false;
        if(effect != null)
        {
            effect.Stop();
        }
    }
}
