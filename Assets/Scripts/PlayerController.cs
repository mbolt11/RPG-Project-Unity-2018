﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //player forward/backward speed
    public float playerSpeed = 5f;

    //player turn speed, should be 15 * playerSpeed
    public float playerTurnSpeed;

    private Rigidbody rb;
    private float moveInput;
    private float turnInput;

    //Keeps track of if we have loaded the fixing scene yet
    private bool loaded = false; 

    // Use this for initialization
    void Start()
    {
        //store the rigidbody component
        rb = GetComponent<Rigidbody>();
        playerTurnSpeed = 50f;
    }

    private void Update()
    {
        //get keyboard input each frame
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }

    //physics code
    void FixedUpdate()
    {
        MovePlayer();
        TurnPlayer();
    }

    //Move player Forward/Backward
    private void MovePlayer()
    {
        Vector3 movement = transform.forward * moveInput * playerSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    //turn player left/right
    private void TurnPlayer()
    {
        float turnValue = turnInput * playerTurnSpeed * Time.deltaTime;
        Quaternion turn = Quaternion.Euler(0f, turnValue, 0f);
        rb.MoveRotation(rb.rotation * turn);
    }

    //If the player collides with a Robot, move him to the fixing scene
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Robot")
        {
            if(!loaded)
            {
                //Load the fix it scene
                SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);
                loaded = true;

                //Unload the overworld scene
                //MySceneManager.mySceneManager.UnloadScene(1);
            }
        }
    }
}

