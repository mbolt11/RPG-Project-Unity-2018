using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //player forward/backward speed
    public float playerSpeed, playerWalkSpeed = 5f, playerRunSpeed = 10f;

    //player turn speed
    public float playerTurnSpeed;

    //Singleton
    public static PlayerController Instance { get; set; }

    private Rigidbody rb;
    private float moveInput;
    private float turnInput;

    //reference to the main camera
    public GameObject userCamera;

    //For player-villager interactions
    public GameObject villager;
    private string villagerName;
    private bool villagerInRange = false;

    private GameObject chest;
    private bool chestInRange = false;
    private int chestNumber;
    private bool inBossZone = false;

    // Use this for initialization
    void Start()
    {
        //store the rigidbody component
        rb = GetComponent<Rigidbody>();
        playerTurnSpeed = 50f;
        playerSpeed = playerWalkSpeed;

        //Instantiate the script singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        //Put the player at the correct overworld location
        transform.position = OverworldGameController.gameInfo.playerlocation;
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
            //Call OpenChest from overworld
            string toolfound = OverworldGameController.gameInfo.OpenChest(chestNumber);
            Destroy(chest);
            chestInRange = false;

            //Show the message
            if(OverworldGameController.gameInfo.numToolsFound <= 4)
            {
                DialogueManager.Instance.ChestOpenedMessage(toolfound);
            }
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

        //Player running/walking
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            playerSpeed = playerRunSpeed;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            playerSpeed = playerWalkSpeed;
        }
    }

    //physics code
    void FixedUpdate()
    {
        TurnPlayer();
        MovePlayer();
        
        //jump if space is pressed down
        if(Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.position.y) < 0.1f)
        {
            rb.AddForce(0, 25f, 0, ForceMode.Impulse);
        }
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
        if (other.tag == "Common Robot" || other.tag == "Outside Robot" || other.tag == "BigBoss")
        {
            //Save the player's position for when they come back
            if(other.tag == "BigBoss")
            {
                OverworldGameController.gameInfo.playerlocation = new Vector3(-24,0,8);
            }
            else
            {
                OverworldGameController.gameInfo.playerlocation = new Vector3(transform.position.x - .5f, transform.position.y, transform.position.z);
            }
            
            //Save this robot's name
            OverworldGameController.gameInfo.robotHitName = other.name;

            //Set the correct type of enemy when entering the fix-it
            if(other.tag == "Common Robot")
            {
                OverworldGameController.gameInfo.setEnemyRobot("Common Robot");
            }
            else if(other.tag == "Outside Robot")
            {
                OverworldGameController.gameInfo.setEnemyRobot("Outside Robot");
            }
            else if(other.tag == "BigBoss")
            {
                OverworldGameController.gameInfo.setEnemyRobot("BigBoss");
            }

            if(other.tag != "BigBoss")
            {
                OverworldGameController.gameInfo.setBossStatus(other.gameObject.GetComponent<RobotController>().isBoss);
            }

            //Safety check that scene has not loaded already
            //is this needed??
            if (!OverworldGameController.gameInfo.isSceneLoading())
            {
                OverworldGameController.gameInfo.changeScene();
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

        if (other.tag == "BossZone" && !inBossZone)
        {
            inBossZone = true;
            //Debug.Log("entered zone:" + other.name);
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

        if (other.tag == "BossZone" && inBossZone)
        {
            inBossZone = false;
            //Debug.Log("exit zone");
        }
    }

    public bool playerInBossZone()
    {
        return inBossZone;
    }
}