﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float gravity = 9.8f;
    public float jumpForce = 300f;
    public float speed = 5f;
    private bool isGrounded = true;
    private bool isJumping = false;
    private float verticalVelocity;
    private float jumpStartTime;
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0f, moveZ);
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            isJumping = true; isJumping = true;
            isGrounded = false;
            verticalVelocity = jumpForce; 
            jumpStartTime = Time.time;    
        }
    }
    private void FixedUpdate()
    {
        if (isJumping)
        {
            float timeElapsed = Time.time - jumpStartTime; 
            verticalVelocity = jumpForce - gravity * timeElapsed; // v_y = v_0 - g * t
            float heightChange = verticalVelocity * Time.deltaTime; 

         
            transform.position += new Vector3(0, heightChange, 0);

           
            if (transform.position.y <= 0f)
            {
                isJumping = false;
                isGrounded = true;
                transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
                verticalVelocity = 0f; 
            }
        }
         void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
            }
        }
    }
}