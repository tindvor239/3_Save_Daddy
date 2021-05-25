using System;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

[Serializable]
public struct CharacterData
{
    [SerializeField]
    private string name;
    [SerializeField]
    private AnimationReferenceAsset idle;
    [SerializeField]
    private AnimationReferenceAsset idleToMove;
    [SerializeField]
    private AnimationReferenceAsset die;
    [SerializeField]
    private AnimationReferenceAsset dieToMove;
    [SerializeField]
    private AnimationReferenceAsset move;
    #region Properties
    public string Name { get => name; }
    public AnimationReferenceAsset Idle { get => idle; }
    public AnimationReferenceAsset IdleToMove { get => idleToMove; }
    public AnimationReferenceAsset Die { get => die; }
    public AnimationReferenceAsset DieToMove { get => dieToMove; }
    public AnimationReferenceAsset Move { get => move; }
    #endregion
}