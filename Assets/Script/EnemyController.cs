using System.Collections;
using UnityEngine;
using UnityEngine.CustomComponents;

public class EnemyController : CharacterController
{
    private CharacterController player;
    protected override void Start()
    {
        base.Start();
        player = GameController.PlayerController;
    }
    public void GetPlayer()
    {
        bool isBlocked = GameManager.IsBlocked(transform.position, player.transform.position, 1 << LayerMask.NameToLayer("Default"));
        if(!isBlocked)
        {
            isBlocked = GameManager.IsBlocked(transform.position, player.transform.position, 1 << LayerMask.NameToLayer("Player"));
            if(!isBlocked)
            {
                GameController.Move(transform, player.transform.position, 0.6f);
                StartCoroutine(Kill(0.7f));
            }
        }
    }

    protected IEnumerator Kill(float duration)
    {
        yield return new WaitForSeconds(duration);
        PoolParty.GetPool("Player").GetBackToPool(player.gameObject);
    }

    public override void Interact()
    {
        PoolParty.GetPool("Shark").GetBackToPool(gameObject);
    }
}
