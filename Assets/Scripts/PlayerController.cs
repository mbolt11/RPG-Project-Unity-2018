using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 5f;

    private Rigidbody rb;
    private float moveInput;
    private float turnInput;

	// Use this for initialization
	void Start ()
    {
        //store the rigidbody component
        rb = GetComponent<Rigidbody>();
	}

    private void Update()
    {
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }

    void FixedUpdate ()
    {
        MovePlayer();

	}

    private void MovePlayer()
    {
        Vector3 movement = transform.forward * moveInput * playerSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }
}
