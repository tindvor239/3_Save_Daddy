using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoozyUI;

public class UIController : Singleton<UIController>
{
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject play;
    [SerializeField]
    private GameObject win;
    [SerializeField]
    private GameObject gameOver;
    #region Singleton
    protected override void Awake()
    {
        base.Awake();
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowUI(GameObject gameObject)
    {
        if(gameObject == menu)
        {
            GameManager.State = GameManager.GameState.menu;
            ViewManager.ShowUI("MENU_UI", true);
            ViewManager.ShowUI("GAMEPLAY_UI", false);
        }
        else if(gameObject == play)
        {
            GameManager.State = GameManager.GameState.play;
            Map currentMap = GetNextLevel();
            if(currentMap != null)
            {
                MapEditor.Instance.Load();
            }
            ViewManager.ShowUI("GAMEPLAY_UI", true);
            ViewManager.ShowUI("MENU_UI", false);
        }
        else if(gameObject == win)
        {
            GameManager.State = GameManager.GameState.win;
        }
        else if(gameObject == gameOver)
        {
            GameManager.State = GameManager.GameState.gameover;
        }
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
