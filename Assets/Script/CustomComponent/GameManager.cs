﻿using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.CustomComponents;

public class GameManager : Singleton<GameManager>
{
    [Header("Movement")]
    [SerializeField]
    private List<Transform> destinations = new List<Transform>();
    [SerializeField]
    private List<EnemyController> enemies = new List<EnemyController>();
    private List<Transform> passedDestinations = new List<Transform>();
    [SerializeField]
    protected GameState gameState;

    [Header("Game Data")]
    [SerializeField]
    private PathPoolParty pathPoolParty;
    [SerializeField]
    private List<Map> mapDatas = new List<Map>();
    #region Properties
    #region Movement
    public List<Transform> Destinations { get => destinations; }
    public List<Transform> PassedDestinations { get => passedDestinations; }
    public List<EnemyController> Enemies { get => enemies; set => enemies = value; }
    public PlayerController Player { get; set; }
    #endregion
    #region Data
    public List<Map> MapData { get => mapDatas; }
    #endregion
    public static GameState State { get => Instance.gameState; set => Instance.gameState = value; }
    #endregion
    protected override void Awake()
    {
        #region Singleton
        base.Awake();
        #endregion
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
            Debug.Log("Hit");
            return hit.collider.gameObject;
        }
        return null;
    }
    public GameObject RayCastToObject(Vector3 fromPosition, Vector3 toPosition)
    {
        Vector3 direction = GetDirectionVector(fromPosition, toPosition);
        GameObject destinateObject = RayCastObject(fromPosition, direction, Vector3.Distance(fromPosition, toPosition));
        return destinateObject;
    }
    public GameObject RayCastToObject(Vector3 fromPosition, Vector3 toPosition, float length)
    {
        Vector3 direction = GetDirectionVector(fromPosition, toPosition);
        GameObject destinateObject = RayCastObject(fromPosition, direction, length);
        return destinateObject;
    }
    public GameObject RayCastToObject(Vector3 fromPosition, Vector3 toPosition, LayerMask layer)
    {
        Vector3 direction = GetDirectionVector(fromPosition, toPosition);
        GameObject destinateObject = RayCastObject(fromPosition, direction, Vector3.Distance(fromPosition, toPosition), layer);
        return destinateObject;
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
    public static int GetDestinationIndex(Transform transform, List<Transform> destinations)
    {
        for (int i = 0; i < destinations.Count; i++)
        {
            if (destinations[i] == transform)
            {
                return i;
            }
        }
        return -1;
    }
    public static int GetNextDestinationIndex(CharacterController controller)
    {
        int index = GetDestinationIndex(controller.transform, Instance.Destinations);
        controller.GetComponent<Collider2D>().enabled = false;
        if (index != -1 && index + 1 < Instance.destinations.Count)
        {
            for (int i = index + 1; i < Instance.destinations.Count; i++)
            {
                bool isBlocked = controller.CheckPathIsBlocked(Instance.destinations[index].position, Instance.destinations[i].position);
                if (!isBlocked)
                {
                    controller.GetComponent<Collider2D>().enabled = true;
                    return i;
                }
            }
            controller.GetComponent<Collider2D>().enabled = true;
            return -1;
        }
        else
        {
            controller.GetComponent<Collider2D>().enabled = true;
            return 0;
        }
    }
    
    public static EnemyController GetClosestEnemy()
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
    #endregion
    #region State Handle
    public static bool isWin()
    {
        if(Instance.destinations[Instance.destinations.Count - 1].GetComponent<CharacterController>() != null)
        {
            Instance.gameState = GameState.win;
            UnlockNextLevel();
            UIController.Instance.ShowWinUI(true);
            return true;
        }
        return false;
    }
    #endregion
    #region Map Handle
    private static void UnlockNextLevel()
    {
        for(int i = 0; i < Instance.mapDatas.Count; i++)
        {
            if(i + 1 < Instance.mapDatas.Count && MapEditor.Instance.currentMap != null
                && Instance.mapDatas[i] == MapEditor.Instance.currentMap)
            {
                Instance.mapDatas[i + 1].isUnlocked = true;
                break;
            }
        }
    }
    #endregion
    public enum GameState {menu, play, pause, win, gameover }
}
