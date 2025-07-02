using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour
{
    public float enemySpeed = 3.5f;          // Default speed
    public NavMeshAgent enemyAgent;          // Assign in Inspector or via code
    public Transform playerPose;             // Assign the Player's Transform

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
    }

    void Update()
    {
        if (playerPose != null && enemyAgent != null)
        {
            enemyAgent.SetDestination(playerPose.position);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Ghost caught the player!");
            // Damage the player
            PlayerMovement2 player = other.GetComponent<PlayerMovement2>();
            if (player != null)
            {
                player.TakeDamage(1); // Reduce player health by 1
                Debug.Log("Ghost caught the player!");

            }
        }
    }
}