using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChestRoom : Singleton<UIChestRoom>
{
    [Header("Box Display")]
    [SerializeField]
    private Sprite openedBox;
    [SerializeField]
    private Sprite normalBox;
    [SerializeField]
    private GameObject boxPrefab;
    [SerializeField]
    private Transform boxParent;

    [Header("Key Display")]
    [SerializeField]
    private Sprite missingKey;
    [SerializeField]
    private Sprite key;
    [SerializeField]
    private List<UIToggle> keys = new List<UIToggle>();

    [Header("Money Display")]
    [SerializeField]
    private Text money;

    [Header("Prize")]
    [SerializeField]
    private Text topPrize;
    [SerializeField]
    private int minPrize, maxPrize;
    
    private List<UIChest> chests = new List<UIChest>();

    #region Properties
    public static long TopPrize
    {
        get
        {
            string number = Instance.topPrize.text;
            if(number != "")
            {
                string[] splitedText = Instance.topPrize.text.Split(' ');
                number = splitedText[1];
                return long.Parse(number);
            }
            else
            {
                return 0;
            }
        }
        set
        {
            string result;
            if(value != 0)
            {
                result = string.Format("$ {0}", value);
            }
            else
            {
                result = "";
            }
            Instance.topPrize.text = result;
        }
    }
    public static long Money
    {
        get
        {
            try
            {
                return long.Parse(Instance.money.text);
            }
            catch
            {
                return 0;
            }
        }
        set => Instance.money.text = string.Format("$ {0}", value);
    }
    #endregion
    protected override void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    void Start()
    {
        SetupBoxes();
        SetupKeys();
    }
    private void SetupBoxes()
    {
        for(int i = 0; i < 9; i++)
        {
            GameObject newGameObject = Instantiate(boxPrefab, boxParent);
            UIChest newChest = newGameObject.GetComponent<UIChest>();
            newChest.Initialize(openedBox, normalBox);
            chests.Add(newChest);
        }
    }
    private void SetupKeys()
    {
        foreach(UIToggle key in keys)
        {
            key.Initialize(this.key, missingKey);
        }
    }

    public static void SetKeys()
    {
        int currentKey = GameManager.CurrentKey;
        foreach(UIToggle key in Instance.keys)
        {
            if(currentKey > 0)
            {
                key.isCheck = true;
                currentKey--;
            }
            else
            {
                key.isCheck = false;
            }
        }
    }

    public void SetupPrize()
    {
        TopPrize = 0;
        foreach(UIChest chest in chests)
        {
            chest.Restart(Random.Range(minPrize, maxPrize));
        }
    }
}
