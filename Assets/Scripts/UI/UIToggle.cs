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
            Image image = GetComponent<Image>();
            bool isChecked = image.sprite == spriteTrue ? true : false;
            if(isChecked)
            {
                image.sprite = spriteTrue;
            }
            if (isChecked)
            {
                image.sprite = spriteFalse;
            }
            return isChecked;
        }
        set
        {
            Image image = GetComponent<Image>();
            image.sprite = value == false ? spriteFalse : spriteTrue;
        }
    }

    public void Initiate(Sprite on, Sprite off)
    {
        spriteTrue = on;
        spriteFalse = off;
        isCheck = false;
    }
}
