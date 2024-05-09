using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        var playerPosition = player.transform.position;
        var position = transform.position;
        position.x = playerPosition.x;
        position.y = playerPosition.y+2;
        position.z = playerPosition.z-8;
        transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        var playerPosition = player.transform.position;
        var position = transform.position;
        position.x = playerPosition.x;
        position.y = playerPosition.y + 2;
        position.z = playerPosition.z - 8;
        transform.position = position;
    }
}
