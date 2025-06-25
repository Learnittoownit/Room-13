using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UserMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 6f;

    private Rigidbody rb;
    private bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow))
            moveDirection += Vector3.forward;

        if (Input.GetKey(KeyCode.DownArrow))
            moveDirection += Vector3.back;

        if (Input.GetKey(KeyCode.LeftArrow))
            moveDirection += Vector3.left;

        if (Input.GetKey(KeyCode.RightArrow))
            moveDirection += Vector3.right;

        // Apply movement
        transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }
}