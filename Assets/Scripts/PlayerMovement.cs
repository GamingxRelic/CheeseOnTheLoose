using Unity.VisualScripting;
using UnityEditor.EditorTools;
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
            // transform.SetPositionAndRotation(teleportPosition, teleportRotation);
            transform.position = teleportPosition;
            isTeleporting = false;
        }
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
