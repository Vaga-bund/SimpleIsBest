using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovemet : MonoBehaviour
{
    public PlayerController controller;

    public float runSpeed = 20.0f;
    float horizontalMove = 0.0f;

    private bool isJumping = false;
    private bool isCrouching = false;
    // Update is called once per frame
    void Update()
    {
        // Input left, right Check
        horizontalMove = Input.GetAxis("Horizontal") * runSpeed;

        // Input jump
        if(Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }

        // Input Crouch
        if(Input.GetButtonDown("Crouch"))
        {
            isCrouching = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            isCrouching = false;
        }
    }

    private void FixedUpdate()
    {
        // playerControl Move
        controller.Move(horizontalMove * Time.fixedDeltaTime, isCrouching, isJumping);
        isJumping = false;
    }
}
