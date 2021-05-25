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
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        material = sprite.material;
        float width = Screen.width;
        float widthRatio = transform.localScale.x / width;
        transform.localScale = new Vector3(widthRatio * width, widthRatio * width, 1);
    }
    // Update is called once per frame
    private void Update()
    {
        material.mainTextureOffset -= new Vector2(Time.deltaTime / speedDivider, 0);
        material.mainTextureOffset = ClampOffset(material.mainTextureOffset);
    }
    void LateUpdate()
    {
        if(GameManager.Instance.Player != null)
        {
            Vector2 offset = ((Vector2)GameManager.Instance.Player.transform.position - lastPosition) / runningDivider;
            material.mainTextureOffset += offset;
            lastPosition = GameManager.Instance.Player.transform.position;
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
