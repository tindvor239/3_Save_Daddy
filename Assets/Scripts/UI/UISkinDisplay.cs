using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
public class UISkinDisplay : MonoBehaviour
{
    [SerializeField]
    private Button button;
    [SerializeField]
    private Text buttonText;
    [SerializeField]
    private Image adsIcon;
    [SerializeField]
    private Skin skinData;
    [SerializeField]
    private SkeletonGraphic graphic;
    private static Color enabledColor = new Color(1, 1, 1);
    private static Color disableColor = new Color(0.15f, 0.15f, 0.15f);
    private void Start()
    {
        GetGraphic();
    }
    private void GetGraphic()
    {
        if(graphic == null)
        {
            graphic = GetComponent<SkeletonGraphic>();
        }
    }
    private void UnequipAll()
    {
        foreach(var skin in GameManager.PlayerSkins)
        {
            if(skin != null)
            {
                skin.isEquiped = false;
            }
        }
        foreach(var skinDisplay in UIShop.Instance.UISkins)
        {
            if(skinDisplay != null)
            {
                skinDisplay.SetButtonDisplay();
            }
        }
    }
    private void Equip()
    {
        if (skinData.isEquiped == false)
        {
            int index = GameManager.PlayerSkins.IndexOf(skinData);
            GameManager.UsingSkin = index;
            UnequipAll();
            skinData.isEquiped = true;
            SetButtonDisplay();
        }
    }

    public void SetButtonDisplay()
    {
        GetGraphic();
        if (skinData.isSold)
        {
            if (skinData.isEquiped)
            {
                button.image.sprite = UIShop.Instance.Icons[2];
                buttonText.text = "Using";
                UIShop.SetDisplay(skinData);
            }
            else
            {
                button.image.sprite = UIShop.Instance.Icons[0];
                buttonText.text = "Equip";
            }
            graphic.color = enabledColor;
            adsIcon.enabled = false;
        }
        else
        {
            button.image.sprite = UIShop.Instance.Icons[1];
            buttonText.text = "Lock";
            graphic.color = disableColor;
        }
    }
    public void Initiate(Skin skinData, List<Sprite> icons)
    {
        this.skinData = skinData;
        SetButtonDisplay();
        GetGraphic();
        ViewManager.SetSkin(graphic.Skeleton, skinData);
    }
    public void Active()
    {
        if(skinData.isSold)
        {
            Equip();
        }
    }
    
}
