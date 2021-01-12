using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player;
    Transform mainCamera;

    private Vector3 playerPosition;

    public float verticalLimit = 6.8f;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = new Vector3(player.position.x, player.position.y, -10);
        mainCamera.position = playerPosition;
        
        if(mainCamera.position.y < verticalLimit)
        {
            mainCamera.position = new Vector3(playerPosition.x, verticalLimit, -10);
        }
    }
}
