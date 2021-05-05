using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.CustomComponents;
public class LandPoolParty : Singleton<LandPoolParty>
{
    [SerializeField]
    private List<PoolParty> parties;
    #region Properties
    public List<PoolParty> Parties { get => parties; }
    public PoolParty UpLandParty { get => parties[0]; }
    public PoolParty DownLandParty { get => parties[1]; }
    public PoolParty RightLandParty { get => parties[2]; }
    public PoolParty OtherLandParty { get => parties[Instance.parties.Count - 1]; }
    #region Up Terrains
    public ObjectPool TerrainUp1 { get => parties[0].Pools[0]; }
    public ObjectPool TerrainUp2 { get => parties[0].Pools[1]; }
    public ObjectPool TerrainUp3 { get => parties[0].Pools[2]; }
    public ObjectPool TerrainUpLeft { get => parties[0].Pools[3]; }
    public ObjectPool TerrainUpRight { get =>  parties[0].Pools[4]; }
    public ObjectPool TerrainUpHole { get => parties[0].Pools[5]; }
    #endregion
    #region Down Terrains
    public ObjectPool TerrainDown1 { get => parties[1].Pools[0]; }
    public ObjectPool TerrainDown2 { get => parties[1].Pools[1]; }
    public ObjectPool TerrainDown3 { get => parties[1].Pools[2]; }
    public ObjectPool TerrainDownLeft { get => parties[1].Pools[3]; }
    #endregion
    #region Right Terrains
    public ObjectPool TerrainRight1 { get => parties[2].Pools[0]; }
    #endregion
    public ObjectPool Grass { get => parties[parties.Count - 1].Pools[0]; }
    public ObjectPool Sand { get => parties[parties.Count - 1].Pools[1]; }
    #endregion
    protected override void Awake()
    {
        #region Singleton
        base.Awake();
        #endregion
    }
}
