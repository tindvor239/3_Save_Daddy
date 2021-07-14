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
    public void DisplayLevels(List<Map> maps, List<string> mapNames)
    {
        for(int i = 0; i < uILevels.Count; i++)
        {
            Map currentMap = maps[i];
            string mapName = mapNames[i];
            uILevels[i].Initiate(currentMap, mapName, SetLevelImage(currentMap));
        }
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
