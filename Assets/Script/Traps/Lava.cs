﻿using System.Collections;
using UnityEngine;
using UnityEngine.CustomComponents;

public class Lava : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            foreach(ObjectPool pool in CharacterPoolParty.Instance.Party.Pools)
            {
                if (pool.PooledObjects.Contains(collision.gameObject))
                {
                    pool.GetBackToPool(collision.gameObject);
                }
            }
        }
        else if(collision.gameObject.tag == "Player")
        {
            if(CharacterPoolParty.Instance.PlayerPool.PooledObjects.Contains(collision.gameObject))
            {
                CharacterPoolParty.Instance.PlayerPool.GetBackToPool(collision.gameObject);
            }
        }
    }
}
