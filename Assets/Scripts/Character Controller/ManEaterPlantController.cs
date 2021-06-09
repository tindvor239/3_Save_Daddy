using UnityEngine;

public class ManEaterPlantController : EnemyController
{

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
                GameManager.Instance.OnHitCallBack(ref timer, maxTimer, GetPlayer);
                break;
        }    
    }
    protected GameObject CastCircle()
    {
        GameObject hitObject;
        hitObject = GameManager.Instance.CircleCast(transform.position, attackRange, transform.right, 0, 1 << LayerMask.NameToLayer("Default"));
        if(hitObject == null)
        {
            hitObject = GameManager.Instance.CircleCast(transform.position, attackRange, transform.right, 0, 1 << LayerMask.NameToLayer("Pin"));
            if (hitObject == null)
            {
                hitObject = GameManager.Instance.CircleCast(transform.position, attackRange, transform.right, 0, 1 << LayerMask.NameToLayer("Player"));
                if(hitObject != null)
                {
                    return hitObject;
                }
            }
        }
        return null;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        maxTimer = 0.15f;
    }

    public override void GetPlayer()
    {
        GameObject beenHitObject = CastCircle();
        if(beenHitObject != null && beenHitObject.GetComponent<PlayerController>())
        {
            StartCoroutine(Kill(0));
        }
        timer = 0;
    }

}
