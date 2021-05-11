using UnityEngine;
using UnityEngine.UI;
public class UIGameOver : UISection
{
    public Image characterIcon { get => (Image)displays[0].Value; }
    public void SkipLevel()
    {
        Debug.Log("Skip level");
        UIController.Instance.ShowWinUI(false);
        UIController.Instance.ShowGameOverUI(false);
    }
    public void WatchToEarn(int reward)
    {
        Debug.Log("reward");
    }
    public void Retry()
    {
        Debug.Log("restart level");
    }
}