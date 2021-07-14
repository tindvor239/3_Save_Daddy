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
            return ui.text;
        }
        private set
        {
            Text ui = (Text)displays[0].Value;
            ui.text = value;
        }
    }
    #endregion
    public void OnShowGameplay()
    {
        int mapIndex = GameManager.Instance.CurrentLevelIndex();
        LevelName = string.Format("Level {0}", mapIndex);
        //Get correct keys
        //Debug.Log("keys: "+ keyCount);
    }
}
