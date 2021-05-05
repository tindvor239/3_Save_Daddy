using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearLava : MonoBehaviour
{
    [SerializeField]
    private LavaPool lavaPoolParty;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Lava")
        {
            bool canCreate = true;
            foreach (GameObject go in lavaPoolParty.Smokes.PooledObjects)
            {
                if (go.activeInHierarchy == true)
                {
                    canCreate = false;
                    break;
                }
            }
            if(canCreate)
            {
                lavaPoolParty.CreateSmoke(collision.transform.position);
            }
            lavaPoolParty.Obstacles.GetBackToPool(collision.gameObject);
        }
    }
}
