using UnityEngine;
using System.Collections.Generic;

public class HarmfullLiquid : Liquid
{
    private static float maxTime = 0.2f;
    private float timer = 0;
    [SerializeField]
    private float radius = 1;
    private void Update()
    {
        GameManager.Instance.OnHitCallBack(ref timer, in maxTime, CheckHit);
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
    protected void CheckHit()
    {
        List<GameObject> hitObjects =
            GameManager.CircleCastAll(transform.position, radius, -transform.right, 0);
        if(hitObjects.Count != 0)
        {
            foreach(GameObject hitObject in hitObjects)
            {
                bool isCharacter = hitObject.tag == "Enemy" || hitObject.tag == "Player";
                if(isCharacter)
                {
                    Debug.Log(hitObject);
                    CharacterController character = hitObject.GetComponent<CharacterController>();
                    character.Interact();
                    return;
                }
            }
        }
    }
    private void OnEnable()
    {
        timer = 0;
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
#endif
}
