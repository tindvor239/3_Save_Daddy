using UnityEngine;
using UnityEngine.UI;

public class UILevelDisplay : MonoBehaviour
{
    [SerializeField]
    private MaskableGraphic levelDisplay;
    [SerializeField]
    private Text levelName;
    
    private Map map;
    #region Properties
    public Map Map { get => map; }
    #endregion
    public void Initiate(Map map, string levelName, Sprite sprite)
    {
        this.map = map;
        this.levelName.text = levelName;
        gameObject.GetComponent<Image>().sprite = sprite;
    }

    public void Load()
    {
        MapEditor.Instance.currentMap = map;
        if(map != null && map.isUnlocked)
        {
            UIController.Instance.ShowLevelUI(false);
            UIController.Instance.ShowProcessUI(true);
            UIController.Instance.MapProcessing.Process();
            UIController.Instance.Load(map);
        }
    }

}
