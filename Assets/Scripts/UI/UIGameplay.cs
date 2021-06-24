using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameplay : UISection
{
    [SerializeField]
    private List<Sprite> sprites;
    [SerializeField]
    private AudioClip gameplayBackground;
    #region Properties
    public List<Image> Stars { get => GetStarImages(); }
    public List<Image> Keys { get => GetKeyImages(); }
    public Sprite KeyOn { get => sprites[0]; }
    public Sprite KeyOff { get => sprites[1]; }
    public Sprite StarOn { get => sprites[2]; }
    public Sprite StarOff { get => sprites[3]; }
    public AudioClip GameplayBackground { get => gameplayBackground; }
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
    public void OnShowGameplay(int keyCount)
    {
        int mapIndex = GameManager.Instance.CurrentLevelIndex();
        LevelName = string.Format("Level {0}", mapIndex);
        //Get correct keys
        Debug.Log(keyCount);
        SetKey(keyCount);
    }

    private void SetKey(int keyCount)
    {
        int count = 0;
        foreach (Image image in Keys)
        {
            if(count < keyCount)
            {
                image.sprite = KeyOn;
                count++;
            }
            else
            {
                image.sprite = KeyOff;
            }
        }
    }
    private List<Image> GetStarImages()
    {
        List<Image> images = new List<Image>();
        for (int index = 0; index < displays.Length / 2; index++)
        {
            images.Add((Image)displays[index].Value);
        }
        return images;
    }
    private List<Image> GetKeyImages()
    {
        List<Image> images = new List<Image>();
        for (int index = 3; index < 6; index++)
        {
            images.Add(displays[index].Value.GetComponent<Image>());
        }
        return images;
    }
}
