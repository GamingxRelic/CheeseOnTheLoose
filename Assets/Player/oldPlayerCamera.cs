using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oldPlayerCamera : MonoBehaviour
{
    [SerializeField] private float mouseSensX, mouseSensY;
    [SerializeField] private PlayerMovement playerMovement;
    
    private float xRot, yRot;
    [SerializeField] private Transform playerOrientation;

    [SerializeField] private float fov = 60.0f;
    [SerializeField] private float zoomFov = 30.0f;



    private void Start()
    {
        // Lock the cursor to stay ingame
        Cursor.lockState = CursorLockMode.Locked;   
        Cursor.visible = false;

    }

    private void Update()
    {
        // Set the mouseX and Y each frame so the camera rotates accordingly. 
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensX; 
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensY; 

        yRot += mouseX;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        playerOrientation.rotation = Quaternion.Euler(0, yRot, 0);

        if(Input.GetAxisRaw("Fire2") > 0.0f){
            Camera.main.fieldOfView = zoomFov;
        }
        else {
            Camera.main.fieldOfView = fov;
        }

    }

    private void FixedUpdate()
    {
        if(playerMovement.isTeleporting) {
            yRot = playerMovement.teleportRotation.y;
            xRot = playerMovement.teleportRotation.x;
        }
    }
}
