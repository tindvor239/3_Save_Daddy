using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.CustomComponents;

[RequireComponent(typeof(Collider2D))]
public class Trap : Obstacle
{
    private Collider2D hitBox;
    private ParticleSystem effect;
    private void Awake()
    {
        hitBox = GetComponent<Collider2D>();
        if (GetComponent<ParticleSystem>())
        {
            effect = GetComponent<ParticleSystem>();
        }
    }
    private void Start()
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnHit(collision.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
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
    public void Disarmed()
    {
        hitBox.enabled = false;
        if(effect != null)
        {
            effect.Stop();
        }
    }
}
