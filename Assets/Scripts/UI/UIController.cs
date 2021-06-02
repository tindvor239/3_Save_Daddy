using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.CustomComponents;
using DoozyUI;

public class UIController : Singleton<UIController>
{
    [SerializeField]
    private ProcessInfoUI processInfo;
    [SerializeField]
    private UIGameplay gameplay;
    [SerializeField]
    private UILevelManager levelManager;
    [SerializeField]
    private UILevelSkip levelSkip;
    [SerializeField]
    private UIMapProcessing mapProcessing;

    private GameManager gameManager;
    private CharacterPoolParty characterParty;
    private CameraController cam;
    private ObstaclePoolParty obstacleParty;
    private MapEditor editor;

    #region Properties
    public ProcessInfoUI ProcessInfo { get => processInfo; }
    public UIGameplay Gameplay { get => gameplay; }
    public UILevelManager LevelManager { get => levelManager; }
    #endregion
    protected override void Awake()
    {
        #region Singleton
        base.Awake();
        #endregion
    }
    private void Start()
    {
        gameManager = GameManager.Instance;
        characterParty = CharacterPoolParty.Instance;
        cam = CameraController.Instance;
        obstacleParty = ObstaclePoolParty.Instance;
        editor = MapEditor.Instance;
    }

    public void ShowMenuUI(bool isActive)
    {
        GameManager.State = GameManager.GameState.menu;
        ViewManager.ShowUI("MENU_UI", isActive);
    }
    public void ShowGameplayUI(bool isActive)
    {
        GameManager.State = GameManager.GameState.play;
        ViewManager.ShowUI("GAMEPLAY_UI", isActive);
    }

    public void Play(bool isActive)
    {
        ShowProcessUI(isActive);
        mapProcessing.Process();
        Load(null);
    }
    public void Retry()
    {
        GameManager.Instance.Player.state = CharacterController.CharacterState.idle;
        GameManager.Instance.Player.Stop();
        ShowGameOverUI(false);
        ShowProcessUI(true);
        mapProcessing.Process();
        GameManager.State = GameManager.GameState.play;
        Load(editor.currentMap);
    }
    public void Skip()
    {
        GameManager.Instance.UnlockNextLevel();
        bool isDoneShowAds = ShowAds();
        if(isDoneShowAds)
        {
            ShowLevelUpUI(true);
        }
    }
    public void Load(Map map)
    {
        if(map == null)
        {
            GetNextLevel();
        }
        editor.Load();
        StartCoroutine(LoadLevelOnTime());
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
        ViewManager.ShowUI("WIN_UI", isActive);
        if(isActive)
        {
            GameManager.State = GameManager.GameState.win;
            processInfo.OnShowProcess();
        }
    }
    public void ShowLevelUpUI(bool isActive)
    {
        GameManager.State = GameManager.GameState.pause;
        ViewManager.ShowUI("GAMEPLAY_UI", !isActive);
        ViewManager.ShowUI("LEVELUP_UI", isActive);
    }
    public void ShowLevelUI(bool isActive)
    {
        GameManager.State = GameManager.GameState.menu;
        ViewManager.ShowUI("GAMEPLAY_UI", !isActive);
        ViewManager.ShowUI("LEVELS_UI", isActive);
    }
    public void ShowChestRoomUI(bool isActive)
    {
        ViewManager.ShowUI("LUCKYSPIN_UI", isActive);
        ViewManager.ShowUI("WIN_UI", !isActive);
        if(!isActive == false)
        {
            Debug.Log("Show Chest Room");
        }
        ViewManager.ShowUI("GAMEPLAY_UI", !isActive);

        UIChestRoom.Instance.SetupPrize();
        UIChestRoom.SetKeys();
    }
    public void ShowProcessUI(bool isActive)
    {
        ViewManager.ShowUI("MENU_UI", false);
        ViewManager.ShowUI("WIN_UI", false);
        ViewManager.ShowUI("LEVELS_UI", false);
        ViewManager.ShowUI("LUCKYSPIN_UI", false);
        ViewManager.ShowUI("PROCESS_UI", isActive);
    }

    private IEnumerator LoadLevelOnTime()
    {
        while(editor.Process != 100 || editor.isDoneClear == false)
        {
            yield return null;
        }
        gameplay.OnShowGameplay();
        GetPlayerController();
        GetEnemyControllers();
        SpawnObstaclesOnPlay();
        Invoke("MovePlayer", 0.5f);
    }
    private bool ShowAds()
    {
        bool videoIsReady = true;
        bool videoIsDone = false;
        if(videoIsReady)
        {
            Debug.Log("Show ads");
            videoIsDone = true;
        }
        return videoIsDone;
    }
    private void MovePlayer()
    {
        //DON'T TOUCH!!!!
        gameManager.Player.MovePlayerToNextDestination();
    }
    private void GetEnemyControllers()
    {
        gameManager.Enemies = new List<EnemyController>();
        foreach (ObjectPool pool in characterParty.Party.Pools)
        {
            if(pool != characterParty.PlayerPool)
            {
                foreach(GameObject gameObject in pool.PooledObjects)
                {
                    if(gameObject != null && gameObject.activeInHierarchy)
                    {
                        gameManager.Enemies.Add(gameObject.GetComponent<EnemyController>());
                    }
                }
            }
        }
    }
    private void GetPlayerController()
    {
        gameManager.Player = characterParty.PlayerPool.PooledObjects[0].GetComponent<PlayerController>();
        PlayerController player = gameManager.Player;
        cam.Player = player;
        Debug.Log(player);
        Vector3 newPosition = new Vector3(player.transform.position.x, player.transform.position.y, cam.gameObject.transform.position.z);
        cam.gameObject.transform.position = newPosition;
        gameManager.Skins.AddRange(player.Skeleton.Skeleton.Data.Skins);
        ViewManager.SetSkin(player.Skeleton, gameManager.Skins[GameManager.UsingSkin]);
    }
    private void SpawnObstaclesOnPlay()
    {
        foreach (ObjectPool pool in obstacleParty.Party.Pools)
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
    private Map GetNextLevel()
    {
        for(int i = 0; i < gameManager.MapData.Count; i++)
        {
            if(!gameManager.MapData[i].isUnlocked && i - 1 >= 0)
            {
                editor.currentMap = gameManager.MapData[i - 1];
                return editor.currentMap;
            }
        }
        editor.currentMap = gameManager.MapData[gameManager.MapData.Count - 1];
        return editor.currentMap;
    }
}
