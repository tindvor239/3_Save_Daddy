using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class Character : ScriptableObject
{
    [SerializeField]
    private CharacterData character;
    #region Property
    public CharacterData Data { get => character; }
    #endregion
    public void SetApearance(SkeletonAnimation skeleton)
    {
        skeleton.initialSkinName = character.Name;
    }
}
