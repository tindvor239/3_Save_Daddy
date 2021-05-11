using System;
using UnityEngine;

public class UISettings : UISection
{
    [SerializeField]
    private LeverUI[] settings = new LeverUI[3];
    #region Properties
    public bool Sound { get => settings[0].isActive; }
    public bool Music { get => settings[1].isActive; }
    public bool Vibration { get => settings[2].isActive; }
    #endregion
}
