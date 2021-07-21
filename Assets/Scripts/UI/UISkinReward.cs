using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class UISkinReward : MonoBehaviour
{
    [SerializeField]
    private UIAnimation animate;
    [SerializeField]
    private SkeletonGraphic graphic;
    [SerializeField]
    private Skin skin;
    #region Properties
    public UIAnimation Animate { get => animate; }
    public SkeletonGraphic Graphic { get => graphic; }
    #endregion

    public void ShowUI(Skin skin)
    {
        this.skin = skin;
        ViewManager.SetSkin(graphic.Skeleton, skin);
        animate.Play();
    }
    public void ShowUI(int index)
    {
        Skin skin = GameManager.PlayerSkins[index];
        ShowUI(skin);
    }    
    public void GetSkin()
    {
        if (skin != null)
        {
            Debug.Log(skin);
            skin.isSold = true;
            UIController.Instance.ShowSkinReward(false, 0);
        }
    }
}
