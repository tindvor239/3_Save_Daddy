using UnityEngine;
using UnityEngine.UI;
public class UIChest : UIToggle
{
    [SerializeField]
    private Text prize;
    [SerializeField]
    private GameObject prizeBackground;
    #region Properties
    public long Prize
    {
        get
        {
            try
            {
                string[] splitedString = prize.text.Split(' ');
                string result = splitedString[1];
                return long.Parse(result);
            }
            catch
            {
                return 0;
            }
        }
        set
        {
            prize.text = string.Format("$ {0}", value);
        }
    }
    #endregion
    public void Open()
    {
        if(GameManager.CurrentKey > 0)
        {
            isCheck = true;
            GameManager.CurrentKey--;
            UIChestRoom.TopPrize += Prize;
            prizeBackground.SetActive(true);
        }
    }
    public void Restart(int randomPrize)
    {
        Prize = randomPrize;
        isCheck = false;
        prizeBackground.SetActive(false);
    }

    private void Start()
    {
        prizeBackground.SetActive(false);
    }
}
