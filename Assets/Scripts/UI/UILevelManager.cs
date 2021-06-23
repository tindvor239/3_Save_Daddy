using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelManager : MonoBehaviour
{
    [SerializeField]
    private Transform levelContent;
    [SerializeField]
    private GameObject levelStackPrefab;
    [SerializeField]
    private Text page;
    [SerializeField]
    private List<Sprite> lockImages;
    [SerializeField]
    private List<StackUIDisplay> levelStacks = new List<StackUIDisplay>();

    [SerializeField]
    private int stackPerPage;
    [SerializeField]
    private int mapPerStack;

    #region Properties
    public List<StackUIDisplay> LevelStacks { get => levelStacks; }
    public int Page
    {
        get
        {
            string[] pageDisplay = page.text.Split(' ');
            return int.Parse(pageDisplay[2]) - 1;
        }
        private set => page.text = string.Format("Page - {0}", value + 1);
    }
    public Sprite lockColor { get => lockImages[0]; }
    public Sprite unlockColor { get => lockImages[1]; }
    public Sprite mapNullColor { get => lockImages[2]; }
    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        Page = 0;
        InitiateStacks();
    }

    private void InitiateStacks()
    {
        for(int i = 0; i < stackPerPage; i++)
        {
            GameObject newObject = Instantiate(levelStackPrefab, levelContent);
            StackUIDisplay newStack = newObject.GetComponent<StackUIDisplay>();
            levelStacks.Add(newStack);
            newStack.Initiate(mapPerStack);
        }
    }
    private void DisplayStacks(int startIndex)
    {
        foreach (StackUIDisplay stackUI in levelStacks)
        {
            if(stackUI != null)
            {
                List<Map> maps = new List<Map>();
                List<string> names = new List<string>();
                GetMapStack(ref startIndex, mapPerStack, maps, names);
                Debug.Log(startIndex);
                stackUI.DisplayLevels(maps, names);
            }
        }
    }
    private void GetMapStack(ref int startIndex, int length, List<Map> result, List<string> mapName)
    {
        //int mapsPerPage = mapPerStack * stackPerPage;
        List<Map> maps = GameManager.Instance.MapData;
        for (int i = 0; i < length; i++)
        {
            Map map = null;
            int remainMaps = GameManager.Instance.MapData.Count - startIndex;
            if(remainMaps > 0)
            {
                map = maps[startIndex];
            }
            result.Add(map);
            mapName.Add((startIndex + 1).ToString());
            startIndex++;
        }
    }
    private int MaxPage()
    {
        List<Map> maps = GameManager.Instance.MapData;
        return maps.Count / (stackPerPage * mapPerStack);
    }
    private int GetMapStartIndex()
    {
        return Page * mapPerStack;
    }
    
    public void SetupLevels()
    {
        int startIndex = GetMapStartIndex();
        DisplayStacks(startIndex);
    }
    public void TurnPage(int side)
    {
        Page += side;
        Page = Mathf.Clamp(Page, 0, MaxPage());
        Debug.Log(Page);
        SetupLevels();
    }
}
