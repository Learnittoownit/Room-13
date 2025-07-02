using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool jump;
    public float JumpSpeed;
    public float PlayerSpeed;
    public GameObject PlayerCamera;
    public Rigidbody PlayerRB;

    public AudioSource footstepAudio; // ✅ مصدر صوت الخطوات

    private float rotationCam = 0f;

    // 🩸 الصحة (1 فقط)
    public int health = 1;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined; // ✅ الماوس ظاهر داخل نافذة اللعبة
        Cursor.visible = true;

        if (footstepAudio != null)
        {
            footstepAudio.loop = true; // تأكد من التكرار
            footstepAudio.playOnAwake = false;
        }
    }

    void Update()
    {
        float horoizontal = Input.GetAxis("Horizontal") * PlayerSpeed;
        float vertical = Input.GetAxis("Vertical") * PlayerSpeed;

        Vector3 direction = transform.forward * vertical + transform.right * horoizontal;
        direction.y = PlayerRB.linearVelocity.y;

        PlayerRB.linearVelocity = direction;

        // 🚶‍♂️ تشغيل/إيقاف صوت الخطوات حسب الحركة
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
        HandleInteraction(); // ✅ تفاعل مع الأشياء عند الضغط
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
        if (Input.GetMouseButtonDown(0)) // زر الفأرة الأيسر
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 3f)) // ✅ المدى 3 وحدات
            {
                Debug.Log("ضغطت على: " + hit.collider.name);

                // مثال: لو فيه سكربت معين على الهدف
                hit.collider.gameObject.SendMessage("OnInteract", SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    // 🛡️ دالة الضرر
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Player took damage! Current health: " + health);

        if (health <= 0)
        {
            Debug.Log("💀 Player is dead. Game Over.");
            // تقدر تضيف هنا: إعادة المشهد، تجميد اللاعب، أو شاشة خسارة
        }
    }
}

