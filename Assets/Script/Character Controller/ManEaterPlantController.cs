using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManEaterPlantController : CharacterController
{
    [SerializeField]
    private float attackRange;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.Player != null)
        {
            GameObject target = GameManager.Instance.RayCastToObject(transform.position, new Vector3(transform.position.x - attackRange, transform.position.y));
            if(target != null && target.tag == "Player" && target.GetComponent<PlayerController>() != null)
            {
                CharacterPoolParty.Instance.PlayerPool.GetBackToPool(target);
            }
            else
            {
                target = GameManager.Instance.RayCastToObject(transform.position, new Vector3(transform.position.x + attackRange, transform.position.y));
                if (target != null && target.tag == "Player" && target.GetComponent<PlayerController>() != null)
                {
                    CharacterPoolParty.Instance.PlayerPool.GetBackToPool(target);
                }
            }
        }
    }
}
