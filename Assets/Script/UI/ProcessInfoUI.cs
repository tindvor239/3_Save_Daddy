using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProcessInfoUI : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Button button;
    [SerializeField]
    private GameObject levelProcessDisplayPrefab;
    [SerializeField]
    private Sprite mapComplete, mapUncomplete, bossMap, tick, bossButton, normalButton;
    [SerializeField]
    private GameObject levelProcessDisplayParent;

    private Image buttonImage;
    private Text buttonText;
    private void Start()
    {
        buttonImage = button.GetComponent<Image>();
        buttonText = button.GetComponentInChildren<Text>();
    }

    public void DisplayProcess()
    {
        List<Map> maps = GetMapsInProcess();
        foreach(Map map in maps)
        {
            GameObject newObject = Instantiate(levelProcessDisplayPrefab, levelProcessDisplayParent.transform);
            LevelProcessDisplayUI levelProcessDisplay = newObject.GetComponent<LevelProcessDisplayUI>();
            if(map.isUnlocked)
            {
                levelProcessDisplay.MainImage = mapComplete;
            }
            else if(maps.IndexOf(map) == 4)
            {
                levelProcessDisplay.MainImage = bossMap;
                levelProcessDisplay.transform.localScale = new Vector3(2, 2);
            }
            else
            {
                levelProcessDisplay.MainImage = mapUncomplete;
            }
            levelProcessDisplay.IsTick = map.isUnlocked;
        }    
    }

    public void ShowProcess()
    {
        int indexOfProcess = GetIndexOfProcess();
        if(indexOfProcess == 4)
        {
            buttonImage.sprite = bossButton;
            buttonText.text = "Fight Boss";
        }
        else if(indexOfProcess == 5)
        {
            buttonImage.sprite = bossButton;
            buttonText.text = "Open Box";
        }
        else
        {
            buttonImage.sprite = normalButton;
            buttonText.text = "Next";
        }
        slider.value = indexOfProcess;
    }

    private List<Map> GetMapsInProcess()
    {
        List<Map> result = new List<Map>();
        int startIndex = GetStartIndexOfProcess();
        for (int i = startIndex; i < startIndex + 5; i++)
        {
            result.Add(GameManager.Instance.MapData[i]);
        }
        return result;
    }
    private int GetIndexOfProcess()
    {
        int levelProcessIndex = GetCurrentLevelIndex();
        return levelProcessIndex % 5;
    }
    private int GetStartIndexOfProcess()
    {
        int levelStack = GetProcessIndex();
        return levelStack * 5;
    }
    private int GetProcessIndex()
    {
        int levelProcessIndex = GetCurrentLevelIndex();
        return levelProcessIndex / 5;
    }
    private int GetCurrentLevelIndex()
    {
        GameManager gameManager = GameManager.Instance;
        MapEditor mapEditor = MapEditor.Instance;
        return gameManager.MapData.IndexOf(mapEditor.currentMap);
    }
}
