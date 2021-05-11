using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        }
        ViewManager.ShowUI("MENU_UI", false);
        ViewManager.ShowUI("GAMEPLAY_UI", isActive);
    }
    public void ShowSettingUI(bool isActive)
    {
        GameManager.State = GameManager.GameState.pause;
        ViewManager.ShowUI("GAMEPLAY_UI", false);
        ViewManager.ShowUI("SETTING_UI", isActive);
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
        return null;
    }
}
