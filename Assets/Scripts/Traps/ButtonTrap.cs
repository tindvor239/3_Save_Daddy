using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrap : Trap
{
    [SerializeField]
    private List<Arrow> arrows = new List<Arrow>();
    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private Vector2 size = new Vector2(0, 1);
    private SpriteRenderer currentSprite;
    private float timer = 0;
    private float maxTimer = .2f;
    protected override void Start()
    {
        base.Start();
        if (currentSprite == null)
        {
            currentSprite = GetComponent<SpriteRenderer>();
        }
        arrows = FindObjectsOfType<Arrow>().Where(f => f.gameObject.activeInHierarchy).ToList();
    }
    protected override void Update()
    {
        if(isDisarmed == false)
        {
            GameManager.Instance.OnHitCallBack(ref timer, maxTimer, CheckHit);
        }
    }
    private void CheckHit()
    {
        List<GameObject> hits =
            GameManager.Instance.BoxCastAll(transform.position, size, transform.eulerAngles.z, transform.right, 0);
        foreach(GameObject hit in hits)
        {
            if(hit.GetComponent<Obstacle>() != null && hit != gameObject)
            {
                Disarmed();
            }
        }
        timer = 0;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        if (currentSprite == null)
        {
            currentSprite = GetComponent<SpriteRenderer>();
        }
        currentSprite.sprite = sprites[0];
        isDisarmed = false;
        timer = 0;
        arrows = new List<Arrow>();
        StartCoroutine(GetArrows());
    }
    private IEnumerator GetArrows()
    {
        if(MapEditor.Instance != null)
        {
            while(MapEditor.Instance.Process < 100)
            {
                yield return null;
            }
            arrows = FindObjectsOfType<Arrow>().Where(f => f.gameObject.activeInHierarchy).ToList();
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }
#endif
    public override void Disarmed()
    {
        foreach(Arrow arrow in arrows)
        {
            arrow.Disarmed();
        }
        currentSprite.sprite = sprites[1];
        sound.PlayOnce(sound.clip);
        isDisarmed = true;
    }
}
