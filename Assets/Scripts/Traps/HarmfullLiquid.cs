using UnityEngine;
using System.Collections.Generic;

public class HarmfullLiquid : Liquid
{
    protected static float maxTime = 0.2f;
    protected float timer = 0;
    [SerializeField]
    protected float radius = 1;
    protected virtual void Update()
    {
        GameManager.Instance.OnHitCallBack(ref timer, in maxTime, CheckHit);
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
    protected virtual void CheckHit()
    {
        List<GameObject> hitObjects =
            GameManager.Instance.CircleCastAll(transform.position, radius, -transform.right, 0);
        if(hitObjects.Count != 0)
        {
            foreach(GameObject hitObject in hitObjects)
            {
                bool isCharacter = hitObject.tag == "Enemy" || hitObject.tag == "Player";
                if(isCharacter)
                {
                    CharacterController character = hitObject.GetComponent<CharacterController>();
                    character.Interact();
                }
            }
        }
    }
    protected virtual void OnEnable()
    {
        timer = 0;
    }
#if UNITY_EDITOR
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
#endif
}
