using System.Collections.Generic;
using UnityEngine;
using UnityEngine.CustomComponents;

public class CameraController : CharacterController
{
    private CharacterPoolParty characterPoolParty;
    private PinPoolParty pinPoolParty;
    private ObstaclePoolParty obstaclePoolParty;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private float maxZoom = 13f;
    [SerializeField]
    private float minZoom = 20f;
    [SerializeField]
    private float zoomLimiter = 50f;

    private Camera cam;
    private List<Transform> targets = new List<Transform>();
    private float smoothTime = .5f;
    private Vector3 velocity = new Vector3();
    private Vector3 cameraSize = new Vector3();

    public Vector3 center = new Vector3();
    #region Properties
    public static CameraController Instance {get; private set;}
    public PlayerController Player { get; set; }
    #endregion
    // Start is called before the first frame update
    protected override void Awake()
    {
        Instance = this;
    }
    protected override void Start()
    {
        cam = GetComponent<Camera>();
        characterPoolParty = CharacterPoolParty.Instance;
        pinPoolParty = PinPoolParty.Instance;
        obstaclePoolParty = ObstaclePoolParty.Instance;

        cameraSize = new Vector3(Camera.main.aspect * Camera.main.orthographicSize * 2, Camera.main.orthographicSize * 2);
    }
    private void LateUpdate()
    {
        if(Player != null)
        {
            Move();
        }

    }

    private void Move()
    {
        Vector3 target = new Vector3();
        if (targets.Count == 0)
        {
            target = new Vector3(Player.transform.position.x + offset.x, Player.transform.position.y + offset.y, transform.position.z);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, maxZoom, Time.deltaTime);
        }
        else
        {
            target = new Vector3(center.x + offset.x, center.y + offset.y, transform.position.z);
            Zoom();
        }
        ViewManager.Instance.SetSmoothPosition(transform, target, ref velocity, smoothTime);
    }
    private void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime);
    }
    private float GetGreatestDistance()
    {
        if(targets[0] != null)
        {
            Bounds bounds = new Bounds(targets[0].position, Vector3.zero);
            for(int i = 0; i < targets.Count; i++)
            {
                bounds.Encapsulate(targets[i].position);
            }
            return bounds.size.x;
        }
        return 0;
    }
    private void GetTargets()
    {
        GetObjectsInCamera(pinPoolParty.Party.Pools);
        GetObjectsInCamera(characterPoolParty.Party.Pools);
        GetObjectsInCamera(obstaclePoolParty.Party.Pools);
    }
    private void GetObjectsInCamera(List<ObjectPool> objectPools)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(gameObject.transform.position, cameraSize, 0);
        foreach (ObjectPool pool in objectPools)
        {
            foreach (GameObject pooledObject in pool.PooledObjects)
            {
                if (pooledObject != null && pooledObject.activeInHierarchy)
                {
                    
                    foreach(Collider2D collide in colliders)
                    {
                        GameObject collideObject = collide.gameObject;
                        Transform collideTransform = collide.transform;
                        Transform collideParent = collideTransform.parent;
                        Transform collideGrandparent = collideParent.parent;
                        if (collideObject == pooledObject)
                        {
                            targets.Add(collideTransform);
                        }
                        else if(collideParent.gameObject == pooledObject)
                        {
                            targets.Add(collideParent);
                        }
                        else if(collideGrandparent.gameObject == pooledObject)
                        {
                            targets.Add(collideGrandparent);
                        }
                    }
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
        Gizmos.DrawWireCube((Vector2)transform.position, new Vector3(Camera.main.aspect * Camera.main.orthographicSize * 2, Camera.main.orthographicSize * 2));
    }

    public Vector3 GetCenterPoint()
    {
        GetTargets();
        if(targets.Count == 1)
        {
            return targets[0].position;
        }
        if(targets.Count != 0)
        {
            var bound = new Bounds(targets[0].position, Vector3.zero);
            for(int i = 0; i < targets.Count; i++)
            {
                bound.Encapsulate(targets[i].position);
            }
            return bound.center;
        }
        return new Vector3();
    }
    public Vector3 RestartCenter()
    {
        targets = new List<Transform>();
        return new Vector3();
    }
}
