﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Trap
{
    [SerializeField]
    private LineRenderer line;
    [SerializeField]
    private Transform[] transforms = new Transform[2];
    [SerializeField]
    private ParticleSystem beamSparkle;
    private float timer = 0;
    private float maxTimer = 0.15f;

    protected override void Start()
    {
        base.Start();
        if(GameManager.Instance != null)
        {
            GameManager.Instance.startParalax = true;
        }

    }
    // Update is called once per frame
    void Update()
    {
        if(GameManager.State == GameManager.GameState.play)
        {
            line.SetPosition(0, transforms[0].position);
            timer += Time.deltaTime;
            if(timer >= maxTimer)
            {
                CheckHit();
                timer = 0;
            }
        }
    }
    private void CheckHit()
    {
        RaycastHit2D hit = Physics2D.Raycast(transforms[0].position, GameManager.Instance.GetDirectionVector(transforms[0].position, transforms[1].position));
        if (hit.point != new Vector2())
        {
            line.SetPosition(1, hit.point);
            if(hit.collider != null)
            {
                OnHit(hit.collider.gameObject);
            }
        }
        else
        {
            line.SetPosition(1, transforms[1].position);
        }
        beamSparkle.transform.position = line.GetPosition(1);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if(GameManager.Instance != null)
        {
            GameManager.Instance.startParalax = true;
        }
        sound.PlayLoop(sound.clip);
    }
    protected void OnDisable()
    {
        GameManager.Instance.startParalax = false;
    }

    public override void Disarmed()
    {
        base.Disarmed();
        if(beamSparkle != null)
        {
            beamSparkle.Stop();
        }
    }
}
