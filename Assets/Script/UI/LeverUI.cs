using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeverUI : MonoBehaviour
{
    [SerializeField]
    private List<Transform> transforms;
    [SerializeField]
    private List<Sprite> sprites;
    [SerializeField]
    private Image buttonIcon;
    private float timer = 0.5f;
    public bool isActive;
    public void OnCLicked()
    {
        isActive = !isActive;
        if(isActive == false)
        {
            GameController.Instance.Move(buttonIcon.transform, transforms[0].position, timer);
        }
        else
        {
            GameController.Instance.Move(buttonIcon.transform, transforms[1].position, timer);
        }
        float iconChangeTimer = timer - 0.2f;
        StartCoroutine(ChangeButtonIcon(iconChangeTimer));
    }

    private IEnumerator ChangeButtonIcon(float time)
    {
        yield return new WaitForSeconds(time);
        ViewManager.Instance.ChangeSprite(buttonIcon, sprites[ isActive == false ? 0 : 1]);
    }
}
