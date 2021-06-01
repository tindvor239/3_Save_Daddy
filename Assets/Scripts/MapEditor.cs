using System.Linq;
using System.Collections.Generic;
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
            Clear();
            currentMap.Load(PoolParties);
            gameManager.GetDestinations();
        }
        else
        {
            Debug.Log("Don't have map");
        }
    }
    public void Clear()
    {
        List<PoolParty> poolParties = PoolParties;
        List<Model> models = FindObjectsOfType<Model>().ToList();
        List<Model> removeModels = new List<Model>();
        foreach(PoolParty poolParty in poolParties)
        {
            foreach(ObjectPool pool in poolParty.Pools)
            {
                foreach(GameObject gameObject in pool.PooledObjects)
                {
                    GetModelsBackToPool(ref models, ref removeModels, gameObject);
                }
            }
        }
        foreach(Model model in removeModels)
        {
            if(models.Contains(model))
            {
                models.Remove(model);
            }
        }
        foreach(Model remainModel in models)
        {
            bool isCamera = remainModel is CameraController;
            if(!isCamera)
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

    private static void GetModelsBackToPool(ref List<Model> models, ref List<Model> removeModels, GameObject gameObject)
    {
        foreach (Model model in models)
        {
            bool isCamera = model is CameraController;
            if (!isCamera && model.gameObject.activeInHierarchy && gameObject == model.gameObject)
            {
                if (model is Pin)
                {
                    Pin pin = (Pin)model;
                    pin.MainTransform.gameObject.SetActive(false);
                }
                else
                {
                    model.gameObject.SetActive(false);
                }
                removeModels.Add(model);
            }
        }
    }
}
