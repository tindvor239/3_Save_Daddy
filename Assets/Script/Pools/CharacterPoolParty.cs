using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.CustomComponents;

public class CharacterPoolParty : Singleton<CharacterPoolParty>
{
    [SerializeField]
    private PoolParty party;
    #region Properties
    public PoolParty Party { get => party; }
    public ObjectPool PlayerPool { get => party.Pools[0]; }
    public ObjectPool SharkPool { get => party.Pools[1]; }
    #endregion

    protected override void Awake()
    {
        #region Singleton
        base.Awake();
        #endregion
    }
}
