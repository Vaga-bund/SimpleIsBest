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

    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        // Input left, right Check
        horizontalMove = Input.GetAxis("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        // Input jump
        if(Input.GetButtonDown("Jump"))
        {
            animator.SetBool("IsJumping", true);
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

        animator.SetFloat("yVelocity", rb.velocity.y);
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
        animator.SetBool("IsJumping", !controller.m_Grounded);
        controller.Move(horizontalMove * Time.fixedDeltaTime, isCrouching, isJumping);
        isJumping = false;
    }
}