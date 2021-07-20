using System.Collections;
using System;
using UnityEngine;

public class TutorialHandler : Singleton<TutorialHandler>
{
    [SerializeField]
    private GameObject[] pinTutorials;
    [SerializeField]
    private GameObject movePin;
    [SerializeField]
    private GameObject releasePin;
    [SerializeField]
    private GameObject holderPin;
    [SerializeField]
    private GameObject rotatePin;
    [SerializeField]
    protected GameObject tutorialUI;

    protected override void Awake()
    {
        base.Awake();
    }
    public void ShowTutorial(bool active, int index)
    {
        if (index < 1)
        {
            UIController.Instance.ShowTutorialUI(active);
            pinTutorials[index].SetActive(active);
        }
    }
}
