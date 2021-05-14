using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Trap
{
    [SerializeField]
    private LineRenderer line;
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
        if(timer >= maxTimer)
        {
            CheckHit();
            timer = 0;
        }
    }
    private void CheckHit()
    {
        GameObject beenHitObject = GameManager.Instance.RayCastToObject(line.GetPosition(0), line.GetPosition(1));
        if(beenHitObject != null)
        {
            OnHit(beenHitObject);
        }
    }
}
