using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
public class UIShop : Singleton<UIShop>
{
    [SerializeField]
    private GameObject skinDisplayPrefab;
    [SerializeField]
    private Transform display;
    [SerializeField]
    private SkeletonGraphic graphic;
    [SerializeField]
    List<UISkinDisplay> uISkins = new List<UISkinDisplay>();
    [SerializeField]
    private List<Sprite> icons;
    #region Properties
    public SkeletonGraphic Graphic { get => graphic; }
    public List<Sprite> Icons { get => icons; }
    public List<UISkinDisplay> UISkins { get => uISkins; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        Initiate();
    }
    private void Initiate()
    {
        foreach(var skin in GameManager.PlayerSkins)
        {
            GameObject newObject = Instantiate(skinDisplayPrefab, display);
            UISkinDisplay skinDisplay = newObject.GetComponent<UISkinDisplay>();
            uISkins.Add(skinDisplay);
            skinDisplay.Initiate(skin, icons);
        }
    }

    public static void SetDisplay(Skin skin)
    {
        ViewManager.SetSkin(Instance.Graphic.Skeleton, skin);
    }
}
