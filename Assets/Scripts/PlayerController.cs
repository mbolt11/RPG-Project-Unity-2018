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

    //Singleton
    public static PlayerController Instance { get; set; }

    private Rigidbody rb;
    private float moveInput;
    private float turnInput;

    //Keeps track of if we have loaded the fixing scene yet
    private bool loaded = false;

    //reference to the main camera
    public GameObject userCamera;

    //For player-villager interactions
    public GameObject villager;
    private string villagerName;
    private bool villagerInRange = false;

    private GameObject chest;
    private bool chestInRange = false;
    private int chestNumber;

    // Use this for initialization
    void Start()
    {
        //store the rigidbody component
        rb = GetComponent<Rigidbody>();
        playerTurnSpeed = 50f;

        //Instantiate the script singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        //have press enter text to talk appear
        if (villagerInRange && !DialogueManager.Instance.stillTalking())
        {
            villager.GetComponent<VillagerManager>().inRangetoTalkText();
        }

        //Start dialogue if they press enter key when they are in range
        if (Input.GetKeyDown(KeyCode.Return) && villagerInRange && !DialogueManager.Instance.stillTalking())
        {
            villager.GetComponent<VillagerManager>().talkToVillager();
        }

        //have press enter text to open chest appear
        if (chestInRange)
        {
            OverworldGameController.gameInfo.EnterKeyTextAppear();
        }

        //destroy chest if player opens it
        if (Input.GetKeyDown(KeyCode.Return) && chestInRange)
        {
            if (OverworldGameController.gameInfo.openChest(chestNumber))
                Destroy(chest);
            chestInRange = false;
        }

        if (!villagerInRange)
        {
            if (DialogueManager.Instance.stillTalking())
            {
                DialogueManager.Instance.forceEndDialogue();
            }
        }

        //have enter prompt dissapear
        if (!chestInRange && !villagerInRange)
        {
            OverworldGameController.gameInfo.EnterKeyTextDisappear();
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

            OverworldGameController.gameInfo.setBossStatus(other.gameObject.GetComponent<RobotController>().isBoss);

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
            villagerInRange = true;
            villagerName = other.name;
        }

        //record if player in radius of opening a chest
        if(other.tag == "Chest")
        {
            chest = other.gameObject;
            chestInRange = true;
            chestNumber = int.Parse(other.name.Substring(other.tag.Length, 1));
        }
    }

    public string getVillagerName()
    {
        return villagerName;
    }

    private void OnTriggerExit(Collider other)
    {
        //If player leaves the villager radius, it is no longer in range
        if (other.tag == "Villager")
        {
            villagerInRange = false;
        }

        if (other.tag == "Chest")
        {
            chestInRange = false;
        }
    }

    IEnumerator WaitFor()
    {
        yield return new WaitForSeconds(3);

        //Load the fix it scene
        SceneManager.LoadScene(1);
    }
}