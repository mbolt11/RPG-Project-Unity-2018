using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;

    //private Vector3 offset;

    //Set the offset of the camera based on the start position of camera and player
    void Start()
    {

    }

    //Update the camera position based on where the player moves
    void LateUpdate()
    {
        transform.position = new Vector3(0f, player.position.y + 2, player.position.z - 4);
        transform.rotation = Quaternion.identity;
        //print(ToString());
        //print(transform.position);

    }
}
