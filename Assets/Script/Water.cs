using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private SpriteRenderer sprite;
    private PlayerController player;
    private Vector2 lastPosition;
    [SerializeField]
    private Material material;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        player = GameController.PlayerController;
        material = sprite.material;
    }
    // Update is called once per frame
    private void Update()
    {
        material.mainTextureOffset += new Vector2(Time.deltaTime / 6, 0);
        material.mainTextureOffset = ClampOffset(material.mainTextureOffset);
    }
    void LateUpdate()
    {
        Vector2 offset = ((Vector2)player.transform.position - lastPosition) / 12;
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
