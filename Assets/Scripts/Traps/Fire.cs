using UnityEngine;
using UnityEngine.CustomComponents;

public class Fire : MonoBehaviour
{
    [SerializeField]
    private LineRenderer line;
    [SerializeField]
    private Transform[] transforms = new Transform[2];
    private float timer = 0;
    private float maxTimer = 0.15f;
    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, transforms[0].position);
        timer += Time.deltaTime;
        if (timer >= maxTimer)
        {
            CheckHit();
            timer = 0;
        }
    }
    private void CheckHit()
    {
        Vector3 direction = GameManager.Instance.GetDirectionVector(transforms[0].position, transforms[1].position);
        RaycastHit2D hit = Physics2D.Raycast(transforms[0].position, direction);
        if (hit.point != new Vector2())
        {
            line.SetPosition(1, hit.point);
            if (hit.collider != null)
            {
                OnHit(hit.collider.gameObject);
            }
        }
        else
        {
            line.SetPosition(1, transforms[1].position);
        }
    }
    private void OnHit(GameObject beenHitObject)
    {
        if (beenHitObject.tag == "Player")
        {
            CharacterPoolParty.Instance.PlayerPool.GetBackToPool(beenHitObject);
        }
        else if (beenHitObject.tag == "Enemy")
        {
            ObjectPool pool = GetCharacterPool(beenHitObject);
            if (pool != null)
            {
                pool.GetBackToPool(beenHitObject);
            }
        }
    }
    private ObjectPool GetCharacterPool(GameObject gameObject)
    {
        foreach (ObjectPool pool in CharacterPoolParty.Instance.Party.Pools)
        {
            if (pool != CharacterPoolParty.Instance.PlayerPool)
            {
                foreach (GameObject pooledObject in pool.PooledObjects)
                {
                    if (pooledObject == gameObject && pooledObject.activeInHierarchy)
                    {
                        return pool;
                    }
                }
            }
        }
        return null;
    }
}
