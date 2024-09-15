using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float mouseSensX, mouseSensY;
    
    private float xRot, yRot;

    [SerializeField] private Transform playerOrientation;


    void Start()
    {
        // Lock the cursor to stay ingame
        Cursor.lockState = CursorLockMode.Locked;   
        Cursor.visible = false;
    }

    void Update()
    {
        // Set the mouseX and Y each frame so the camera rotates accordingly. 
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensX; //* Time.deltaTime 
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensY; //* Time.deltaTime 

        yRot += mouseX;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        playerOrientation.rotation = Quaternion.Euler(0, yRot, 0);


    }
}
