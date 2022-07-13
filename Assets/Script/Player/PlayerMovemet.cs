using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovemet : MonoBehaviour
{
    [Header("Controller")]
    [SerializeField] private PlayerController controller;

    [Header("Animator")]
    [SerializeField] private Animator animator;
    
    [Header("Status Paramater")]
    public float runSpeed = 20.0f;
    float horizontalMove = 0.0f;

    private bool isJumping = false;
    private bool isCrouching = false;
    // Update is called once per frame
    void Update()
    {
        // Input left, right Check
        horizontalMove = Input.GetAxis("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        // Input jump
        if(Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            animator.SetBool("IsJumping", true);
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

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    public void OnCrouching()
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    private void FixedUpdate()
    {
        // playerControl Move
        controller.Move(horizontalMove * Time.fixedDeltaTime, isCrouching, isJumping);
        isJumping = false;
    }
}