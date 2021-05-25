using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProcessInfoUI : MonoBehaviour
{
    [SerializeField]
    private Text levelInfo;
    [SerializeField]
    private Button box;
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
    [SerializeField]
    private ParticleSystem winParticle;
    private Image buttonImage;
    private Text buttonText;
    private void Start()
    {
        buttonImage = button.GetComponent<Image>();
        buttonText = button.GetComponentInChildren<Text>();
        box.gameObject.SetActive(false);
        slider.maxValue = 5;
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
            box.gameObject.SetActive(false);
            slider.value = indexOfProcess + 1;
        }
        else if(indexOfProcess == 0)
        {
            buttonImage.sprite = bossButton;
            box.gameObject.SetActive(true);
            buttonText.text = "Open Box";
            slider.value = 5;
        }
        else
        {
            buttonImage.sprite = normalButton;
            buttonText.text = "Next";
            box.gameObject.SetActive(false);
            slider.value = indexOfProcess + 1;
        }
        winParticle.Play();
        levelInfo.text = UIController.Instance.Gameplay.LevelName;
    }

    private List<Map> GetMapsInProcess()
    {
        List<Map> result = new List<Map>();
        int startIndex = GetStartIndexOfProcess();
        if((startIndex + 1) % 5 == 0)
        {
            for (int i = startIndex - 5; i <= startIndex; i++)
            {
                result.Add(GameManager.Instance.MapData[i]);
            }
        }
        else
        {
            for (int i = startIndex; i < startIndex + 5; i++)
            {
                result.Add(GameManager.Instance.MapData[i]);
            }
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
