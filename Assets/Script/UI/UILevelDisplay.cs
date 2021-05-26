using UnityEngine;
using UnityEngine.UI;

public class UILevelDisplay : MonoBehaviour
{
    [SerializeField]
    private MaskableGraphic levelDisplay;
    [SerializeField]
    private Text levelName;
    [SerializeField]
    private int star;
    [SerializeField]
    private Map map;

    public void Initialize(Map map, string levelName, Color color)
    {
        this.map = map;
        this.levelName.text = levelName;
        star = PlayerPrefs.GetInt(levelName);

        gameObject.GetComponent<Image>().color = color;
    }
    public void Load()
    {
        MapEditor.Instance.currentMap = map;
        if(map.isUnlocked)
        {
            UIController.Instance.ShowGameplayUI(true);
            UIController.Instance.Load(map);
        }
    }
}
