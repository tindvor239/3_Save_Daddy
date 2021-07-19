using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : Trap
{
    [SerializeField]
    private SpriteRenderer[] blades = new SpriteRenderer[2];
    [SerializeField]
    private float spinSpeed = 800f;
    private bool beenHit = false;
    private Animator animator;
    private Collider2D collid;
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }
    private void SpiningBlades()
    {
        foreach(SpriteRenderer blade in blades)
        {
            float z = blade.transform.eulerAngles.z + (Time.deltaTime * spinSpeed);
            GameController.Instance.Rotate(blade.transform, new Vector3(0, 0, z));
        }
    }
    // Update is called once per frame
    protected override void Update()
    {
        SpiningBlades();
    }

    public override void OnBeingHit(GameObject hitObject)
    {
        if(!beenHit)
        {
            animator.SetTrigger("beenDestroyed");
            collid.enabled = false;
            beenHit = true;
            Disarmed();
            StartCoroutine(Destroy());
        }
    }
    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1.2f);
        ObstaclePoolParty.Instance.Party.GetPool("Saw Pool").GetBackToPool(gameObject);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        if(collid == null)
        {
            collid = GetComponent<Collider2D>();
        }
        collid.enabled = true;
        beenHit = false;
        sound.PlayLoop(sound.clip);
    }

    public override void Disarmed()
    {
        sound.source.Stop();
    }
}
