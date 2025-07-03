using UnityEngine;

public class HideGroundWithDelay : MonoBehaviour
{
    public string playerTag = "Player";
    public float delay = 2f; // مدة الانتظار بعد لمس اللاعب

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            Invoke("HideGround", delay);
        }
    }

    void HideGround()
    {
        gameObject.SetActive(false);
    }
}