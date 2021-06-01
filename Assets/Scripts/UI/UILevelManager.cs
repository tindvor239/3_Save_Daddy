using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelManager : MonoBehaviour
{
    [SerializeField]
    private Transform levelContent;
    [SerializeField]
    private GameObject levelDisplayPrefab;
    [SerializeField]
    private Text page;
    [SerializeField]
    private List<Color> lockColors;
    private List<UILevelDisplay> levelDisplays = new List<UILevelDisplay>();

    [SerializeField]
    private int mapPerPage;

    #region Properties
    public int Page
    {
        get
        {
            string[] pageDisplay = page.text.Split(' ');
            return int.Parse(pageDisplay[2]) - 1;
        }
        private set => page.text = string.Format("Page - {0}", value + 1);
    }
    public Color unlockColor { get => lockColors[1]; }
    public Color lockColor { get => lockColors[0]; }
    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        Page = 0;
        SpawnLevelTiles();
    }

    private void SpawnLevelTiles()
    {
        List<Map> maps = GameManager.Instance.MapData;
        int mapsPages = GetPageIndex(maps.Count);
        for(int i = GetMapStartIndex(); i < mapPerPage; i++)
        {
            GameObject newObject = Instantiate(levelDisplayPrefab, levelContent);
            UILevelDisplay levelDisplay = newObject.GetComponent<UILevelDisplay>();
            if(i < maps.Count)
            {
                levelDisplay.Initialize(maps[i], (i + 1).ToString(), SetLevelColor(maps[i]));
            }
            levelDisplays.Add(levelDisplay);
        }
    }
    private void SetupLevels()
    {
        List<Map> maps = GameManager.Instance.MapData;
        int startIndex = GetMapStartIndex();
        for (int i = startIndex; i < startIndex + mapPerPage; i++)
        {
            levelDisplays[GetPageIndex(i)].Initialize(maps[i], i.ToString(), SetLevelColor(maps[i]));
        }
    }
    private int GetMapStartIndex()
    {
        return Page * mapPerPage;
    }
    private int GetPageIndex(int mapIndex)
    {
        return mapIndex / mapPerPage;
    }
    private Color SetLevelColor(Map map)
    {
        Color result = Color.red;
        if (map.isUnlocked)
        {
            result = unlockColor;
        }
        else
        {
            result = lockColor;
        }
        return result;
    }

    public void TurnPage(int side)
    {
        Page += side;
        SetupLevels();
    }
}
