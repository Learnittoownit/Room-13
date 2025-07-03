using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour
{
    public float enemySpeed = 4.7f;
    public NavMeshAgent enemyAgent;
    public Transform playerPose;

    public AudioSource ghostAudio; 
    public float maxVolume = 1f;  
    public float minVolume = 0.2f;
    public float volumeDistance = 10f; 
    void Start()
    {
        if (enemyAgent == null)
        {
            enemyAgent = GetComponent<NavMeshAgent>();
        }

        enemyAgent.speed = enemySpeed;

        if (playerPose == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                playerPose = player.transform;
            else
                Debug.LogError("Player not found! Make sure the Player is tagged 'Player'");
        }

        if (ghostAudio != null)
        {
            ghostAudio.loop = true;
            ghostAudio.playOnAwake = false;
        }
    }

    void Update()
    {
        if (playerPose != null && enemyAgent != null)
        {
            enemyAgent.SetDestination(playerPose.position);

            float distance = Vector3.Distance(transform.position, playerPose.position);

            // تشغيل الصوت عند الحركة
            if (enemyAgent.velocity.magnitude > 0.1f)
            {
                if (ghostAudio != null && !ghostAudio.isPlaying)
                {
                    ghostAudio.Play();
                }

                // 🔊 تغيير الصوت حسب المسافة
                if (ghostAudio != null)
                {
                    float t = Mathf.Clamp01(1 - (distance / volumeDistance));
                    ghostAudio.volume = Mathf.Lerp(minVolume, maxVolume, t);
                }
            }
            else
            {
                if (ghostAudio != null && ghostAudio.isPlaying)
                {
                    ghostAudio.Pause(); // أو Stop()
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Ghost caught the player!");
            PlayerMovement2 player = other.GetComponent<PlayerMovement2>();
            if (player != null)
            {
                player.TakeDamage(1);
            }
        }
    }
}