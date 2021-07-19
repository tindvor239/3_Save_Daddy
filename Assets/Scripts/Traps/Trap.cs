using UnityEngine;
using System;
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
    protected virtual void Update()
    {
        if (GameManager.State == GameManager.GameState.play)
        {
            OnTriggered(Triggering);
        }
        else
        {
            OnTriggered(Detriggering);
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
    public override void OnBeingHit(GameObject hitObject)
    {
    }

    protected void OnTriggered(Action action)
    {
        action();
    }
    protected virtual void Triggering()
    {
        if (GetComponent<Rigidbody2D>() != null)
        {
            Rigidbody2D rigid = GetComponent<Rigidbody2D>();
            if (rigid.simulated == false)
            {
                rigid.simulated = true;
            }
        }
    }
    protected virtual void Detriggering()
    {
        if (GetComponent<Rigidbody2D>() != null)
        {
            Rigidbody2D rigid = GetComponent<Rigidbody2D>();
            if (rigid.simulated)
            {
                rigid.simulated = false;
            }
        }
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        EnableRigid();
    }
    protected void EnableRigid()
    {
        if (GetComponent<Rigidbody2D>() != null)
        {
            GetComponent<Rigidbody2D>().simulated = false;
        }
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
}
