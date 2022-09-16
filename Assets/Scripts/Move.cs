using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed = 5f;
    public CharacterController characterController;


    private float gravity = -9.81f * 3f;
    private Vector3 velocity;
    private float jumpHeight = 1f;
    GroundCheck check;

    private float runSpeed = 9f;
    public KeyCode runningKey = KeyCode.LeftShift;
    private void Start()
    {
        check = GetComponentInChildren<GroundCheck>();      
    }
    void Update()
    {
        float characterSpeed =  moveSpeed;

        Vector3 move = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
        if (Input.GetKey(runningKey) && check.isGrounded)
        {
            characterSpeed = runSpeed;
        }
        characterController.Move(move * characterSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime ;


        if (Input.GetButtonDown("Jump") && check.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (check.isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }
        characterController.Move(velocity * Time.deltaTime);

        


    }
}