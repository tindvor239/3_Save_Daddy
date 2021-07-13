using System.Collections;
using UnityEngine;

public class Fan : Trap
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float blowingSpeed = 10f;
    [SerializeField]
    private bool isReverse;
    private float speedMultiplier = 0.002f;
    private float timer = 0;
    private float maxTime = 0.15f;

    private int count = 0;
    private GameObject blowedObject;
    // Update is called once per frame
    private void Update()
    {
        GameManager.Instance.OnHitCallBack(ref timer, in maxTime, CheckHit);
    }
    private void FixedUpdate()
    {
        Blowing();
    }
    private void CheckHit()
    {
        GameObject beenHitObject = RayCastingToObject();
        Debug.DrawRay(transform.position, GameManager.Instance.GetDirectionVector(transform.position, target.position), Color.red, Vector2.Distance(transform.position, target.position) / 2);
        OnHit(beenHitObject);
        if (beenHitObject != null && (beenHitObject.tag == "Player" || beenHitObject.tag == "Enemy"))
        {
            if (beenHitObject.GetComponent<PlayerController>() != null && count == 0)
            {
                StartCoroutine(MoveNext(beenHitObject.GetComponent<PlayerController>(), beenHitObject.GetComponent<PlayerController>().MoveDuration(beenHitObject.transform.position, target.position) + 1));
                count++;
            }
        }
    }

    protected override void OnHit(GameObject beenHitObject)
    {
        blowedObject = beenHitObject;
        if(beenHitObject != null && beenHitObject.GetComponent<CharacterController>())
        {
            beenHitObject.GetComponent<CharacterController>().Stop();
        }
    }
    protected void Blowing()
    {
        Vector3 direction = GameManager.Instance.GetDirectionVector(transform.position, target.position);
        if(blowedObject != null && (blowedObject.tag == "Player" || blowedObject.tag == "Enemy"))
        {
            if (isReverse)
            {
                blowedObject.transform.position -= direction * Time.deltaTime * speedMultiplier * blowingSpeed;
            }
            else
            {
                blowedObject.transform.position += direction * Time.deltaTime * speedMultiplier * blowingSpeed;
            }
            if(blowedObject.tag == "Player")
            {
                CameraController.Instance.MoveKeepCenter();
            }
        }
    }
    private GameObject RayCastingToObject()
    {
        GameObject beenHitObject = GameManager.Instance.RayCastToObject(transform.position, target.position, 1 << LayerMask.NameToLayer("Pin"));
        if (beenHitObject == null)
        {
            beenHitObject = GameManager.Instance.RayCastToObject(transform.position, target.position, 1 << LayerMask.NameToLayer("Player"));
            if (beenHitObject == null)
            {
                beenHitObject = GameManager.Instance.RayCastToObject(transform.position, target.position, 1 << LayerMask.NameToLayer("Enemy"));
            }
        }
        return beenHitObject;
    }
    private IEnumerator MoveNext(PlayerController player, float duration)
    {
        yield return new WaitForSeconds(duration);
        player.MovePlayerToNextDestination();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        sound.PlayLoop(sound.clip);
        timer = 0;
        count = 0;
    }
}
