using System.Collections;
using UnityEngine;
using UnityEngine.CustomComponents;

public class ClearWater : ClearLiquid
{
    [SerializeField]
    private SpriteRenderer sprite;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Liquid" && collision.gameObject.GetComponent<Water>())
        {
            bool canCreate = true;
            foreach (GameObject gameObject in liquidPoolParty.Smokes.PooledObjects)
            {
                if (gameObject.activeInHierarchy == true)
                {
                    canCreate = false;
                    break;
                }
            }
            if (canCreate)
            {
                liquidPoolParty.CreateSmoke(collision.transform.position);
            }
            liquidPoolParty.Obstacles.GetBackToPool(collision.gameObject);
            ViewManager.Fading(sprite, 0, 4f);
            Debug.Log("In");
        }
    }

    private void OnEnable()
    {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
    }
}
