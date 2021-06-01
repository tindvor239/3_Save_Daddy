using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.CustomComponents;
public class PinPoolParty : Singleton<PinPoolParty>
{
    [SerializeField]
    private PoolParty party;
    public PoolParty Party { get => party; }
    protected override void Awake()
    {
        #region Singleton
        base.Awake();
        #endregion
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
