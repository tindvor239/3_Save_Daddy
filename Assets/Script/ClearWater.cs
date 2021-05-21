using System.Collections;
using UnityEngine;
using UnityEngine.CustomComponents;

public class ClearWater : ClearLiquid
{
    [SerializeField]
    private SpriteRenderer sprite;
    public override void StartClear(LiquidPool liquidPool, Vector3 position)
    {
        base.StartClear(liquidPool, position);
        ViewManager.Fading(sprite, 0, 4f);
    }

    private void OnEnable()
    {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
    }
}
