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

    //direction flags
    bool moveForward, moveBack, moveLeft, moveRight;

    //Keeps track of if we have loaded the fixing scene yet
    private bool loaded = false;

    //reference to the main camera
    public GameObject userCamera;

    //For player-villager interactions
    public GameObject villager;
    private bool inRange = false;

    // Use this for initialization
    void Start()
    {
        //store the rigidbody component
        rb = GetComponent<Rigidbody>();
        playerTurnSpeed = 50f;
    }

    private void Update()
    {
        //see which key is down
        if (Input.GetKeyDown(KeyCode.W))
        {
            moveLeft = false;
            moveRight = false;
            moveForward = true;
            moveBack = false;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            moveLeft = true;
            moveRight = false;
            moveForward = false;
            moveBack = false;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            moveLeft = false;
            moveRight = false;
            moveForward = false;
            moveBack = true;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            moveLeft = false;
            moveRight = true;
            moveForward = false;
            moveBack = false;
        }

        if(!Input.anyKey)
        {
            moveLeft = false;
            moveRight = false;
            moveForward = false;
            moveBack = false;
        }

        //Start dialogue if they press enter key when they are in range
        if (Input.GetKeyDown(KeyCode.Return) && inRange)
        {
            villager.GetComponent<VillagerManager>().talkToVillager();
        }
    }

    //physics code
    void FixedUpdate()
    {
        TurnPlayer();
        MovePlayer();
    }

    //Move player Forward/Backward
    private void MovePlayer()
    {
        if(moveForward || moveBack || moveLeft || moveRight)
        {
            Vector3 movement = transform.forward * playerSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + movement);
        }
        else
        {
            rb.velocity = new Vector3(0f,0f,0f);
        }
    }

    //turn player left/right
    private void TurnPlayer()
    {
        if(moveLeft)
        {
            rb.rotation = Quaternion.Euler(0f, -90f, 0f);
        }
        else if(moveRight)
        {
            rb.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
        else if(moveForward)
        {
            rb.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if(moveBack)
        {
            rb.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //If the player collides with a Robot, go to the fixing scene
        if (other.tag == "Common Robot" || other.tag == "Outside Robot")
        {
            if(other.tag == "Common Robot")
            {
                GameObject.Find("GameController").GetComponent<OverworldGameController>().getSingleton().setEnemyRobot("Common Robot");
            }
            else if(other.tag == "Outside Robot")
            {
                GameObject.Find("GameController").GetComponent<OverworldGameController>().getSingleton().setEnemyRobot("Outside Robot");
            }
            //Safety check that scene has not loaded already
            if (!loaded)
            {
                userCamera.GetComponent<TransitionScript>().StartTransition();
                StartCoroutine(WaitFor()); //wait for 3 seconds
                
                //userCamera.GetComponent<TransitionScript>().EndTransition();
            }
        }

        //Record if the player is within the radius of a villager
        if(other.tag == "Villager")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //If player leaves the villager radius, it is no longer in range
        if (other.tag == "Villager")
        {
            inRange = false;
        }
    }

    IEnumerator WaitFor()
    {
        Debug.Log("waiting");
        yield return new WaitForSeconds(3);
        Debug.Log("doneWaiting");

        //Unload the overworld scene
        //SceneManager.UnloadSceneAsync(0);
        //Load the fix it scene
        SceneManager.LoadScene(1);
    }
}