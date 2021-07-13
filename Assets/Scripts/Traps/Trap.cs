using UnityEngine;
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
            if (GetComponentInChildren<ParticleSystem>())
            {
                effect = GetComponentInChildren<ParticleSystem>();
            }
        }
    }

    public override void OnBeingHit(GameObject hitObject)
    {
        Debug.Log("Destroy Self");
    }

    protected override void OnHit(GameObject beenHitObject)
    {
        CharacterController character = beenHitObject.GetComponent<CharacterController>();
        bool isCharacter = character != null;
        if (isCharacter && character.state != CharacterController.CharacterState.die)
        {
            character.Interact();
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
        if (hitBox != null)
        {
            hitBox.enabled = false;
        }
        if (effect != null)
        {
            effect.Stop();
        }
        isDisarmed = true;
    }

}
