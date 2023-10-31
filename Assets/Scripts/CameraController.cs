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


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleCameraInput();
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            distance = distance + 0.5f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            distance = 3.0f;
        }

        LayerMask wall = LayerMask.GetMask("Wall");

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), 2.8f, wall))
        {
            Debug.Log("Hit wall");
        }
    }
    void LateUpdate()
    {
        FollowTarget();
    }

    void HandleCameraInput()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;
        rotationX -= mouseY;
        rotationY += mouseX;
        if (distance > 2.0f)
        {
            rotationX = Mathf.Clamp(rotationX, 0, 8);
        }
        else
        {
            rotationX = Mathf.Clamp(rotationX, 0, 14);
        }
        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);
        target.Rotate(Vector3.up * -mouseX);
    }
    void FollowTarget()
    {
        targetPosition = target.position - transform.forward * distance;
        transform.position = targetPosition;
    }
    
}

