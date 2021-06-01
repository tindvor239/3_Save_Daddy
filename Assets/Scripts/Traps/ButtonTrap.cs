using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrap : Trap
{
    [SerializeField]
    private List<Arrow> arrows = new List<Arrow>();
    [SerializeField]
    private Vector2 size = new Vector2(0, 1);
    private float timer = 0;
    private float maxTimer = .2f;
    protected override void Start()
    {
        arrows = FindObjectsOfType<Arrow>().Where(f => f.gameObject.activeInHierarchy).ToList();
    }
    private void Update()
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
                Debug.Log(hit);
                Disarmed();
            }
        }
        timer = 0;
    }
    private void OnEnable()
    {
        isDisarmed = false;
        timer = 0;
        arrows = new List<Arrow>();
        arrows = FindObjectsOfType<Arrow>().Where(f => f.gameObject.activeInHierarchy).ToList();
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
        isDisarmed = true;
    }
}
