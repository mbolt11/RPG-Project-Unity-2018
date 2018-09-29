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

    //Public variables for the transition
    public GameObject transition;
    public float pauseTime;
    private float tcounter = -598;
    private RectTransform mytransform;

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

        //Set the initial position of the black transition screen
        mytransform = transition.GetComponentInChildren<RectTransform>();
        Vector3 startpos = new Vector3(tcounter, 304, 0);
        Quaternion noturn = Quaternion.Euler(0f, 0f, 0f);
        mytransform.SetPositionAndRotation(startpos, noturn);
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

    //If the player collides with a Robot, go to the fixing scene
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Robot")
        {
            //Safety check that scene has not loaded already
            if (!loaded)
            {
                //Make transition image move across the Hud
                StartCoroutine(Transition());

                //Load the fix it scene
                SceneManager.LoadSceneAsync(1);

                //Unload the overworld scene
                SceneManager.UnloadSceneAsync(0);
            }
        }
    }

    //Coroutine for transition
    IEnumerator Transition()
    {
        while (tcounter < 598)
        {
            Vector3 movecanvas = new Vector3(tcounter, 0, 0);
            Quaternion noturn = Quaternion.Euler(0f, 0f, 0f);
            mytransform.SetPositionAndRotation(movecanvas, noturn);
            tcounter++;
            yield return new WaitForSeconds(pauseTime);
        }
    }
}