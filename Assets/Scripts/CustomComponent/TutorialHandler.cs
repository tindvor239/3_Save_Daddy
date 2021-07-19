using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHandler : MonoBehaviour
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
    public void OnCheckTutorial()
    {

    }    
    public void SetUpTutorial()
    {

    }
    public void TutorialOn()
    {
        tutorialUI.SetActive(true);
    }
}
