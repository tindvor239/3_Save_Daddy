﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : Trap
{
    [SerializeField]
    private SpriteRenderer[] blades = new SpriteRenderer[2];
    [SerializeField]
    private float spinSpeed = 800f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void SpiningBlades()
    {
        foreach(SpriteRenderer blade in blades)
        {
            float z = blade.transform.eulerAngles.z + (Time.deltaTime * spinSpeed);
            GameController.Rotate(blade.transform, new Vector3(0, 0, z));
        }
        //sequence.SetLoops(-1);
    }
    // Update is called once per frame
    void Update()
    {
        SpiningBlades();
    }
}