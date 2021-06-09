using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.CustomComponents;

public class CameraController : CharacterController
{
    private CharacterPoolParty characterPoolParty;
    private PinPoolParty pinPoolParty;
    private List<ObjectPool> uShapes = new List<ObjectPool>();
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private float maxZoom = 15f;
    [SerializeField]
    private float minZoom = 10f;
    [SerializeField]
    private float zoomLimiter = 50f;
    [SerializeField]
    private Vector3 center = new Vector3();
    private Camera cam;
    private float smoothTime = .5f;
    private Vector3 velocity = new Vector3();
    private Vector3 cameraSize = new Vector3();
    private float greatestDistance;

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
        uShapes.Add(LandPoolParty.Instance.TerrainUpHole);
        uShapes.Add(LandPoolParty.Instance.TerrainUpHole2);
        uShapes.Add(LandPoolParty.Instance.TerrainUpHole3);


        cameraSize = new Vector3(Camera.main.aspect * Camera.main.orthographicSize * 2, Camera.main.orthographicSize * 2);
    }
    private void LateUpdate()
    {
        if(Player != null && GameManager.State == GameManager.GameState.play)
        {
            Move();
        }
    }

    private void Move()
    {
        Vector3 target = new Vector3();
        float defaultResolution = 1280f / 720f;
        float defaultMaxZoom = maxZoom;
        float currentResolution = (float)Screen.height / (float)Screen.width;
        float newMaxZoom = currentResolution * defaultMaxZoom / defaultResolution;
        if (destinations.Count == 0)
        {
            target = new Vector3(Player.transform.position.x + offset.x, Player.transform.position.y + offset.y, transform.position.z);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newMaxZoom, Time.deltaTime);
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
        float defaultResolution = 1280f / 720f;
        float defaultMaxZoom = maxZoom;
        float currentResolution = (float)Screen.height / (float)Screen.width;
        float newMaxZoom = currentResolution * defaultMaxZoom / defaultResolution;

        float newZoom = Mathf.Lerp(newMaxZoom, minZoom, greatestDistance / zoomLimiter);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime);
    }
    private float GetGreatestDistance()
    {
        if(destinations[0] != null)
        {
            Bounds bounds = new Bounds(destinations[0].position, Vector3.zero);
            for(int i = 0; i < destinations.Count; i++)
            {
                bounds.Encapsulate(destinations[i].position);
            }
            return bounds.size.x;
        }
        return 0;
    }
    private void GetTargets()
    {
        destinations.Add(Player.transform.GetChild(0));
        GetObjectsInCamera(pinPoolParty.Party.Pools);
        GetObjectsInCamera(characterPoolParty.Party.Pools);
        GetObjectsInCamera(uShapes);
    }
    private void GetObjectsInCamera(List<ObjectPool> objectPools)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(gameObject.transform.position, cameraSize, 0);
        foreach(Collider2D collide in colliders)
        {
            foreach (ObjectPool pool in objectPools)
            {
                foreach (GameObject pooledObject in pool.PooledObjects)
                {
                    if (pooledObject != null && pooledObject.activeInHierarchy && pooledObject != Player.gameObject)
                    {

                        GameObject collideObject = collide.gameObject;
                        Transform collideTransform = collide.transform;
                        Transform collideParent = collideTransform.parent;
                        Transform collideGrandparent = collideParent.parent;
                        if (collideObject == pooledObject || collideParent.gameObject == pooledObject || collideGrandparent.gameObject == pooledObject)
                        {
                            if(objectPools == characterPoolParty.Party.Pools)
                            {
                                destinations.Add(collideTransform.GetChild(0));
                            }
                            else
                            {
                                destinations.Add(collideTransform);
                            }
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
    private Vector3 GetCenterPoint()
    {
        GetTargets();
        if(destinations.Count == 1)
        {
            return destinations[0].position;
        }
        if(destinations.Count != 0)
        {
            var bound = new Bounds(destinations[0].position, Vector3.zero);
            for(int i = 0; i < destinations.Count; i++)
            {
                bound.Encapsulate(destinations[i].position);
            }
            return bound.center;
        }
        return new Vector3();
    }
    public void ZoomCenterPoint()
    {
        center = GetCenterPoint();
        greatestDistance = GetGreatestDistance();
    }
    public void RestartCenter()
    {
        destinations = new List<Transform>();
        center = new Vector3();
    }
}
