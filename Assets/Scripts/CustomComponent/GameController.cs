using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DoozyUI;
using System.Collections.Generic;
public class GameController : Singleton<GameController>
{
    private PlayerController player;
    private static bool canTrigger = false;
    private static float unpinDelay = 1f;
    private static float unpinDelayTimer = 1f;

    public GameManager.GameState lastState;
    protected override void Awake()
    {
        #region Singleton
        base.Awake();
        #endregion
    }
    private void Start()
    {
        player = GameManager.Instance.Player;
    }
    private void Update()
    {
        switch(GameManager.State)
        {
            case GameManager.GameState.play:
                UnpinDelayCountDown();
                if (Input.GetMouseButtonDown(0) && unpinDelayTimer <= 0)
                {
                    unpinDelayTimer = 0;
                    canTrigger = true;
                    GameObject clickedObject = GetObjectByMouseRayCast();
                    Debug.Log(clickedObject);
                    if (clickedObject != null)
                    {
                        Pin selectedPin = GetPinComponent(clickedObject);
                        if(selectedPin != null)
                        {
                            selectedPin.Interact();
                        }
                    }
                }
                break;
        }
        BackHandle();
    }

    private Pin GetPinComponent(GameObject clickedObject)
    {
        Pin result = null;
        Transform parent = clickedObject.transform.parent;

        if (CanUnpin(clickedObject.GetComponent<Pin>()))
        {
            Debug.Log(string.Format("Pin: {0}, can unpin: {1}", clickedObject.GetComponent<Pin>()? true : false, CanUnpin(clickedObject.GetComponent<Pin>())));
            result = clickedObject.GetComponent<Pin>();
        }
        else if (CanUnpin(parent.GetComponentInChildren<Pin>()))
        {
            Debug.Log(string.Format("Pin: {0}, can unpin: {1}", parent.GetComponentInChildren<Pin>() ? true : false, CanUnpin(clickedObject.GetComponent<Pin>())));
            result = parent.GetComponentInChildren<Pin>();
        }
        else if(CanUnpin(parent.parent.gameObject.GetComponent<Pin>()))
        {
            Debug.Log(string.Format("Pin: {0}, can unpin: {1}", parent.parent.gameObject.GetComponent<Pin>() ? true : false, CanUnpin(clickedObject.GetComponent<Pin>())));
            result = parent.parent.gameObject.GetComponent<Pin>();
        }
        unpinDelayTimer = unpinDelay;
        return result;
    }
    private bool CanUnpin(Pin pin)
    {
        return pin != null && canTrigger;
    }
    private void UnpinDelayCountDown()
    {
        if(unpinDelayTimer > 0)
        {
            unpinDelayTimer -= Time.deltaTime;
            canTrigger = false;
        }
        
    }
    #region Behaviours
    #region Inputs
    public GameObject GetObjectByMouseRayCast()
    {
        return GameManager.Instance.RayCastObject(GetMousePosition(), Vector3.forward, Mathf.Infinity, 1 << LayerMask.NameToLayer("Pin"));
    }
    public Vector3 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    public Vector3 MousePositionOnScreen()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    #endregion
    #region Movement Handle
    public void Move(in Sequence sequence, in Transform moveObject, in Vector2 destination, in float duration)
    {
        ViewManager.Instance.MoveOutput(sequence, moveObject, destination, duration);
    }
    public void Move(in Sequence sequence, in Transform moveObject, in Vector2 destination, in float duration, in Ease ease)
    {
        ViewManager.Instance.MoveOutput(sequence, moveObject, destination, duration, ease);
    }
    public void Move(in Transform moveObject, in Vector2 destination, in float duration)
    {
        ViewManager.Instance.MoveOutput(moveObject, destination, duration);
    }
    public void Move(in Transform moveObject, in Vector2 destination, in float duration, in Ease ease)
    {
        ViewManager.Instance.MoveOutput(moveObject, destination, duration, ease);
    }
    #endregion
    #region Rotate Handle
    public void Rotate(in Transform transform, in Quaternion target,in float duration)
    {
        ViewManager.Instance.Rotating(transform, target, duration);
    }
    public void Rotate(in Transform transform, in Vector3 target, in float duration, Ease ease)
    {
        ViewManager.Instance.Rotating(transform, target, duration, ease);
    }
    public void Rotate(Sequence sequence, in Transform transform, in Vector3 target, in float duration, Ease ease)
    {
        ViewManager.Instance.Rotating(sequence, transform, target, duration, ease);
    }
    public void Rotate(in Transform transform, in Vector3 target)
    {
        ViewManager.Instance.Rotating(transform, target);
    }
    #endregion
    #region Calculation

    public void LookRotation(Vector3 axis, GameObject gameObject, Vector3 lookDirection)
    {
        Quaternion lookRotation = Quaternion.LookRotation(axis, lookDirection);
        ViewManager.Instance.Rotating(gameObject.transform, lookRotation);
    }
    public void LookRotation(Vector3 axis, GameObject gameObject, Vector3 lookDirection, float min, float max)
    {
        Quaternion lookRotation = Quaternion.LookRotation(axis, lookDirection);
        ViewManager.Instance.Rotating(gameObject.transform, lookRotation, min, max);
    }
    #endregion
    private void BackHandle()
    {
        bool isActive;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (GameManager.State)
            {
                case GameManager.GameState.play:
                    UIController.Instance.ShowSettingUI(true);
                    break;
                case GameManager.GameState.level:
                    UIController.Instance.ShowLevelUI(false);
                    UIController.Instance.ShowMenuUI(true);
                    break;
                case GameManager.GameState.pause:
                    isActive = UIElement.IsUIElementVisible("SETTINGS_UI");
                    if(isActive)
                        UIController.Instance.ShowSettingUI(false);
                    else
                    {
                        UIController.Instance.ShowBeforeExitUI(false);
                    }
                    break;
                case GameManager.GameState.menu:
                    isActive = UIElement.IsUIElementVisible("BEFOREEXIT_UI");
                    UIController.Instance.ShowBeforeExitUI(!isActive);
                    break;
            }
        }
    }
    #endregion
}
