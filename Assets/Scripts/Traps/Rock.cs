using UnityEngine;
using UnityEngine.CustomComponents;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Rock : Trap
{
    public RockPool rockPoolParty;
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject beenHitObject = collision.transform.gameObject;
        if (beenHitObject.tag == "Enemy" || beenHitObject.tag == "Player" || beenHitObject.tag == "Trap")
        {
            OnHit(collision.transform.gameObject);
        }
    }
    protected override void OnHit(GameObject beenHitObject)
    {
        CharacterController characterController = beenHitObject.GetComponent<CharacterController>();
        if (characterController != null)
        {
            characterController.Interact();
            if(IsChildRock())
            {
                bool isHitCharacter = beenHitObject.tag == "Enemy" || beenHitObject.tag == "Player";
                if (rockPoolParty != null && isHitCharacter)
                {
                    rockPoolParty.GetAllRockToPool();
                }
            }
            else
            {
                ObstaclePoolParty.Instance.Party.GetPool(poolName).GetBackToPool(gameObject);
            }
            sound.PlayOnce(sound.clip);
        }
        else
        {
            if (beenHitObject.GetComponent<Trap>() != null)
            {
                beenHitObject.GetComponent<Trap>().OnBeingHit(gameObject);
                sound.PlayOnce(sound.clip);
            }
        }
    }
    private bool IsChildRock()
    {
        return poolName == "Stone 0" || poolName == "Stone 1" || poolName == "Stone 2" || poolName == "Stone 3" || poolName == "Stone 4" ? true : false;
    }
}
