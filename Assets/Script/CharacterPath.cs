﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPath : Model
{
    // Start is called before the first frame update
    void Start()
    {
        poolName = PathPoolParty.Instance.Party.Pools[0].Name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
