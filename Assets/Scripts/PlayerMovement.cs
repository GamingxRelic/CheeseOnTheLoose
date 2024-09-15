using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 300f; 
    [SerializeField] private float airControlMovementMultiplier = 0.65f;
    [SerializeField] private float jumpForce = 15f;

    private float horizontalInput, verticalInput;

    [Header("Gravity")]
    [SerializeField] private float gravity = 9.8f;
    // private int groundColliderCount;
    // [SerializeField] private LayerMask whatIsGround;

    [Header("Jumping")]
    [SerializeField] private GroundedCheck groundedCheck;

    private Vector3 movement; 

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

    private void Movement() {
        Vector3 moveDir = transform.forward * verticalInput + transform.right * horizontalInput;
        float moveDirY = movement.y;

        if(IsGrounded()) {
            movement = moveDir.normalized * moveSpeed;
        } 
        else {
            movement = moveDir.normalized * moveSpeed * airControlMovementMultiplier;
        }

        movement.y = moveDirY;
    }

    private void HandleJump() {
        if(Input.GetButtonDown("Jump") && IsGrounded()) {
            movement.y = jumpForce;
        }
    }

    private void ApplyMovement() {
        if(!IsGrounded()) {
            movement.y -= gravity * Time.deltaTime;
        }

        controller.Move(movement * Time.deltaTime);
    }

    private bool IsGrounded() {
        return groundedCheck.GetColliderCount() > 0; 
    }
    
// madison was here 
}
