using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIToggle : MonoBehaviour
{
    [SerializeField]
    private Sprite spriteTrue;
    [SerializeField]
    private Sprite spriteFalse;
    public bool isCheck
    {
        get
        {
            bool isChecked = GetComponent<Image>().sprite == spriteTrue ? true : false;
            if(isChecked)
            {
                GetComponent<Image>().sprite = spriteTrue;
            }
            if (isChecked)
            {
                GetComponent<Image>().sprite = spriteFalse;
            }
            return isChecked;
        }
    }
}
