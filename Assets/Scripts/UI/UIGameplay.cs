﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameplay : UISection
{
    [SerializeField]
    private List<Sprite> sprites;

    #region Properties
    public List<Image> Stars { get => GetStars(); }
    public List<Image> Keys { get => GetKeys(); }
    public Sprite StarOn { get => sprites[0]; }
    public Sprite StarOff { get => sprites[1]; }
    public Sprite KeyOn { get => sprites[2]; }
    public Sprite KeyOff { get => sprites[3]; }
    public string LevelName
    {
        get
        {
            Text ui = (Text)displays[6].Value;
            return ui.text;
        }
        private set
        {
            Text ui = (Text)displays[6].Value;
            ui.text = value;
        }
    }
    #endregion
    public void OnShowGameplay()
    {
        int mapIndex = GameManager.Instance.CurrentLevelIndex();
        LevelName = string.Format("Level {0}", mapIndex);
    }
    private List<Image> GetStars()
    {
        List<Image> images = new List<Image>();
        for (int index = 0; index < displays.Length / 2; index++)
        {
            images.Add((Image)displays[index].Value);
        }
        return images;
    }
    private List<Image> GetKeys()
    {
        List<Image> images = new List<Image>();
        for (int index = 3; index < displays.Length; index++)
        {
            images.Add((Image)displays[index].Value);
        }
        return images;
    }
}