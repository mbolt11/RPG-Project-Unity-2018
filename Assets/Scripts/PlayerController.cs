using System.Collections;
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
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

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
        // Adjust the position of the tank based on the player's input.
        Vector3 movement = transform.forward * moveInput * playerSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    //turn player left/right
    private void TurnPlayer()
    {
        float turn = turnInput * playerTurnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
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
        yield return new WaitForSeconds(3);

        //Load the fix it scene
        SceneManager.LoadScene(1);
    }
}