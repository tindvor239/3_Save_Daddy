using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gass : Trap
{
    [SerializeField]
    private Transform[] points = new Transform[2];
    private float timer = 0;
    private float maxTimer = 0.15f;
    protected override void Awake()
    {
        base.Awake();
    }
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
        GameObject beenHitObject = GameManager.RayCastToObject(points[0].position, points[1].position);
        if (beenHitObject != null)
        {
            OnHit(beenHitObject);
        }
    }
    protected override void OnHit(GameObject beenHitObject)
    {
        if (beenHitObject.tag == "Player")
        {
            Debug.Log("Destroy Object");
        }
        else if(beenHitObject.tag == "Untagged")
        {
            if(effect.isStopped == false)
            {
                Disarmed();
            }
        }
    }
}
