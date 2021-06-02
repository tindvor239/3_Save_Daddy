using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIMapProcessing : MonoBehaviour
{
    [SerializeField]
    private Slider process;
    private delegate void OnProcessing();
    private event OnProcessing onProcessing;

    private void Update()
    {
        if(onProcessing != null)
        {
            onProcessing.Invoke();
        }
    }

    public void Process()
    {
        process.maxValue = MapEditor.Instance.processMaxValue;
        onProcessing += Processing;
    }

    private void Processing()
    {
        process.value = MapEditor.Instance.processValue;
        if(process.value == process.maxValue)
        {
            onProcessing = null;
            UIController.Instance.ShowProcessUI(false);
            UIController.Instance.ShowGameplayUI(true);
        }
    }
}
