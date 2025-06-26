using UnityEngine;
using Unity.Hierarchy;

public class PlayerMovement : MonoBehaviour
{
    public bool jump;
    public float JumpSpeed;
    public float PlayerSpeed;
    public GameObject PlayerCamera;
    public Rigidbody PlayerRB;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        float horoizontal = Input.GetAxis("Horizontal") * PlayerSpeed;
        float vertical = Input.GetAxis("Vertical") * PlayerSpeed;

        PlayerRB.linearVelocity = new Vector3(horoizontal, PlayerRB.linearVelocity.y, vertical);

        if (Input.GetKeyDown(KeyCode.Space) && jump)
        {
            PlayerRB.AddForce(new Vector3(0, JumpSpeed, 0), ForceMode.Impulse);
            jump = false;
        }

        MoveCamera();

    }

    float rotationCam = 0;

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


        //Â‰« ›Ì «‰Â Ì «ﬂœ «‰ «·«⁄» „ÊÃÊœ ⁄·Ï «·«—÷ Ê·« ·«
        jump = true;
    }

}

