using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StackUIDisplay : MonoBehaviour
{
    [SerializeField]
    private Transform levels;
    [SerializeField]
    private GameObject levelUIPrefab;
    [SerializeField]
    private List<UILevelDisplay> uILevels;
    [Header("Key Chest")]
    [SerializeField]
    private List<UIKeyChest> keyChests;
    [SerializeField]
    private List<Sprite> sprites;
    [SerializeField]
    private Slider slider;
    
    public string Name
    {
        get => StackName(StackIndex());
    }
    private Sprite SetLevelImage(Map map)
    {
        Sprite result = null;
        UILevelManager levelManager = UIController.Instance.LevelManager;
        if (map != null)
        {
            if (map.isUnlocked)
            {
                result = levelManager.unlockColor;
            }
            else
            {
                result = levelManager.lockColor;
            }
        }
        else
        {
            result = levelManager.mapNullColor;
        }
        return result;
    }
    private void SpawnLevels(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            GameObject newObject = Instantiate(levelUIPrefab, levels);
            UILevelDisplay newLevel = newObject.GetComponent<UILevelDisplay>();

            if(newLevel != null)
            {
                uILevels.Add(newLevel);
            }
        }
    }
    private void AnimateChests()
    {
        int isSet = PlayerPrefs.GetInt(Name);
        if(isSet != 1)
        {
            ResetKeys();
            PlayerPrefs.SetInt(Name, 1);
        }
        foreach (UIKeyChest keyChest in keyChests)
        {
            keyChest.Initiate(sprites[0], sprites[1]);
            int index = keyChests.IndexOf(keyChest);
            if(GetKey(index) == 1)
            {
                if (index + 1 <= slider.value / 5)
                {
                    keyChest.GetComponent<UIAnimation>().Play();
                }
            }
            else
            {
                keyChests[index].isCheck = true;
            }
        }
    }

    private void SetKey(int index, int value)
    {
        PlayerPrefs.SetInt(Name + index, value);
    }
    private int GetKey(int index)
    {
        return PlayerPrefs.GetInt(Name + index);
    }
    
    private void ResetKeys()
    {
        for (int i = 0; i < 3; i++)
        {
            SetKey(i, 1);
        }
    }

    public void DisplayLevels(List<Map> maps, List<string> mapNames)
    {
        int stars = 0;
        for(int i = 0; i < uILevels.Count; i++)
        {
            Map currentMap = maps[i];
            string mapName = mapNames[i];
            uILevels[i].Initiate(currentMap, mapName, SetLevelImage(currentMap));
            stars += uILevels[i].Star;
        }
        slider.value = stars;
        AnimateChests();
    }
    public void Initiate(int amount)
    {
        SpawnLevels(amount);
    }
    public int StackIndex()
    {
        if(uILevels[0] != null)
        {
            return GameManager.Instance.MapData.IndexOf(uILevels[0].Map) / 5;
        }
        return -1;
    }
    public void Open(UIKeyChest uIKeyChest)
    {
        //check ui key chest index.
        int index = keyChests.IndexOf(uIKeyChest);
        int canOpenCount = ((int)slider.value / 5) - 1;
        if (index != -1 && canOpenCount >= index && GetKey(index) == 1)
        {
            // and star count reach ui key chest index => open chest.
            uIKeyChest.Open();
            SetKey(index, 0);
        }
    }

    public static string StackName(int index)
    {
        return string.Format("Stack {0}", index);
    }
    public static int MapInStack(Map map)
    {
        int index = GameManager.Instance.MapData.IndexOf(map);
        return index / 5;
    }
    public static int KeyCount(string name)
    {
        int count = 0;
        for(int i = 0; i < 3; i++)
        {
            int key = PlayerPrefs.GetInt(name + i);
            if(key != 1)
            {
                count++;
            }
        }
        return count;
    }
}
