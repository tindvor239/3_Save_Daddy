using UnityEngine;

public class ManEaterPlantController : EnemyController
{
    private float timer = 0;
    private float maxTimer = 0.15f;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    protected override void Update()
    {
        switch(state)
        {
            case CharacterState.die:
                break;
            default:
                timer += Time.deltaTime;
                if (timer >= maxTimer)
                {
                    GetPlayer();
                    timer = 0;
                }
                break;
        }    
    }
    protected GameObject CastCircle()
    {
        GameObject hitObject;
        hitObject = GameManager.CircleCast(transform.position, attackRange, transform.right, 0, 1 << LayerMask.NameToLayer("Default"));
        if(hitObject == null)
        {
            hitObject = GameManager.CircleCast(transform.position, attackRange, transform.right, 0, 1 << LayerMask.NameToLayer("Pin"));
            if (hitObject == null)
            {
                hitObject = GameManager.CircleCast(transform.position, attackRange, transform.right, 0, 1 << LayerMask.NameToLayer("Player"));
                if(hitObject != null)
                {
                    return hitObject;
                }
            }
        }
        return null;
    }

    public override void GetPlayer()
    {
        GameObject beenHitObject = CastCircle();
        if(beenHitObject != null && beenHitObject.GetComponent<PlayerController>())
        {
            Kill(1);
            Invoke("OnMove", actingDelay);
        }
    }
    
}
