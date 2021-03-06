using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.CustomComponents;
using DoozyUI;

public class UIController : Singleton<UIController>
{
    [SerializeField]
    private WinInfoUI winInfo;
    [SerializeField]
    private UIGameOver gameOver;
    [SerializeField]
    private UIGameplay gameplay;
    [SerializeField]
    private UILevelManager levelManager;
    [SerializeField]
    private UILevelSkip levelSkip;
    [SerializeField]
    private UIMapProcessing mapProcessing;
    [SerializeField]
    private UISkinReward skinReward;
    [SerializeField]
    private Sound sound;
    [SerializeField]
    private GameObject skipButton;
    private GameManager gameManager;
    private CharacterPoolParty characterParty;
    private CameraController cam;
    private ObstaclePoolParty obstacleParty;
    private MapEditor editor;

    #region Properties
    public WinInfoUI ProcessInfo { get => winInfo; }
    public UIGameplay Gameplay { get => gameplay; }
    public UILevelManager LevelManager { get => levelManager; }
    public UIMapProcessing MapProcessing { get => mapProcessing; }
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
        sound.Initiate(gameObject, SoundManager.Instance.MusicSource);
        sound.source.loop = true;
        ShowMenuUI(true);
    }

    public void ShowShop(bool isActive)
    {
        if(isActive)
        {
            GameManager.State = GameManager.GameState.shop;
            ViewManager.ShowUI("MAINBACKGROUND_UI", isActive);
        }
        ViewManager.ShowUI("SHOP_UI", isActive);
        ShowMenuUI(!isActive);
    }
    public void ShowMenuUI(bool isActive)
    {
        if(isActive)
        {
            GameManager.State = GameManager.GameState.menu;
            sound.source.clip = sound.clip;
            sound.source.Play();
            ViewManager.ShowUI("MAINBACKGROUND_UI", isActive);
        }
        ViewManager.ShowUI("MENU_UI", isActive);
    }
    public void ShowGameplayUI(bool isActive)
    {
        GameManager.State = GameManager.GameState.play;
        sound.source.clip = gameplay.GameplayBackground;
        sound.source.Play();
        ViewManager.ShowUI("GAMEPLAY_UI", isActive);
    }
    public void Play(bool isActive)
    {
        ShowProcessUI(isActive);
        Load(null);
    }
    public void Retry()
    {
        if (GameManager.State == GameManager.GameState.play || GameManager.State == GameManager.GameState.gameover)
        {
            if(GameManager.Instance.Player != null)
            {
                GameManager.Instance.Player.state = CharacterController.CharacterState.idle;
                GameManager.Instance.Player.Stop();
            }
            ShowGameOverUI(false);
            ShowProcessUI(true);
            GameManager.State = GameManager.GameState.pause;
            Load(editor.currentMap);
        }
    }
    public void ShowVideoSkip()
    {
        ShowViewReward(Skip);
    }
    public void Share()
    {
        ZenSDK.instance.Share();
    }
    public void Rate()
    {
        ZenSDK.instance.Rate();
    }
    public void ShowViewReward(UnityEngine.Events.UnityAction action)
    {
        ZenSDK.instance.ShowVideoReward((bool onSuccess) => {
            if(onSuccess)
            {
                action();
            }
            else
            {
            }
        });
    }
    private void Skip()
    {
        GameManager.Instance.UnlockNextLevel();
        bool isDoneShowAds = ShowAds();
        if(isDoneShowAds)
        {
            ShowLevelUpUI(true);
            Load(GetNextMap());
            ShowProcessUI(false);
        }
    }
    public void Load(Map map)
    {
        if(map == null)
        {
            ContinuePlay();
        }
        int index = GameManager.Instance.CurrentLevelIndex();
        editor.Load();
        mapProcessing.Process();
        StartCoroutine(LoadLevelOnTime());
        ZenSDK.instance.TrackLevelCompleted(index);
    }
    public void ShowSettingUI(bool isActive)
    {
        if(GameManager.State != GameManager.GameState.win && GameManager.State != GameManager.GameState.gameover)
        {
            if(isActive)
            {
                GameManager.State = GameManager.GameState.pause;
                sound.source.Pause();
                SoundManager.Instance.StopAllSound();
                gameManager.Player.Pause();
                foreach (var enemy in gameManager.Enemies)
                {
                    enemy.Pause();
                }
            }
            else
            {
                GameManager.State = GameManager.GameState.play;
                sound.source.Play();
                SoundManager.Instance.Continue();
                gameManager.Player.Continue();
                foreach (var enemy in gameManager.Enemies)
                {
                    enemy.Continue();
                }
            }
            ViewManager.ShowUI("GAMEPLAY_UI", !isActive);
            ViewManager.ShowUI("SETTINGS_UI", isActive);
        }    
    }
    public void ShowGameOverUI(bool isActive)
    {
        GameManager.State = GameManager.GameState.gameover;
        ViewManager.ShowUI("GAMEOVER_UI", isActive);
        if(isActive)
        {
            sound.PlayOnce(gameOver.LoseSound);
            StartCoroutine(ShowVideoReward());
        }
    }
    public void ShowTutorialUI(bool isActive)
    {
        ViewManager.ShowUI("TUTORIAL_UI", isActive);
    }
    public void ShowWinUI(bool isActive)
    {
        ViewManager.ShowUI("WIN_UI", isActive);
        if(isActive)
        {
            GameManager.State = GameManager.GameState.win;
            sound.source.PlayOneShot(winInfo.WinSound);
            winInfo.DisplayProcessUI();
            StartCoroutine(ShowVideoReward());
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
        if(isActive)
        {
            GameManager.State = GameManager.GameState.level;
        }
        ViewManager.ShowUI("GAMEPLAY_UI", false);
        ViewManager.ShowUI("MENU_UI", !isActive);
        ViewManager.ShowUI("LEVELS_UI", isActive);
        levelManager.SetupLevels();
    }
    public void ShowChestRoomUI(bool isActive)
    {
        ViewManager.ShowUI("LUCKYSPIN_UI", isActive);
        ViewManager.ShowUI("WIN_UI", !isActive);
        ViewManager.ShowUI("MENU_UI", false);
        if(!isActive == false)
        {
            Debug.Log("Show Chest Room");
        }
        ViewManager.ShowUI("GAMEPLAY_UI", !isActive);
    }
    public void ShowProcessUI(bool isActive)
    {
        ViewManager.ShowUI("MENU_UI", false);
        ViewManager.ShowUI("WIN_UI", false);
        ViewManager.ShowUI("LEVELS_UI", false);
        ViewManager.ShowUI("LUCKYSPIN_UI", false);
        ViewManager.ShowUI("PROCESS_UI", isActive);
        ViewManager.ShowUI("MAINBACKGROUND_UI", isActive);
    }
    public void ShowBeforeExitUI(bool isActive)
    {
        // Avoid Button Overlaying
        ViewManager.ShowUI("BEFOREEXIT_UI", isActive);
        switch(GameManager.State)
        {
            case GameManager.GameState.pause:
                ViewManager.ShowUI("SETTINGS_UI", !isActive);
                break;
            case GameManager.GameState.menu:
                List<UIElement> uiElements = UIElement.GetUIElements("MENU_UI");
                foreach (UIElement uiElement in uiElements)
                {
                    uiElement.GetComponent<GraphicRaycaster>().enabled = !isActive;
                }
                break;
        }
    }
    public void BeforeExit(bool isActive)
    {
        switch (GameManager.State)
        {
            case GameManager.GameState.pause:
                ViewManager.ShowUI("SETTINGS_UI", !isActive);
                ViewManager.ShowUI("BEFOREEXIT_UI", !isActive);
                if(isActive)
                {
                    ViewManager.ShowUI("MAINBACKGROUND_UI", isActive);
                    editor.Clear();
                    StartCoroutine(BackToMenu());
                }
                break;
            case GameManager.GameState.menu:
                ShowBeforeExitUI(isActive);
                break;
        }
    }
    private void ShowSkinReward(bool isActive, Skin skin)
    {
        ShowSkinReward(isActive);
        if (isActive)
        {
            skinReward.ShowUI(skin);
        }
    }
    private void ShowSkinReward(bool isActive)
    {
        ViewManager.ShowUI("VIDEO_REWARD_UI", isActive);
        if(isActive == false)
        {
            ShowWinUI(!isActive);
        }
    }
    public void ShowSkinReward(bool isActive, int index)
    {
        UnityEngine.Events.UnityAction action = () =>
        {
            ShowSkinReward(isActive, GameManager.PlayerSkins[index]);
        };
        ShowViewReward(action);
    }

    private IEnumerator BackToMenu()
    {
        while(editor.isDoneClear == false)
        {
            yield return new WaitForEndOfFrame();
        }
        ShowMenuUI(true);
    }
    private IEnumerator ShowVideoReward()
    {
        yield return new WaitForSeconds(2f);
        ZenSDK.instance.ShowFullScreen();
    }
    private IEnumerator LoadLevelOnTime()
    {
        while(editor.Process != 100 || editor.isDoneClear == false)
        {
            yield return null;
        }
        ViewManager.ShowUI("LEVELUP_UI", false);
        gameplay.OnShowGameplay();
        GetPlayerController();
        GetEnemyControllers();
        SoundManager.Instance.Continue();
        StartCoroutine(SpawnObstaclesOnPlay());
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
        if(characterParty.PlayerPool.PooledObjects[0] == null)
        {
            characterParty.PlayerPool.PooledObjects[0] = characterParty.PlayerPool.CreatePooledObject();
        }
        gameManager.Player = characterParty.PlayerPool.PooledObjects[0].GetComponent<PlayerController>();
        PlayerController player = gameManager.Player;
        cam.Player = player;
        Vector3 newPosition = new Vector3(player.transform.position.x, player.transform.position.y, cam.gameObject.transform.position.z);
        cam.gameObject.transform.position = newPosition;
        cam.Move();;
        ViewManager.SetSkin(player.SkeletonAnimation.skeleton, GameManager.PlayerSkins[GameManager.UsingSkin]);
    }
    private IEnumerator SpawnObstaclesOnPlay()
    {
        while(editor.Process < 100)
        {
            yield return null;
        }
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
    private Map ContinuePlay()
    {
        for(int i = 0; i < gameManager.MapData.Count; i++)
        {
            if(gameManager.MapData[i] != null && !gameManager.MapData[i].isUnlocked && i - 1 >= 0)
            {
                editor.currentMap = gameManager.MapData[i - 1];
                return editor.currentMap;
            }
        }
        editor.currentMap = gameManager.MapData[gameManager.MapData.Count - 1];
        return editor.currentMap;
    }
    private Map GetNextMap()
    {
        int index = gameManager.MapData.IndexOf(editor.currentMap);
        if (index + 1 < gameManager.MapData.Count)
        {
            editor.currentMap = gameManager.MapData[index + 1];
        }
        return editor.currentMap;
    }
}
