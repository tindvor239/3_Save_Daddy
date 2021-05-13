using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : CharacterController
{
    public static CameraController Instance {get; private set;}
    public PlayerController Player { get; set; }
    [SerializeField]
    private Vector3 offset;
    // Start is called before the first frame update
    protected override void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        
    }
    // Update is called once per frame
    private void LateUpdate()
    {
        if(Player != null)
        {
            Vector3 target = new Vector3(Player.transform.position.x + offset.x, Player.transform.position.y + offset.y, transform.position.z);
            ViewManager.SetPosition(transform, target);
        }

    }
}
