using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float baseSpeed = 1f;
    public float currentSpeed;
    public float sprint = 5f;
    Rigidbody rb;
    private Vector3 startPos;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
     
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentSpeed = baseSpeed + sprint;
        }
        else
        {
            currentSpeed = baseSpeed;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            currentSpeed = 0.5f;
        }
        else
        {
            currentSpeed = baseSpeed;
        }

        Transform camTransform = Camera.main.transform;
        Vector3 camPosition = new Vector3(camTransform.position.x, transform.position.y, camTransform.position.z);
        Vector3 direction = (transform.position - camPosition).normalized;
        Vector3 forwardMovement = direction * Input.GetAxis("Vertical");
        Vector3 horizontalMovement = camTransform.right * Input.GetAxis("Horizontal");
        Vector3 movement = Vector3.ClampMagnitude(forwardMovement + horizontalMovement, 1);
        transform.Translate(movement * currentSpeed * Time.deltaTime, Space.World);
    }
}
