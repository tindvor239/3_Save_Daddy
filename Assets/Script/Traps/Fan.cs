using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : Trap
{
    [SerializeField]
    private Transform target;
    private float timer = 0;
    private float maxTimer = 0.15f;
    // Start is called before the first frame update
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= maxTimer)
        {
            CheckHit();
            timer = 0;
        }
    }

    private void CheckHit()
    {
        GameObject beenHitObject = GameManager.RayCastToObject(transform.position, target.position);
        if (beenHitObject != null)
        {
            OnHit(beenHitObject);
        }
    }

    protected override void OnHit(GameObject beenHitObject)
    {
        
    }
}
