using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.CustomComponents;

public class CameraController : CharacterController
{
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private float size;
    private Camera cam;
    private float smoothTime = .5f;
    private Vector3 velocity = new Vector3();

    private float defaultResolution = 1280f / 720f;
    private float currentResolution;
    #region Properties
    public static CameraController Instance { get; private set; }
    public PlayerController Player { get; set; }
    public Vector3 FixedOffset { get; set; }
    #endregion
    // Start is called before the first frame update
    protected override void Awake()
    {
        Instance = this;
    }
    protected override void Start()
    {
        cam = GetComponent<Camera>();
        currentResolution = (float)Screen.height / (float)Screen.width;
        FixedOffset = offset;
    }
    protected override void Update()
    {
        if (Player != null && GameManager.State == GameManager.GameState.play)
        {
            Moving();
        }
    }

    private void Moving()
    {
        Vector3 target = new Vector3();
#if UNITY_EDITOR
        if (Application.isPlaying)
        {
            currentResolution = (float)Screen.height / (float)Screen.width;
        }
#endif
        float size = this.size;
        if (destinations.Count == 0)
        {
            Debug.Log(FixedOffset);
            target = new Vector3(Player.transform.position.x + FixedOffset.x, Player.transform.position.y + FixedOffset.y, transform.position.z);
            target = new Vector3(target.x + moveSpeed, target.y, target.z);
        }
        else
        {
            CameraPath cameraPath = destinations[0].GetComponent<CameraPath>();
            size = cameraPath.Size * currentResolution / defaultResolution;
            target = new Vector3(cameraPath.Center.x, cameraPath.Center.y, transform.position.z);
        }
        cam.orthographicSize = ViewManager.Instance.SmoothFloat(cam.orthographicSize, size, Time.deltaTime);
        ViewManager.Instance.SetSmoothPosition(transform, target, ref velocity, smoothTime);
    }
    private Transform ClosestPath()
    {
        CameraPath closestPath = null;
        float closestDistance = 0;
        foreach (CameraPath cameraPath in GameManager.Instance.CameraPaths)
        {
            float currentDistance = Vector3.Distance(transform.position, cameraPath.transform.position);
            if (closestDistance == 0 || closestDistance > currentDistance)
            {
                closestDistance = currentDistance;
                closestPath = cameraPath;
            }
        }
        return closestPath.transform;
    }

    public void ZoomCenterPoint()
    {
        destinations.Add(ClosestPath());
    }
    public void Move()
    {
        destinations = new List<Transform>();
        FixedOffset = offset;
    }
    public void MoveKeepCenter()
    {
        FixedOffset = destinations[0].GetComponent<CameraPath>().Center - Player.transform.position;
        FixedOffset = new Vector3(FixedOffset.x - moveSpeed, FixedOffset.y);
        Debug.Log(FixedOffset);
        destinations = new List<Transform>();
    }
}
