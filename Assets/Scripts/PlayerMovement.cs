using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform front;
    public float speed = 10f;
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
        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector3(-zMove, rb.velocity.y, xMove) * speed;
    }
}
