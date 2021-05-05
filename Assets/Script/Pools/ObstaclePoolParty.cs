using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.CustomComponents;
public class ObstaclePoolParty : Singleton<ObstaclePoolParty>
{
    [SerializeField]
    private PoolParty party;
    #region Properties
    public PoolParty Party { get => party; }
    #endregion
    // Start is called before the first frame update
    protected override void Awake()
    {
        #region Singleton
        Instance = this;
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
