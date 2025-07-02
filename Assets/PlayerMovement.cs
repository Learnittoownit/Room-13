using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public bool jump;
    public float JumpSpeed;
    public float PlayerSpeed;
    public GameObject PlayerCamera;
    public Rigidbody PlayerRB;

    public int health = 1; // Player starts with 1 life
    float rotationCam = 0;

    void Start()
    {
        // Optional initialization
    }

    void Update()
    {
        // 🔹 Sprinting
        float currentSpeed = PlayerSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = 5f; // Sprint speed
        }

        // 🔹 Movement
        float horizontal = Input.GetAxis("Horizontal") * currentSpeed;
        float vertical = Input.GetAxis("Vertical") * currentSpeed;

        Vector3 movement = new Vector3(horizontal, PlayerRB.linearVelocity.y, vertical);
        PlayerRB.linearVelocity = movement;

        // 🔹 Jumping
        if (Input.GetKeyDown(KeyCode.Space) && jump)
        {
            PlayerRB.AddForce(new Vector3(0, JumpSpeed, 0), ForceMode.Impulse);
            jump = false;
        }

        MoveCamera();
    }

    void MoveCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(new Vector3(0, mouseX, 0));

        float mouseY = Input.GetAxis("Mouse Y");
        rotationCam -= mouseY;
        PlayerCamera.transform.localRotation = Quaternion.Euler(rotationCam, 0, 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        jump = true;
    }

    // 🔹 Take Damage
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player Died!");
        gameObject.SetActive(false);
        Invoke("RestartScene", 2f); // Restart after 2 seconds
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
