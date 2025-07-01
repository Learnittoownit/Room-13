using UnityEngine;

public class KeypadDoor : MonoBehaviour
{
    public Transform door; 
    public Vector3 openOffset = new Vector3(0, 75, 0); 
    public float speed = 3f;

    private Quaternion targetRotation;
    private bool shouldOpen = false;

    public void OpenDoor()
    {
        if (!shouldOpen)
        {
            targetRotation = transform.rotation * Quaternion.Euler(openOffset);
            shouldOpen = true;
        }
    }

    void Update()
    {
        if (shouldOpen)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * speed);
        }
    }
}
