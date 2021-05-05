using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.CustomComponents;

public class PathPoolParty : Singleton<PathPoolParty>
{
    [SerializeField]
    private PoolParty party;
    #region Properties
    public PoolParty Party { get => party; }
    #endregion
    protected override void Awake()
    {
        #region Singleton
        base.Awake();
        #endregion
    }
}
