using UnityEngine;

public class ClearLiquid : MonoBehaviour
{
    [SerializeField]
    private LiquidPool liquidPoolParty;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Liquid")
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
            if(canCreate)
            {
                liquidPoolParty.CreateSmoke(collision.transform.position);
            }
            liquidPoolParty.Obstacles.GetBackToPool(collision.gameObject);
        }
    }
}
