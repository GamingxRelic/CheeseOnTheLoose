using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private Rigidbody rb;

    [Header("Movement")]
    // [SerializeField] private float walkSpeed = 5f, sprintSpeed = 7f, crouchSpeed = 3f; 
    [SerializeField] private float airControlMovementMultiplier = 0.65f;
    [SerializeField] private float jumpForce = 15f;

    // private bool isCrouching = false;
    // private bool isSprinting = false;

    [SerializeField] private float moveSpeed;

    private float horizontalInput, verticalInput;
    private Vector3 movement; 

    [Header("Grounded Check")]
    [SerializeField] private Transform playerFeet; // A point at the player's feet
    [SerializeField] private float groundDistance = 0.4f; // Radius of the sphere
    [SerializeField] private LayerMask groundMask; // Layer to check (e.g., ground layer)

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Movement();
        HandleJump();
        ApplyMovement();
    }

    private void GetInput() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void HandleJump() {
        if(Input.GetButtonDown("Jump") && IsGrounded()){ 
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public bool IsGrounded() {
        return true;
    }

    private void Movement() {
        Vector3 moveDir = transform.forward * verticalInput + transform.right * horizontalInput;

        if(IsGrounded()) {
            // movement = moveDir.normalized * moveSpeed;
            rb.velocity = new Vector3(moveDir.x * moveSpeed, rb.velocity.y, moveDir.z * moveSpeed);
        } 
        else {
            rb.velocity = new Vector3(moveDir.x * moveSpeed * airControlMovementMultiplier, rb.velocity.y, moveDir.z * moveSpeed * airControlMovementMultiplier);
            // movement = moveDir.normalized * moveSpeed * airControlMovementMultiplier;
        }
        
    }

    private void ApplyMovement() {
        rb.AddForce(movement, ForceMode.Force);
    }
}
