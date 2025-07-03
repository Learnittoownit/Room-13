using UnityEngine;

public class ShadowMover : MonoBehaviour
{
    public float speed = 5f;
    public Vector3 moveDirection = Vector3.right;
    public float moveDistance = 5f;
    public AudioSource moveAudioSource; // ✅ مصدر الصوت الجاهز

    private Vector3 startPosition;
    private bool moving = false;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (moving)
        {
            transform.position += moveDirection.normalized * speed * Time.deltaTime;

            if (Vector3.Distance(startPosition, transform.position) >= moveDistance)
            {
                gameObject.SetActive(false);
                moving = false;
            }
        }
    }

    public void StartMoving()
    {
        Debug.Log("الظل بدأ يتحرك");
        moving = true;

        if (moveAudioSource != null && !moveAudioSource.isPlaying)
        {
            moveAudioSource.Play();
        }
    }
}