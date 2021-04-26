using UnityEngine;
using UnityEngine.CustomComponents;
public class PlayerController : CharacterController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MovePlayerToNextDestination()
    {
        int index = GameManager.GetNextDestinationIndex(this);
        if (index != -1)
        {
            Vector3 pathPosition = GameManager.Instance.Destinations[index].position;
            bool isBlocked = GameManager.IsBlocked(transform.position, pathPosition, 1 << LayerMask.NameToLayer("Default"));
            if (!isBlocked)
            {
                isBlocked = GameManager.IsBlocked(transform.position, pathPosition, 1 << LayerMask.NameToLayer("Enemy"));
                if (!isBlocked)
                {
                    GameController.MoveToDestination(this, GameManager.Instance.Destinations[index]);
                    GameManager.isWin();
                }
            }
        }
    }
    public override void Interact()
    {
        PoolParty.GetPool("Player").GetBackToPool(gameObject);
    }
}
