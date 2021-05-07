using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.CustomComponents;

public class MapEditor : Singleton<MapEditor>
{
    [SerializeField]
    public Map currentMap;
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
    public CharacterPoolParty CharacterPoolParty { get => characterPoolParty; }
    public ObstaclePoolParty ObstaclePoolParty { get => obstaclePoolParty; }
    public PinPoolParty PinPoolParty { get => pinPoolParty; }
    public PathPoolParty PathPoolParty { get => pathPoolParty; }
    public LandPoolParty LandPoolParty { get => landPoolParty; }
    public GameManager GameManager { get => gameManager; }
    public List<PoolParty> PoolParties { get => GetPoolParties(); }
    #endregion
    private List<PoolParty> GetPoolParties()
    {
        List<PoolParty> poolParties = new List<PoolParty>();
        poolParties.Add(characterPoolParty.Party);
        poolParties.Add(obstaclePoolParty.Party);
        poolParties.Add(pinPoolParty.Party);
        poolParties.Add(pathPoolParty.Party);
        foreach(PoolParty poolParty in landPoolParty.Parties)
        {
            poolParties.Add(poolParty);
        }
        return poolParties;
    }

    protected override void Awake()
    {
        base.Awake();
    }

    public void Load()
    {
        if(currentMap != null)
        {
            foreach (PoolParty poolParty in PoolParties)
            {
                foreach (ObjectPool pool in poolParty.Pools)
                {
                    pool.PooledObjects.RemoveAll(x => x == null);
                }
            }
            currentMap.Load(PoolParties);
            gameManager.GetDestinations();
            Debug.Log("Loaded");
        }
        else
        {
            Debug.Log("Don't have map");
        }
    }
}

[CustomEditor(typeof(MapEditor))]
public class MapScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        MapEditor scriptEditor = (MapEditor)target;
        List<PoolParty> poolParties = scriptEditor.PoolParties;

        if (GUILayout.Button("Save"))
        {
            scriptEditor.currentMap.Save();
        }
        if(GUILayout.Button("Load"))
        {
            scriptEditor.Load();
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
