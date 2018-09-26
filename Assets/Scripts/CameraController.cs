using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;

    private Vector3 offset;

    //Set the offset of the camera based on the start position of camera and player
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    //Update the camera position based on where the player moves
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
