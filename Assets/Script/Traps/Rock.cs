using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Rock : Obstacle
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.gameObject.tag == "Enemy" || collision.transform.gameObject.tag == "Player")
        {
            OnHit(collision.transform.gameObject);
        }
    }

    protected override void OnHit(GameObject beenHitObject)
    {
        //TO DO: => OnInteract().
        if(beenHitObject.GetComponent<CharacterController>() != null)
        {
            beenHitObject.GetComponent<CharacterController>().Interact();
        }
        else
        {
            if (beenHitObject.GetComponent<Trap>() != null)
            {
                beenHitObject.GetComponent<Trap>().OnBeingHit(gameObject);
            }
        }
    }
}
