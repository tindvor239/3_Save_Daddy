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
}
