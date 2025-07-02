using UnityEngine;
using UnityEngine.SceneManagement; // ✅ لإعادة تحميل المشهد

public class PlayerMovement2 : MonoBehaviour
{
    public bool jump;
    public float JumpSpeed;
    public float PlayerSpeed;
    public float SprintSpeed = 5f;
    public GameObject PlayerCamera;
    public Rigidbody PlayerRB;

    public AudioSource footstepAudio;

    private float rotationCam = 0f;
    public int health = 1;

    private bool isDead = false; // ✅ منع تكرار الموت

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        if (footstepAudio != null)
        {
            footstepAudio.loop = true;
            footstepAudio.playOnAwake = false;
        }
    }

    void Update()
    {
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? SprintSpeed : PlayerSpeed;

        float horoizontal = Input.GetAxis("Horizontal") * currentSpeed;
        float vertical = Input.GetAxis("Vertical") * currentSpeed;

        Vector3 direction = transform.forward * vertical + transform.right * horoizontal;
        direction.y = PlayerRB.linearVelocity.y;

        PlayerRB.linearVelocity = direction;

        if ((horoizontal != 0 || vertical != 0) && jump)
        {
            if (!footstepAudio.isPlaying)
                footstepAudio.Play();
        }
        else
        {
            if (footstepAudio.isPlaying)
                footstepAudio.Stop();
        }

        if (Input.GetKeyDown(KeyCode.Space) && jump)
        {
            PlayerRB.AddForce(new Vector3(0, JumpSpeed, 0), ForceMode.Impulse);
            jump = false;
        }

        MoveCamera();
        HandleInteraction();
    }

    void MoveCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(new Vector3(0, mouseX, 0));

        float mouseY = Input.GetAxis("Mouse Y");
        rotationCam -= mouseY;
        rotationCam = Mathf.Clamp(rotationCam, -90f, 90f);

        PlayerCamera.transform.localRotation = Quaternion.Euler(rotationCam, 0, 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        jump = true;
    }

    void HandleInteraction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 3f))
            {
                Debug.Log("ضغطت على: " + hit.collider.name);
                hit.collider.gameObject.SendMessage("OnInteract", SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    // 🛡️ دالة الضرر
    public void TakeDamage(int damage)
    {
        if (isDead) return; // ✅ منع تكرار الضرر بعد الموت

        health -= damage;
        Debug.Log("Player took damage! Current health: " + health);

        if (health <= 0)
        {
            isDead = true;
            Debug.Log("💀 Player is dead. Game Over.");

            // ✅ إعادة تحميل المشهد الحالي بعد 2 ثانية
            Invoke("ReloadScene", 2f);
        }
    }

    void ReloadScene()
    {
        Time.timeScale = 0;
    }
}
