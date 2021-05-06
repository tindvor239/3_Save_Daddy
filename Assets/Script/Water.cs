using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private SpriteRenderer sprite;
    private PlayerController player;
    private Vector2 lastPosition;
    private Material material;
    [SerializeField]
    private float speedDivider = 6f;
    [SerializeField]
    private float runningDivider = 20f;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        player = GameManager.Instance.Player;
        material = sprite.material;
    }
    // Update is called once per frame
    private void Update()
    {
        if(player == null)
        {
            player = GameManager.Instance.Player;
        }
        material.mainTextureOffset -= new Vector2(Time.deltaTime / speedDivider, 0);
        material.mainTextureOffset = ClampOffset(material.mainTextureOffset);
    }
    void LateUpdate()
    {
        Vector2 offset = ((Vector2)player.transform.position - lastPosition) / runningDivider;
        material.mainTextureOffset += offset;
        lastPosition = player.transform.position;
    }

    private Vector2 ClampOffset(Vector2 offset)
    {
        Vector2 result = offset;
        if (offset.x >= 1)
        {
            result.x = 0;
        }
        if (offset.y >= 1)
        {
            result.y = 0;
        }
        return result;
    }
}
