using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Movement")]
    [SerializeField]
    private List<Transform> destinations = new List<Transform>();
    [SerializeField]
    private List<CameraPath> cameraPaths = new List<CameraPath>();
    [SerializeField]
    private List<EnemyController> enemies = new List<EnemyController>();
    [SerializeField]
    protected GameState gameState;
    [Header("Game Data")]
    [SerializeField]
    private PathPoolParty pathPoolParty;
    [SerializeField]
    private List<Map> mapDatas = new List<Map>();
    [SerializeField]
    private List<Skin> playerSkins = new List<Skin>();
    [SerializeField]
    private Material laserMaterial;
    [SerializeField]
    private bool isVibration;
    private int winCount = 0;
    private float speed = 2;
    public bool startParalax = false;
    #region Properties
    #region Movement
    public List<Transform> Destinations { get => destinations; }
    public List<CameraPath> CameraPaths { get => cameraPaths; }
    public List<EnemyController> Enemies { get => enemies; set => enemies = value; }
    public PlayerController Player { get; set; }
    #endregion
    #region Data
    public List<Map> MapData { get => mapDatas; }
    public static List<Skin> PlayerSkins { get => Instance.playerSkins; }
    public static int UsingSkin
    {
        get => PlayerPrefs.GetInt("skinIndex");
        set => PlayerPrefs.SetInt("skinIndex", value);
    }
    public static bool IsVibration
    {
        get => PlayerPrefs.GetInt("isVibration") == 1 ? true : false;
        set
        {
            int volume = value == true ? 1 : 0;
            PlayerPrefs.SetInt("isVibration", volume);
        }
    }
    #endregion
    public static GameState State { get => Instance.gameState; set => Instance.gameState = value; }
    #endregion
    protected override void Awake()
    {
        #region Singleton
        base.Awake();
        #endregion
    }
    protected void Update()
    {
        if(laserMaterial != null && startParalax)
        {
            laserMaterial.mainTextureOffset -= new Vector2(Time.deltaTime * speed, 0);
            if(laserMaterial.mainTextureOffset.x <= -0.1f)
            {
                laserMaterial.mainTextureOffset = new Vector2(1, laserMaterial.mainTextureOffset.y);
            }
        }
    }
    #region Raycasting
    public GameObject RayCastObject(Vector3 fromPosition, Vector3 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(fromPosition, direction);
        if (hit.collider != null)
        {
            return hit.collider.gameObject;
        }
        return null;
    }
    public GameObject RayCastObject(Vector3 fromPosition, Vector3 direction, float length)
    {
        RaycastHit2D hit = Physics2D.Raycast(fromPosition, direction, length);
        if (hit.collider != null)
        {
            return hit.collider.gameObject;
        }
        return null;
    }
    public GameObject RayCastObject(Vector3 fromPosition, Vector3 direction, float length, LayerMask layer)
    {
        RaycastHit2D hit = Physics2D.Raycast(fromPosition, direction, length, layer);
        if (hit.collider != null)
        {
            return hit.collider.gameObject;
        }
        return null;
    }
    public GameObject RayCastToObject(Vector3 fromPosition, Vector3 toPosition)
    {
        Vector3 direction = Instance.GetDirectionVector(fromPosition, toPosition);
        GameObject destinateObject = RayCastObject(fromPosition, direction, Vector3.Distance(fromPosition, toPosition));
        return destinateObject;
    }
    public GameObject RayCastToObject(Vector3 fromPosition, Vector3 toPosition, float length)
    {
        Vector3 direction = Instance.GetDirectionVector(fromPosition, toPosition);
        GameObject destinateObject = RayCastObject(fromPosition, direction, length);
        return destinateObject;
    }
    public GameObject RayCastToObject(Vector3 fromPosition, Vector3 toPosition, LayerMask layer)
    {
        Vector3 direction = Instance.GetDirectionVector(fromPosition, toPosition);
        GameObject destinateObject = RayCastObject(fromPosition, direction, Vector3.Distance(fromPosition, toPosition), layer);
        return destinateObject;
    }

    public GameObject CircleCast(Vector3 origin, float radius, Vector3 direction)
    {
        RaycastHit2D hit = Physics2D.CircleCast(origin, radius, direction);
        if(hit.collider != null)
        {
            return hit.collider.gameObject;
        }
        return null;
    }
    public GameObject CircleCast(Vector3 origin, float radius, Vector3 direction, float distance, LayerMask layer)
    {
        RaycastHit2D hit = Physics2D.CircleCast(origin, radius, direction, distance, layer);
        if (hit.collider != null)
        {
            return hit.collider.gameObject;
        }
        return null;
    }
    public List<GameObject> CircleCastAll(Vector3 origin, float radius, Vector3 direction, float distance)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(origin, radius, direction, distance);
        List<GameObject> hitObjects = new List<GameObject>();
        foreach(RaycastHit2D hit in hits)
        {
            hitObjects.Add(hit.collider.gameObject);
        }
        return hitObjects;
    }
    public List<GameObject> CircleCastAll(Vector3 origin, float radius, Vector3 direction, float distance, LayerMask layer)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(origin, radius, direction, distance, layer);
        List<GameObject> hitObjects = new List<GameObject>();
        foreach (RaycastHit2D hit in hits)
        {
            hitObjects.Add(hit.collider.gameObject);
        }
        return hitObjects;
    }
    public List<GameObject> BoxCastAll(Vector3 origin, Vector2 size, float angle, Vector2 direction, float distance)
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(origin, size, angle, direction, distance);
        List<GameObject> hitObjects = new List<GameObject>();
        foreach (RaycastHit2D hit in hits)
        {
            hitObjects.Add(hit.collider.gameObject);
        }
        return hitObjects;
    }
    #endregion
    #region Linecasting
    public bool IsBlocked(Vector3 fromPosition, Vector3 toPosition)
    {
        RaycastHit2D hit = Physics2D.Linecast(fromPosition, toPosition);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject);
            if (hit.collider.transform.position == toPosition)
            {
                return false;
            }
            return true;
        }
        return false;
    }
    public bool IsBlocked(Vector3 fromPosition, Vector3 toPosition, int layerIndex)
    {
        RaycastHit2D hit = Physics2D.Linecast(fromPosition, toPosition, layerIndex);
        if (hit.collider != null)
        {
            if (hit.collider.transform.position == toPosition)
            {
                return false;
            }
            return true;
        }
        return false;
    }
    #endregion
    #region Path Handle
    public Vector3 GetDirectionVector(Vector3 gameObject, Vector3 target)
    {
        return target - gameObject;
    }
    public bool CheckPlayerInPath()
    {
        foreach(Transform transform in destinations)
        {
            if(transform.GetComponent<CharacterController>())
            {
                return true;
            }
        }
        return false;
    }
    public int GetCurrentPosIndex(Transform transform, List<Transform> destinations)
    {
        float roundedX = (float)Math.Round(transform.position.x, 2);
        float roundedY = (float)Math.Round(transform.position.y, 2);
        Vector2 roundedPos = new Vector2(roundedX, roundedY);
        int destinationIndex = -1;
        for (int i = 0; i < destinations.Count; i++)
        {
            Vector2 currentDestination = destinations[i].position;
            float roundedDesX = (float)Math.Round(currentDestination.x, 2);
            float roundedDesY = (float)Math.Round(currentDestination.y, 2);
            currentDestination = new Vector2(roundedDesX, roundedDesY);

            if (destinations[i] == transform || roundedPos == currentDestination)
            {
                //Debug.Log("current Pos: "+i);
                return i;
            }
            else if ((roundedPos.x == currentDestination.x && roundedPos.y != currentDestination.y && roundedPos.y > currentDestination.y)
                || (roundedPos.x != currentDestination.x && roundedPos.x > currentDestination.x && roundedPos.y == currentDestination.y))
            {
                destinationIndex = i;
            }
            else if (i - 1 >= 0)
            {
                Vector2 lastDestination = destinations[i - 1].position;
                float roundedLastDesX = (float)Math.Round(lastDestination.x, 2);
                float roundedLastDesY = (float)Math.Round(lastDestination.y, 2);
                lastDestination = new Vector2(roundedLastDesX, roundedLastDesY);
                bool isBetweenDestinationX = currentDestination.x >= transform.position.x &&
                    transform.position.x >= lastDestination.x;
                bool isBetwwenDestinationY = currentDestination.y >= transform.position.y &&
                    transform.position.y >= lastDestination.y;

                if (isBetweenDestinationX && isBetwwenDestinationY)
                {
                    destinationIndex = i - 1;
                }
            }
        }
        return destinationIndex;
    }
    public int GetNextDestinationIndex(CharacterController controller)
    {
        int index = GetCurrentPosIndex(controller.transform, Instance.Destinations);
        //Debug.Log("current pos: " + index);
        if (index != -1 && index < Instance.destinations.Count - 1)
        {
            for(int i = index + 1; i < destinations.Count; i++)
            {
                bool isBlocked = controller.CheckPathIsBlocked(controller.transform.position, Instance.destinations[i].position);
                if (!isBlocked)
                {
                    //Debug.Log("next destination: " + i);
                    return i;
                }
            }
            return -1;
        }
        else if(index == Instance.destinations.Count - 1)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
    public EnemyController GetClosestEnemy()
    {
        EnemyController closestEnemy = null;
        if(Instance.enemies.Count != 0 && Instance.enemies[0] != null)
        {
            float firstDistance = Vector2.Distance(Instance.Player.transform.position, Instance.enemies[0].transform.position);
            foreach(EnemyController enemy in Instance.enemies)
            {
                float distance = Vector2.Distance(Instance.Player.transform.position, enemy.transform.position);
                if (distance <= firstDistance)
                {
                    closestEnemy = enemy;
                }
            }
        }
        return closestEnemy;
    }
    public void GetDestinations()
    {
        Transform[] newDestinations = new Transform[pathPoolParty.PathPool.PooledObjects.Count];
        for (int i = 0; i < pathPoolParty.PathPool.PooledObjects.Count; i++)
        {
            if (pathPoolParty.PathPool.PooledObjects[i].activeInHierarchy)
            {
                int index = int.Parse(pathPoolParty.PathPool.PooledObjects[i].name) - 1;
                newDestinations[index] = pathPoolParty.PathPool.PooledObjects[i].transform;
            }
        }
        destinations = newDestinations.ToList();
        destinations.RemoveAll(x => x == null);
    }
    public void GetCameraPaths()
    {
        List<CameraPath> newCamPaths = new List<CameraPath>();
        List<GameObject> pooledObjects = pathPoolParty.CameraPathPool.PooledObjects;
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if(pooledObjects[i].activeInHierarchy)
            {
                newCamPaths.Add(pooledObjects[i].GetComponent<CameraPath>());
            }
        }
        cameraPaths = newCamPaths;
    }
    #endregion
    #region State Handle
    public bool isWin()
    {
        int playerIndex = GetCurrentPosIndex(Instance.Player.transform, Instance.destinations);
        if (playerIndex == Instance.destinations.Count - 1)
        {
            UnlockNextLevel();
            winCount = 6;
            if (winCount == 6)
            {
                ShowSkinOnWin();
            }
            else
            {
                UIController.Instance.ShowWinUI(true);
                winCount++;
            }
            int index = MapData.IndexOf(MapEditor.Instance.currentMap);
            ZenSDK.instance.TrackLevelCompleted(index + 1);
            SetMapStar(3);
            return true;
        }
        return false;
    }

    private void ShowSkinOnWin()
    {
        //Show Skin.
        int index = RandomUnSoldSkin();
        UIController.Instance.ShowSkinReward(true, index);
        Debug.Log(playerSkins[index]);
        winCount = 0;
    }
    private int RandomUnSoldSkin()
    {
        List<Skin> unSoldSkin = UnSoldSkins();
        int random = UnityEngine.Random.Range(0, unSoldSkin.Count);
        return playerSkins.IndexOf(unSoldSkin[random]);
    }
    private List<Skin> UnSoldSkins()
    {
        List<Skin> result = new List<Skin>();
        foreach(var skin in playerSkins)
        {
            if(skin.isSold == false)
            {
                result.Add(skin);
            }
        }
        return result;
    }
    #endregion
    #region Map Handle
    public void UnlockNextLevel()
    {
        int index = Instance.mapDatas.IndexOf(MapEditor.Instance.currentMap);
        if (index != -1 && index + 1 < Instance.mapDatas.Count)
        {
            Debug.Log("index: "+index);
            Instance.mapDatas[index + 1].isUnlocked = true;
        }
    }
    public int CurrentLevelIndex()
    {
        int result = Instance.mapDatas.IndexOf(MapEditor.Instance.currentMap) + 1;
        return result;
    }

    private void SetMapStar(int starAmount)
    {
        int mapIndex = mapDatas.IndexOf(MapEditor.Instance.currentMap);
        string mapName = (mapIndex + 1).ToString();
        PlayerPrefs.SetInt(mapName, starAmount);
    }
    #endregion
    #region Hit Handler
    public void OnHitCallBack(ref float timer, in float maxTime, Action action)
    {
        timer += Time.deltaTime;
        if(timer >= maxTime)
        {
            action();
        }
    }
    #endregion
    public enum GameState {menu, level, shop, play, pause, win, gameover }
}
