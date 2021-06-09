using UnityEngine;
using UnityEngine.UI;

public class UILevelDisplay : MonoBehaviour
{
    [SerializeField]
    private MaskableGraphic levelDisplay;
    [SerializeField]
    private Text levelName;
    [SerializeField]
    private Sprite[] stars;
    [SerializeField]
    private UIToggle[] starDisplays;
    [SerializeField]
    private int star;
    [SerializeField]
    private Map map;
    #region Properties
    public int Star { get => star; }
    #endregion
    public void Initiate(Map map, string levelName, Sprite sprite)
    {
        this.map = map;
        this.levelName.text = levelName;

        if (map != null)
        {
            star = PlayerPrefs.GetInt(levelName);
        }
        SetUpStars();

        gameObject.GetComponent<Image>().sprite = sprite;
    }
    public void Load()
    {
        MapEditor.Instance.currentMap = map;
        if(map.isUnlocked)
        {
            UIController.Instance.ShowLevelUI(false);
            UIController.Instance.ShowProcessUI(true);
            UIController.Instance.MapProcessing.Process();
            UIController.Instance.Load(map);
        }
    }

    private void SetUpStars()
    {
        int count = 0;
        foreach (UIToggle toggle in starDisplays)
        {
            toggle.Initiate(stars[0], stars[1]);
            if (count < star)
            {
                toggle.isCheck = true;
                count++;
            }
            else
            {
                toggle.isCheck = false;
            }
        }
    }

}
