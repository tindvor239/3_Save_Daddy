using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.CustomComponents;
using DoozyUI;

public class UIController : Singleton<UIController>
{
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
    public void ShowMenuUI(bool isActive)
    {
        GameManager.State = GameManager.GameState.menu;
        ViewManager.ShowUI("MENU_UI", isActive);
    }
    public void ShowGamePlayUI(bool isActive)
    {
        GameManager.State = GameManager.GameState.play;
        Map currentMap = GetNextLevel();
        if (currentMap != null)
        {
            MapEditor.Instance.Load();
            GetPlayerController();
            GetEnemyControllers();
            SpawnObstaclesOnPlay();
            Invoke("MovePlayer", 0.5f);
        }
        ViewManager.ShowUI("MENU_UI", false);
        ViewManager.ShowUI("WIN_UI", false);
        ViewManager.ShowUI("GAMEPLAY_UI", isActive);
    }
    private void MovePlayer()
    {
        //DON'T TOUCH!!!!
        GameManager.Instance.Player.MovePlayerToNextDestination();
    }
    private void GetEnemyControllers()
    {
        GameManager.Instance.Enemies = new List<EnemyController>();
        foreach (ObjectPool pool in CharacterPoolParty.Instance.Party.Pools)
        {
            if(pool != CharacterPoolParty.Instance.PlayerPool)
            {
                foreach(GameObject gameObject in pool.PooledObjects)
                {
                    if(gameObject.activeInHierarchy)
                    {
                        GameManager.Instance.Enemies.Add(gameObject.GetComponent<EnemyController>());
                    }
                }
            }
        }
    }
    private void GetPlayerController()
    {
        GameManager.Instance.Player = CharacterPoolParty.Instance.PlayerPool.PooledObjects[0].GetComponent<PlayerController>();
        CameraController.Instance.Player = GameManager.Instance.Player;
    }
    private void SpawnObstaclesOnPlay()
    {
        foreach (ObjectPool pool in ObstaclePoolParty.Instance.Party.Pools)
        {
            foreach(GameObject gameObject in pool.PooledObjects)
            {
                if(gameObject.GetComponent<ObstaclePool>() != null && gameObject.activeInHierarchy)
                {
                    gameObject.GetComponent<ObstaclePool>().SpawnObstaclesOnLoad();
                }
            }
        }
    }
    public void ShowSettingUI(bool isActive)
    {
        if(isActive)
        {
            GameManager.State = GameManager.GameState.pause;
        }
        else
        {
            GameManager.State = GameManager.GameState.play;
        }
        ViewManager.ShowUI("GAMEPLAY_UI", !isActive);
        ViewManager.ShowUI("SETTINGS_UI", isActive);
    }
    public void ShowGameOverUI(bool isActive)
    {
        GameManager.State = GameManager.GameState.gameover;
        ViewManager.ShowUI("GAMEOVER_UI", isActive);
    }
    public void ShowWinUI(bool isActive)
    {
        GameManager.State = GameManager.GameState.win;
        ViewManager.ShowUI("WIN_UI", isActive);
    }
    private Map GetNextLevel()
    {
        for(int i = 0; i < GameManager.Instance.MapData.Count; i++)
        {
            if(!GameManager.Instance.MapData[i].isUnlocked && i - 1 >= 0)
            {
                MapEditor.Instance.currentMap = GameManager.Instance.MapData[i - 1];
                return MapEditor.Instance.currentMap;
            }
        }
        MapEditor.Instance.currentMap = GameManager.Instance.MapData[GameManager.Instance.MapData.Count - 1];
        return MapEditor.Instance.currentMap;
    }
}
