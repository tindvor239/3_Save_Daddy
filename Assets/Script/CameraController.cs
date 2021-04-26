using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : CharacterController
{
    private CharacterController player;
    [SerializeField]
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        player = GameController.PlayerController;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = new Vector3(player.transform.position.x + offset.x, player.transform.position.y + offset.y, transform.position.z);
        SceneOutput.SetPosition(transform, target);
    }
}
