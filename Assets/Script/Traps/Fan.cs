using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : Trap
{
    [SerializeField]
    private Transform target;
    private float timer = 0;
    private float maxTimer = 0.15f;
    private bool alreadyBlow = false;
    // Start is called before the first frame update
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!alreadyBlow)
        {
            timer += Time.deltaTime;
            if (timer >= maxTimer)
            {
                CheckHit();
                timer = 0;
            }
        }
    }

    private void CheckHit()
    {
        GameObject beenHitObject = GameManager.Instance.RayCastToObject(transform.position, target.position);
        if (beenHitObject != null && beenHitObject.tag == "Player")
        {
            OnHit(beenHitObject);
            alreadyBlow = true;
        }
    }

    protected override void OnHit(GameObject beenHitObject)
    {
        if(beenHitObject.tag == "Player")
        {
            PlayerController player = beenHitObject.GetComponent<PlayerController>();
            player.Move(target.position, DG.Tweening.Ease.Linear);
            StartCoroutine(MoveNext(player, player.MoveDuration(beenHitObject.transform.position, target.position)));
        }
    }


    private IEnumerator MoveNext(PlayerController player, float duration)
    {
        yield return new WaitForSeconds(duration);
        player.MovePlayerToNextDestination();
    }
    private void OnEnable()
    {
        alreadyBlow = false;
        timer = 0;
    }
}
