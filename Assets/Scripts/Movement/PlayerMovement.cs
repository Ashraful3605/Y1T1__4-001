using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float baseSpeed = 4f;
    public float currentSpeed;
    public float sprint = 6f;
    Rigidbody rb;
    private Vector3 startPos;
    public Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position; //sets the starting position of the character for use later
        rb = GetComponent<Rigidbody>(); //sets a rigidbody to the rb variable
     
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl)) //the actual speed of the movement when crouching is decided 
        {
            currentSpeed = baseSpeed - 2f;
        }
        else // if neither pressed, then there should be no change but if there is a change then it will set it back to the original value
        {
            currentSpeed = baseSpeed;
        }

        if (Input.GetKey(KeyCode.LeftShift)) //the actual speed of the movement when sprinting is decided
        {
            currentSpeed = sprint;
        }
        //whole section deals with moving the player in accordance to the camera, where the front is decided by the opposite position of the camera in terms of x and moving forward according to the input and which was is forward.
        Transform camTransform = Camera.main.transform;
        Vector3 camPosition = new Vector3(camTransform.position.x, transform.position.y, camTransform.position.z);
        Vector3 direction = (transform.position - camPosition).normalized;
        Vector3 forwardMovement = direction * Input.GetAxis("Vertical");
        Vector3 horizontalMovement = camTransform.right * Input.GetAxis("Horizontal");
        movement = Vector3.ClampMagnitude(forwardMovement + horizontalMovement, 1);
        transform.Translate(movement * currentSpeed * Time.deltaTime, Space.World);

    }
}

