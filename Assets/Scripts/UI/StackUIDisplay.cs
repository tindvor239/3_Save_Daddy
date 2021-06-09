using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StackUIDisplay : MonoBehaviour
{
    private int keys;
    [SerializeField]
    private Transform levels;
    [SerializeField]
    private GameObject levelUIPrefab;
    [SerializeField]
    private List<UILevelDisplay> uILevels;
    [SerializeField]
    private Slider slider;
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
        int stars = 0;
        for(int i = 0; i < uILevels.Count; i++)
        {
            Map currentMap = maps[i];
            string mapName = mapNames[i];
            uILevels[i].Initiate(currentMap, mapName, SetLevelImage(currentMap));
            stars += uILevels[i].Star;
        }
        keys = stars / 5;
        slider.value = stars;
    }
    public void Initiate(int amount)
    {
        SpawnLevels(amount);
    }
}
