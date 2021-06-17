using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningWater : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Vector2 lastPosition;
    private Material material;
    [SerializeField]
    private float speedDivider = 6f;
    [SerializeField]
    private float runningDivider = 20f;
    float startWidth, startHeight;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        material = sprite.material;
        startWidth = sprite.bounds.size.x;
        startHeight = sprite.bounds.size.y;
    }
    // Update is called once per frame
    private void Update()
    {
        if (GameManager.State == GameManager.GameState.play)
        {
            material.mainTextureOffset -= new Vector2(Time.deltaTime / speedDivider, 0);
            material.mainTextureOffset = ClampOffset(material.mainTextureOffset);
        }
    }
    void LateUpdate()
    {
        if (GameManager.State == GameManager.GameState.play)
        {
            float height = Camera.main.orthographicSize * 2;
            float width = height * 1.5f;
            transform.localScale = new Vector3(width, height, 1);
            if (GameManager.Instance.Player != null)
            {
                Vector2 offset = ((Vector2)GameManager.Instance.Player.transform.position - lastPosition) / runningDivider;
                material.mainTextureOffset += offset;
                lastPosition = GameManager.Instance.Player.transform.position;
            }
        }
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
