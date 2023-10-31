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
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
     
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            currentSpeed = baseSpeed - 1f;
        }
        else
        {
            currentSpeed = baseSpeed;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprint;
        }

        Transform camTransform = Camera.main.transform;
        Vector3 camPosition = new Vector3(camTransform.position.x, transform.position.y, camTransform.position.z);
        Vector3 direction = (transform.position - camPosition).normalized;
        Vector3 forwardMovement = direction * Input.GetAxis("Vertical");
        Vector3 horizontalMovement = camTransform.right * Input.GetAxis("Horizontal");
        movement = Vector3.ClampMagnitude(forwardMovement + horizontalMovement, 1);
        transform.Translate(movement * currentSpeed * Time.deltaTime, Space.World);

    }
}

