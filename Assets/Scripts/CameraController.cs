using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 targetPosition;
    public PlayerMovement playermovement;
    public float distance = 3.0f;
    public float sensitivity = 2.0f;
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;
    public Vector3 playerPosition;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void awake()
    {
        playerPosition = transform.localPosition.normalized; 
    }

    // Update is called once per frame
    void Update()
    {
        HandleCameraInput();
        
        if (Input.GetKeyDown(KeyCode.LeftShift)) //sprint bind to move camera back
        {
            distance = distance + 0.5f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) // reset camera back to normal
        {
            distance = 3.0f;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl)) //crouch bind to move camera forward
        {
            distance = distance - 0.5f;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl)) // reset camera back to normal
        {
            distance = 3.0f;
        }

        LayerMask wall = LayerMask.GetMask("Wall"); // check the layer that it is a wall

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), 2.8f, wall)) // fire a raycast from the camera to the player and if there is something between that distance then...
        {
            //Debug.Log("Hit wall");
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.green, wall);
        }
    }
    void LateUpdate()
    {
        FollowTarget(); // have the camera in its fixed position from the camera after the input has been dealt with and the distance from the player has been decided based on the input
    }

    void HandleCameraInput() //get input from mouse and translate it to the movement of the camera
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity; //get x input from mouse
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity; //get y input from mouse
        rotationX -= mouseY; //inverts or reverts the movement in relation to the game
        rotationY += mouseX; //inverts or reverts the movement in relation to the game 
        if (distance > 2.0f)
        {
            rotationX = Mathf.Clamp(rotationX, 0, 8); //restricts the movement of the camera so the player cannot look through the ceiling
        }
        else
        {
            rotationX = Mathf.Clamp(rotationX, 0, 14); //restricts the movement of the camera so the player cannot look through the floor
        }
        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0); 
        target.Rotate(Vector3.up * -mouseX); //rotates the player object in relation to the movement
    }
    void FollowTarget() // allows for the 3rd person aspect
    {
        targetPosition = target.position - transform.forward * distance; // the desired location of the camera according to the input or movement as a result
        transform.position = targetPosition; // moves the camera back that much
    }
    
}

