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
        Debug.Log(collision.transform.gameObject);
        if (collision.transform.gameObject.tag == "Enemy" || collision.transform.gameObject.tag == "Player" || collision.transform.gameObject.tag == "Trap")
        {
            Debug.Log("In");
            OnHit(collision.transform.gameObject);
        }
    }

    protected override void OnHit(GameObject beenHitObject)
    {
        if(beenHitObject.GetComponent<CharacterController>() != null)
        {
            beenHitObject.GetComponent<CharacterController>().Interact();
        }
        else
        {
            if (beenHitObject.GetComponent<Trap>() != null)
            {
                Debug.Log("In 2");
                beenHitObject.GetComponent<Trap>().OnBeingHit(gameObject);
            }
        }
    }
}
