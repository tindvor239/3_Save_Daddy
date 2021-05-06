using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.CustomComponents;

public class MapEditor : Singleton<MapEditor>
{
    [SerializeField]
    private Map currentMap;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private CharacterPoolParty characterPoolParty;
    [SerializeField]
    private ObstaclePoolParty obstaclePoolParty;
    [SerializeField]
    private PinPoolParty pinPoolParty;
    [SerializeField]
    private PathPoolParty pathPoolParty;
    [SerializeField]
    private LandPoolParty landPoolParty;
    #region Properties
    public Map CurrentMap { get => currentMap; }
    public CharacterPoolParty CharacterPoolParty { get => characterPoolParty; }
    public ObstaclePoolParty ObstaclePoolParty { get => obstaclePoolParty; }
    public PinPoolParty PinPoolParty { get => pinPoolParty; }
    public PathPoolParty PathPoolParty { get => pathPoolParty; }
    public LandPoolParty LandPoolParty { get => landPoolParty; }
    public GameManager GameManager { get => gameManager; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
    }
}

[CustomEditor(typeof(MapEditor))]
public class MapScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        MapEditor scriptEditor = (MapEditor)target;

        List<PoolParty> poolParties = new List<PoolParty>();
        poolParties.Add(scriptEditor.CharacterPoolParty.Party);
        poolParties.Add(scriptEditor.ObstaclePoolParty.Party);
        poolParties.Add(scriptEditor.PinPoolParty.Party);
        poolParties.Add(scriptEditor.PathPoolParty.Party);
        foreach (PoolParty poolParty in scriptEditor.LandPoolParty.Parties)
        {
            poolParties.Add(poolParty);
        }

        if (GUILayout.Button("Save"))
        {
            scriptEditor.CurrentMap.Save();
        }
        if(GUILayout.Button("Load"))
        {
            foreach(PoolParty poolParty in poolParties)
            {
                foreach(ObjectPool pool in poolParty.Pools)
                {
                    pool.PooledObjects.RemoveAll(x => x == null);
                }
            }
            scriptEditor.CurrentMap.Load(poolParties);
        }
        if (GUILayout.Button("Clear"))
        {
            foreach (PoolParty poolParty in poolParties)
            {
                foreach(ObjectPool pool in poolParty.Pools)
                {
                    for(int i = 0; i < pool.PooledObjects.Count; i++)
                    {
                        pool.GetBackToPool(pool.PooledObjects[i]);
                    }
                }
            }
        }
    }
}
