using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrller : MonoBehaviour
{
    [Header("Character")]
    public float speed = 12f;
    public float gravity = -0.981f * 2;
    public float jumpHeight = 1.5f;
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = 0.4f;
    private CharacterController controller;
    private Vector3 velocity;
    private Animator animator;

    private int isJumping;
    



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        isJumping = Animator.StringToHash("isJumping");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsCheckGround() && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = (transform.right * x + transform.forward * z);
        controller.Move(move * speed * Time.deltaTime);
        if (Input.GetButtonDown("Jump") && IsCheckGround())
        {
            if (animator != null)
            {
                animator.SetTrigger(isJumping);
            }

            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        AnimatorMovementInput(IsCheckGround());
    }
    bool IsCheckGround()
    {
        bool ground = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        return ground;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }

    private void AnimatorMovementInput(bool isGrounded)
    {
            if (animator != null)
        {
            if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
            {
                animator.SetBool("isWalking", true);
            }
            else animator.SetBool("isWalking", false);
            animator.SetBool("inAir", !isGrounded); // Nếu không đứng trên mặt đất thì kích hoạt 
            }

    }
}
