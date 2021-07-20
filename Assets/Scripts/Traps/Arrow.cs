using System.Collections.Generic;
using UnityEngine;

public class Arrow : Trap
{
    [SerializeField]
    private Vector2 size = new Vector2(1, 1);
    [SerializeField]
    private float flySpeed = 5f;
    [SerializeField]
    private AudioClip impact;
    public override void Disarmed()
    {
        isDisarmed = true;
        sound.PlayOnce(sound.clip);
    }

    protected override void Update()
    {
    }
    private void FixedUpdate()
    {
        if(isDisarmed)
        {
            transform.position += transform.right * flySpeed * Time.deltaTime;
        }
    }
    protected override void OnEnable()
    {
        isDisarmed = false;
    }
    private void CheckHit()
    {
        List<GameObject> hits =
            GameManager.Instance.BoxCastAll(transform.position, size, transform.eulerAngles.z, transform.right, 0);
        foreach(GameObject hit in hits)
        {

            if(hit.tag == "Enemy" || hit.tag == "Player")
            {
                CharacterController character = hit.GetComponent<CharacterController>();
                Debug.Log(character);
                character.Interact();
                sound.PlayOnce(impact);
                isDisarmed = false;
            }
            else if((hit.tag == "Pin"|| hit.tag == "Untagged") && hit.GetComponent<Arrow>() == null)
            {
                isDisarmed = false;
                Debug.Log(gameObject);
            }
        }
        if(hits.Count != 0)
        {
            ObstaclePoolParty.Instance.Party.GetPool(poolName).GetBackToPool(gameObject);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(isDisarmed)
        {
            CheckHit();
        }
    }
}
