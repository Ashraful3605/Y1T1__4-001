using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public float distance = 5.0f;
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
        rotationY -= mouseX;
        //rotationX = Mathf.Clamp(rotationX, -90, 90);
        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);
        target.Rotate(Vector3.up * -mouseX);
    }
    void FollowTarget()
    {
        Vector3 targetPosition = target.position - transform.forward * distance;
        transform.position = targetPosition;
    }
}
