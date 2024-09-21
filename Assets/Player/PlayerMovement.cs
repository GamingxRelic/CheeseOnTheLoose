using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Components")]    
    [SerializeField] private CharacterController controller;
    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] private float standardHeight = 2.0f;
    [SerializeField] private float crouchHeight = 1.4f;
    [SerializeField] private HeadCollisionCheck headCollisionCheck;

    [SerializeField] private GameObject mesh; 

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 5f; 
    [SerializeField] private float sprintSpeed = 7f; 
    [SerializeField] private float crouchSpeed = 3f; 
    [SerializeField] private float airControlMovementMultiplier = 0.65f;
    [SerializeField] private float jumpForce = 15f;

    private bool isCrouching = false;
    private bool isSprinting = false;

    private float moveSpeed;

    private float horizontalInput, verticalInput;

    [Header("Gravity")]
    [SerializeField] private float gravity = 9.8f;

    [Header("Jumping")]
    [SerializeField] private GroundedCheck groundedCheck;

    private Vector3 movement; 

    [Header("Teleporting")]
    public bool isTeleporting = false;
    private Vector3 teleportPosition;
    public Vector2 teleportRotation;


    void Update()
    {
        GetInput(); 
        Movement();
        HandleJump();
        ApplyMovement();
    }

    private void FixedUpdate()
    {
        if(isTeleporting) {
            transform.position = teleportPosition;
            isTeleporting = false;
        }
    }

    private void GetInput() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(IsGrounded()) {
            if(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.C) || (headCollisionCheck.IsHeadColliding() && isCrouching)) { // Crouch when L.ctrl is held or C is held.
                isCrouching = true;
                isSprinting = false;

                capsuleCollider.height = crouchHeight;
                controller.height = crouchHeight;   
                capsuleCollider.center = new Vector3(0f, (crouchHeight-standardHeight)/2.0f, 0f);
                mesh.transform.localScale = new Vector3(1.0f, 0.8f, 1.0f);

            } else {
                isCrouching = false;

                capsuleCollider.height = standardHeight;
                capsuleCollider.center = Vector3.zero;
                controller.height = standardHeight;
                mesh.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            }


            
            if(Input.GetKey(KeyCode.LeftShift) && !isCrouching) { // Toggle sprint when left shift is pressed
                isSprinting = true;
            }
            else {
                isSprinting = false;
            }
        }
        else {
            isCrouching = false;
        }
    }

    private void Movement() {
        Vector3 moveDir = transform.forward * verticalInput + transform.right * horizontalInput;
        float moveDirY = movement.y;

        // Set speeds based on movement type [walk, sprint, crouch]
        if(isCrouching) {
            moveSpeed = crouchSpeed;
        }
        else if(isSprinting) {
            moveSpeed = sprintSpeed;
        } 
        else {
            moveSpeed = walkSpeed;
        }

        if(IsGrounded()) {
            movement = moveDir.normalized * moveSpeed;
        } 
        else {
            movement = moveDir.normalized * moveSpeed * airControlMovementMultiplier;
        }
        
        movement.y = moveDirY;
    }

    private void HandleJump() {
        if(Input.GetButtonDown("Jump") && IsGrounded() && !(headCollisionCheck.IsHeadColliding() && isCrouching)) { 
            // If jump is pressed, the player is grounded, and the head collider is not colliding with anything while the player is crouched,
            // then set the movement.y delta to jumpForce. 
            movement.y = jumpForce;
        }
    }

    [Tooltip("Applies any movement changes ")]
    private void ApplyMovement() {
        if(!IsGrounded()) {
            movement.y -= gravity * Time.deltaTime;
        }

        controller.Move(movement * Time.deltaTime);
    }

    [Tooltip("Returns if the player is on the ground or not")]
    private bool IsGrounded() {
        return groundedCheck.GetColliderCount() > 0; 
    }

    [Tooltip("Used for teleporting the player to a position")]
    public void TeleportPlayer(Vector3 newPosition, Vector2 newRotation) {
        teleportPosition = newPosition;
        teleportRotation = newRotation;
        isTeleporting = true;
    }
    
// madison was here 
}
