using System.Collections;
using UnityEngine;

public class Fan : Trap
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float blowingSpeed = 10f;
    private float speedMultiplier = 0.002f;
    private float timer = 0;
    private float maxTimer = 0.15f;

    private GameObject blowedObject;
    private delegate void OnBlowing();
    private OnBlowing onBlowing;
    // Start is called before the first frame update
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= maxTimer)
        {
            CheckHit();
            timer = 0;
        }
    }
    private void FixedUpdate()
    {
        if(onBlowing != null)
        {
            onBlowing.Invoke();
        }
    }
    private void CheckHit()
    {
        GameObject beenHitObject = GameManager.Instance.RayCastToObject(transform.position, target.position);
        if (beenHitObject != null && (beenHitObject.tag == "Player" || beenHitObject.tag == "Enemy"))
        {
            OnHit(beenHitObject);
        }
        else
        {
            onBlowing = null;
        }
    }

    protected override void OnHit(GameObject beenHitObject)
    {
        blowedObject = beenHitObject;
        if(beenHitObject.GetComponent<CharacterController>())
        {
            beenHitObject.GetComponent<CharacterController>().Stop();
        }
        onBlowing += Blowing;
        if (beenHitObject.GetComponent<PlayerController>() != null &&
            beenHitObject.transform.position.x >= target.transform.position.x && beenHitObject.transform.position.y >= target.transform.position.y)
        {
            StartCoroutine(MoveNext(beenHitObject.GetComponent<PlayerController>(), beenHitObject.GetComponent<PlayerController>().MoveDuration(beenHitObject.transform.position, target.position)));
        }
    }
    protected void Blowing()
    {
        blowedObject.transform.position += GameManager.Instance.GetDirectionVector(transform.position, target.position) * Time.deltaTime * speedMultiplier * blowingSpeed;
    }

    private IEnumerator MoveNext(PlayerController player, float duration)
    {
        yield return new WaitForSeconds(duration);
        player.MovePlayerToNextDestination();
    }
    private void OnEnable()
    {
        timer = 0;
        onBlowing = null;
    }
}
