using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameplay : UISection
{
    [SerializeField]
    private AudioClip gameplayBackground;
    #region Properties
    public AudioClip GameplayBackground { get => gameplayBackground; }
    public string LevelName
    {
        get
        {
            Text ui = (Text)displays[0].Value;
            string[] splited = ui.text.Split(' ');
            return splited[1];
        }
        private set
        {
            Text ui = (Text)displays[0].Value;
            ui.text = "Level " + value;
        }
    }
    #endregion
    public void OnShowGameplay()
    {
        int mapIndex = GameManager.Instance.CurrentLevelIndex();
        LevelName = mapIndex.ToString();
        //Get correct keys
        //Debug.Log("keys: "+ keyCount);
    }
}
