using UnityEngine;
using System.Collections.Generic;

public class Rad : HarmfullLiquid
{
    private Rigidbody2D rigbody;
    [SerializeField]
    private float moveSpeed = 2f;
    private void Start()
    {
        rigbody = GetComponent<Rigidbody2D>();
    }
    protected override void Update()
    {
        base.Update();
        rigbody.velocity += (Vector2)transform.right * Time.deltaTime * moveSpeed;
    }
    protected override void CheckHit()
    {
        List<GameObject> hitObjects =
            GameManager.Instance.CircleCastAll(transform.position, radius, -transform.right, 0);
        if (hitObjects.Count != 0)
        {
            foreach (GameObject hitObject in hitObjects)
            {
                bool isCharacter = hitObject.tag == "Enemy" || hitObject.tag == "Player";
                if (isCharacter)
                {
                    CharacterController character = hitObject.GetComponent<CharacterController>();
                    character.Interact();
                    liquidPool.GetAllLiquidToPool();
                    return;
                }
            }
        }
    }

}
