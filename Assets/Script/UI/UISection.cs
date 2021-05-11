using System;
using UnityEngine;
using UnityEngine.UI;
public class UISection : MonoBehaviour
{
    [Serializable]
    public class Display : SinglePair<string, MaskableGraphic> { };
    [SerializeField]
    protected Display[] displays;
    [Serializable]
    public class UserInterface : SinglePair<string, Selectable> { };
    [SerializeField]
    protected UserInterface[] userInterfaces;
    #region Properties
    public Display[] Displays { get => displays; }
    public UserInterface[] UserInterfaces { get => userInterfaces; }
    #endregion
    public MaskableGraphic GetDisplay(string name)
    {
        foreach (Display userinterface in displays)
        {
            if (name == userinterface.Key)
            {
                return userinterface.Value;
            }
        }
        return null;
    }

    public Selectable GetSelectable(string name)
    {
        foreach(UserInterface userinterface in userInterfaces)
        {
            if(name == userinterface.Key)
            {
                return userinterface.Value;
            }
        }
        return null;
    }
}
