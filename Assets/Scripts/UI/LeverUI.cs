using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeverUI : MonoBehaviour
{
    [SerializeField]
    private List<Transform> transforms;
    [SerializeField]
    private UIToggle button;
    private float timer = 0.5f;

    #region Properties
    public UIToggle Button { get => button; }
    #endregion
    public void OnCLicked(out bool isActive)
    {
        if (GameManager.State == GameManager.GameState.pause)
        {
            isActive = !button.isCheck;
            MoveButton(isActive);
            float iconChangeTimer = timer - 0.2f;
            StartCoroutine(ChangeButtonIcon(iconChangeTimer, isActive));
        }
        else
            isActive = false;
    }
    public void Initiate(in bool isActive)
    {
        button.isCheck = isActive;
        ViewManager.Instance.SetPosition(button.transform, GetPosition(isActive));
    }

    private void MoveButton(bool isActive)
    {
        if(isActive)
        {
            GameController.Instance.Move(button.transform, transforms[1].position, timer);
        }
        else
        {
            GameController.Instance.Move(button.transform, transforms[0].position, timer);
        }
    }
    private Vector3 GetPosition(bool isActive)
    {
        if(isActive)
        {
            return transforms[1].position;
        }
        else
        {
            return transforms[0].position;
        }
    }
    private IEnumerator ChangeButtonIcon(float time, bool isActive)
    {
        yield return new WaitForSeconds(time);
        button.isCheck = isActive;
    }
}
