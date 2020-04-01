using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float shortJumpHeight = 2.5f;
    public float longJumpHeight = 3.1f;
    public float sprintSpeed = 1.5f;

    public float delay = 0.5f;
    float startTime;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundmask;
    public bool isGrounded;

    public Transform wallCheckLeft;
    public Transform wallCheckRight;
    public float wallDistance = 0.4f;
    public float wallGravity = -1.5f;
    public LayerMask Wallmask;
    public bool isWallrunningLeft;
    public bool isWallrunningRight;

    bool jumpOnce = false;

    Vector3 velocity;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundmask);
        isWallrunningLeft = Physics.CheckSphere(wallCheckLeft.position, wallDistance, Wallmask);
        isWallrunningRight = Physics.CheckSphere(wallCheckRight.position, wallDistance, Wallmask);


        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        if(!isGrounded && isWallrunningLeft && velocity.y < 0) {
            velocity.y = wallGravity;
        } else if(!isGrounded && isWallrunningRight && velocity.y < 0) {
            velocity.y = wallGravity;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Input.GetButton("Running")) {
            z *= sprintSpeed;
        }

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump")) {
            startTime = Time.time;
        }

        Debug.Log("Before if: " + Time.time + " "+ jumpOnce);

        if (!jumpOnce && (isGrounded || isWallrunningLeft || isWallrunningRight)) {
            if (Input.GetButtonUp("Jump") && Time.time - startTime < delay) {
                Debug.Log("Short jump");
                velocity.y = Mathf.Sqrt(shortJumpHeight * -2f * gravity);
                jumpOnce = true;
            }
            else if (Input.GetButton("Jump") && Time.time - startTime > delay) {
                Debug.Log("Long jump");
                velocity.y = Mathf.Sqrt(longJumpHeight * -2f * gravity);
                jumpOnce = true;
            }
        }
        else if(!isGrounded && !isWallrunningLeft && !isWallrunningRight){
            jumpOnce = false;
        }

        Debug.Log("after if: " + Time.time + " " + jumpOnce);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    } 
}
