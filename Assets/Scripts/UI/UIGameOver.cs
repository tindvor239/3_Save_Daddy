using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
public class UIGameOver : UISection
{
    [SerializeField]
    private AudioClip loseSound;
    #region Properties
    public Image characterIcon { get => (Image)displays[0].Value; }
    public AudioClip LoseSound { get => loseSound; }
    #endregion
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