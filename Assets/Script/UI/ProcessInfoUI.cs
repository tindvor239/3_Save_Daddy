using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProcessInfoUI : MonoBehaviour
{
    [SerializeField]
    private Text levelInfo;
    [SerializeField]
    private GameObject box;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Button button;
    [SerializeField]
    private GameObject levelProcessDisplayPrefab;
    [SerializeField]
    private List<LevelProcessDisplayUI> processDisplays = new List<LevelProcessDisplayUI>();
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
        box.SetActive(false);
        SetupProcess();
        slider.maxValue = 4;
    }

    private void DisplayProcess()
    {
        List<Map> maps = GetMapsInProcess();
        Debug.Log(maps.Count);
        for(int i = 0; i < maps.Count; i++)
        {
            if(maps[i].isUnlocked)
            {
                processDisplays[i].MainImage = mapComplete;
            }
            else if(maps.IndexOf(maps[i]) == 4)
            {
                processDisplays[i].MainImage = bossMap;
                processDisplays[i].transform.localScale = new Vector3(2, 2);
            }
            else
            {
                processDisplays[i].MainImage = mapUncomplete;
            }
            processDisplays[i].IsTick = maps[i].isUnlocked;
        }    
    }
    public void OnShowProcess()
    {
        int indexOfProcess = GetIndexOfProcess();
        box.SetActive(false);
        DisplayProcess();
        slider.value = indexOfProcess;
        button.onClick.RemoveAllListeners();
        if (indexOfProcess == 4)
        {
            buttonImage.sprite = bossButton;
            buttonText.text = "Fight Boss";
            button.onClick.AddListener(NextLevel);
        }
        else if(indexOfProcess == 0)
        {
            buttonImage.sprite = bossButton;
            box.SetActive(true);
            buttonText.text = "Open Box";
            button.onClick.AddListener(ChestRoom);
            slider.value = slider.maxValue;
        }
        else
        {
            buttonImage.sprite = normalButton;
            buttonText.text = "Next";
            button.onClick.AddListener(NextLevel);
        }
        winParticle.Play();
        levelInfo.text = UIController.Instance.Gameplay.LevelName;
    }

    private void SetupProcess()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject newObject = Instantiate(levelProcessDisplayPrefab, levelProcessDisplayParent.transform);
            LevelProcessDisplayUI levelProcessDisplay = newObject.GetComponent<LevelProcessDisplayUI>();
            processDisplays.Add(levelProcessDisplay);
        }
    }
    private void NextLevel()
    {
        UIController.Instance.Play(true);
    }
    private void ChestRoom()
    {
        UIController.Instance.ShowChestRoomUI(true);
    }
    private List<Map> GetMapsInProcess()
    {
        List<Map> result = new List<Map>();
        int startIndex = GetStartIndexOfProcess();
        if((startIndex + 1) % 5 == 0)
        {
            int startI = startIndex - 4;
            if(startIndex - 4 < 0)
            {
                startI = 0;
            }
            for (int i = startI; i <= startIndex; i++)
            {
                result.Add(GameManager.Instance.MapData[i]);
            }
        }
        else
        {
            int endI = startIndex + 5;
            if(endI > GameManager.Instance.MapData.Count - 1)
            {
                endI = GameManager.Instance.MapData.Count - 1;
            }
            for (int i = startIndex; i < endI; i++)
            {
                result.Add(GameManager.Instance.MapData[i]);
            }
        }
        return result;
    }
    private int GetIndexOfProcess()
    {
        int levelIndex = GameManager.CurrentLevelIndex();
        return levelIndex % 5;
    }
    private int GetStartIndexOfProcess()
    {
        int levelStack = GetProcessIndex();
        return GetCurrentLevelIndex() + 1 - levelStack;
    }
    private int GetProcessIndex()
    {
        int levelProcessIndex = GetCurrentLevelIndex() + 1;
        return levelProcessIndex / 5;
    }
    private int GetCurrentLevelIndex()
    {
        GameManager gameManager = GameManager.Instance;
        MapEditor mapEditor = MapEditor.Instance;
        return gameManager.MapData.IndexOf(mapEditor.currentMap);
    }
}
