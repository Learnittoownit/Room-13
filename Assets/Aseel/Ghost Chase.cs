using UnityEngine;
using UnityEngine.AI;
public class enemy : MonoBehaviour
{
    public float enemySpeed;
    public NavMeshAgent enemyAgent;
    public Transform playerPose;

    void Start()
    {

    }


    void Update()
    {
        //transform.position -= new Vector3(0, 0, enemySpeed);
        enemyAgent.SetDestination(playerPose.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
