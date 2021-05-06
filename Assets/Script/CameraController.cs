using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : CharacterController
{
    private CharacterController player;
    [SerializeField]
    private Vector3 offset;
    // Start is called before the first frame update
    protected override void Start()
    {
    }
    private void Update()
    {
        if(player == null)
        {
            player = GameManager.Instance.Player;
        }
    }
    // Update is called once per frame
    private void LateUpdate()
    {
        Vector3 target = new Vector3(player.transform.position.x + offset.x, player.transform.position.y + offset.y, transform.position.z);
        ViewManager.SetPosition(transform, target);
    }
}
