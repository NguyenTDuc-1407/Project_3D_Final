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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (IsCheckGround() && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && IsCheckGround())
        {
            animator.SetBool("isJumping", true);



            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        AnimatorMovementInput(IsCheckGround());

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            transform.Translate(-Vector3.forward * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * speed * Time.deltaTime);
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
            animator.SetBool("isJumping", !isGrounded);
        }

    }
}
