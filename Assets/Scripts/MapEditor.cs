using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.CustomComponents;
using System.Collections;

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

    public bool isDoneClear = false;
    public int processValue = 0;
    public int processMaxValue = 0;
    #region Properties
    public CharacterPoolParty CharacterPoolParty { get => characterPoolParty; }
    public ObstaclePoolParty ObstaclePoolParty { get => obstaclePoolParty; }
    public PinPoolParty PinPoolParty { get => pinPoolParty; }
    public PathPoolParty PathPoolParty { get => pathPoolParty; }
    public LandPoolParty LandPoolParty { get => landPoolParty; }
    public GameManager GameManager { get => gameManager; }
    public List<PoolParty> PoolParties { get => GetPoolParties(); }
    public int Process
    {
        get
        {
            try
            {
                return ((processValue * 100) / processMaxValue);
            }
            catch
            {
                return 0;
            }
        }
    }
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
            processMaxValue = currentMap.packages.Count + currentMap.pinPackages.Count;
            processValue = 0;
            foreach (PoolParty poolParty in PoolParties)
            {
                foreach (ObjectPool pool in poolParty.Pools)
                {
                    pool.PooledObjects.RemoveAll(x => x == null);
                }
            }
            Clear();
            if (Application.isPlaying)
            {
                StartCoroutine(OnWaitingClearModels());
            }
            else
            {
                currentMap.Load(PoolParties, gameManager);
            }
        }
        else
        {
            Debug.Log("Don't have map");
        }
    }
    public void Clear()
    {
        List<PoolParty> poolParties = PoolParties;
        isDoneClear = false;
        if(SoundManager.Instance != null)
        {
            SoundManager.Instance.StopAllSound();
        }
        if (Application.isPlaying)
        {
            StartCoroutine(OnClearingModels(poolParties));
        }
        else
        {
            foreach (PoolParty poolParty in poolParties)
            {
                foreach (ObjectPool pool in poolParty.Pools)
                {
                    foreach (GameObject gameObject in pool.PooledObjects)
                    {
                        pool.GetBackToPool(gameObject);
                    }
                }
            }

            List<Model> models = FindObjectsOfType<Model>().Where(f => f.gameObject.activeInHierarchy).ToList();
            if (models.Count != 0)
            {
                ClearModel(models);
                //Clear Models NOT in pool
            }
        }
    }
    private IEnumerator OnWaitingClearModels()
    {
        while(isDoneClear == false)
        {
            yield return null;
        }
        currentMap.Load(PoolParties, gameManager);
    }
    private IEnumerator OnClearingModels(List<PoolParty> poolParties)
    {
        while (isDoneClear == false)
        {
            foreach (PoolParty poolParty in poolParties)
            {
                foreach (ObjectPool pool in poolParty.Pools)
                {
                    foreach (GameObject gameObject in pool.PooledObjects)
                    {
                        pool.GetBackToPool(gameObject);
                        yield return null;
                    }
                }
            }

            List<Model> models = FindObjectsOfType<Model>().Where(f => f.gameObject.activeInHierarchy).ToList();
            if (models.Count != 0)
            {
                ClearModel(models);
                //Clear Models NOT in pool
            }
            isDoneClear = true;
        }
    }
    private void ClearModel(List<Model> models)
    {
        foreach (Model remainModel in models)
        {
            bool isCamera = remainModel is CameraController;
            if (!isCamera)
            {
                if (remainModel is Pin)
                {
                    Pin pin = (Pin)remainModel;
                    DestroyImmediate(pin.MainTransform.gameObject);
                }
                else
                {
                    DestroyImmediate(remainModel.gameObject);
                }
            }
        }
    }
}
