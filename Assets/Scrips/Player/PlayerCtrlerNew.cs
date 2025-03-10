using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrlerNew : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float rotationSpeed = 20f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private bool isGround = false;
    [SerializeField] private float gravity = 0.981f * 2;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundDistance = 0.3f;
    [SerializeField] private bool isJump = false;
    [SerializeField] private Transform cameraTransform;
    private Vector3 velocity;
    private CharacterController playerCtrler;
    private Animator animator;

    void Start()
    {
        if (groundCheck == null)
        {

        }

        playerCtrler = GetComponent<CharacterController>();

        cameraTransform = Camera.main.transform;
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        MoveInput();
        //Jump();
        AnimatorMovementInput();

        ApplyGravity();
    }

    void MoveInput()
    {
        Vector3 moveDirection = Vector3.zero;

        
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            moveDirection += forward;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            moveDirection -= forward;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            moveDirection -= right;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            moveDirection += right;

        
        if (moveDirection != Vector3.zero)
        {
            moveDirection.Normalize();
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            playerCtrler.Move(moveDirection * moveSpeed * Time.deltaTime);
        }
        
    }
    private void AnimatorMovementInput()
    {
        if (animator != null)
        {
            if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
            {
                animator.SetBool("isWalking", true);
            }
            else animator.SetBool("isWalking", false);

        }

    }
    void ApplyGravity()
    {
        if (playerCtrler.isGrounded)
        {
            velocity.y = -2f;
        }
        else
        {
            velocity.y -= gravity * Time.deltaTime;
        }

        playerCtrler.Move(velocity * Time.deltaTime);
    }

}
