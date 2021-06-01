using UnityEngine;
using UnityEngine.UI;

public class LevelProcessDisplayUI : MonoBehaviour
{
    [SerializeField]
    private Image mainImage;
    [SerializeField]
    private GameObject tick;

    public Sprite MainImage { get => mainImage.sprite; set => mainImage.sprite = value; }
    public bool IsTick { get => tick.activeInHierarchy; set => tick.SetActive(value); }
}