using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool jump;
    public float JumpSpeed;
    public float PlayerSpeed;
    public Rigidbody PlayerRB;
    void Start()
    {
        
    }

   
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

        void OnCollisionEnter(Collision collision)
        {


            //Â‰« ›Ì «‰Â Ì «ﬂœ «‰ «·«⁄» „ÊÃÊœ ⁄·Ï «·«—÷ Ê·« ·«
            jump = true;
        }

    }
}
