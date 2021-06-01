using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Trap
{
    [SerializeField]
    private Vector2 size = new Vector2(1, 1);
    private float timer = 0;
    private float maxTimer = 0.12f;
    [SerializeField]
    private float flySpeed = 5f;
    public override void Disarmed()
    {
        isDisarmed = true;
    }

    private void Update()
    {
        if(isDisarmed)
        {
            GameManager.Instance.OnHitCallBack(ref timer, in maxTimer, CheckHit);
        }
    }
    private void FixedUpdate()
    {
        if(isDisarmed)
        {
            transform.position += transform.right * flySpeed * Time.deltaTime;
        }
    }
    private void OnEnable()
    {
        isDisarmed = false;
        timer = 0;
        maxTimer = 0.2f;
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
                character.Interact();
                isDisarmed = false;
            }
            else if((hit.tag == "Pin"|| hit.tag == "Untagged") && hit.GetComponent<Arrow>() == null)
            {
                isDisarmed = false;
            }
        }
        if(hits.Count != 0)
        {
            ObstaclePoolParty.Instance.Party.GetPool(poolName).GetBackToPool(gameObject);
        }
        timer = 0;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }
#endif
}
