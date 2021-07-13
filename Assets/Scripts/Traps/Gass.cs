using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gass : Trap
{
    [SerializeField]
    private Transform[] points = new Transform[2];
    private float timer = 0;
    private static float maxTimer = 0.2f;
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    private void Update()
    {
        GameManager.Instance.OnHitCallBack(ref timer, in maxTimer, CheckHit);
    }
    private void CheckHit()
    {
        bool beenBlocked = GameManager.Instance.IsBlocked(points[0].position, points[1].position, 1 << LayerMask.NameToLayer("Pin"));
        if (beenBlocked)
        {
            Disarmed();
            sound.source.Stop();
        }
        else
        {
            GameObject beenHitObject = GameManager.Instance.RayCastToObject(points[0].position, points[1].position);
            if(beenHitObject != null)
            {
                OnHit(beenHitObject);
            }
            else
            {
                if(isDisarmed)
                {
                    isDisarmed = false;
                    effect.Play();
                }
            }
        }
        timer = 0;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        sound.PlayLoop(sound.clip);
    }
}
