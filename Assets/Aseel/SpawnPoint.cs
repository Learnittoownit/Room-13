using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour
{
    public GameObject scaryMessage;    
    public GameObject ghost;           
    public Transform ghostSpawnPoint;
    public float delay = 2f;            

    private bool triggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(TriggerScare());
        }
    }

    IEnumerator TriggerScare()
    {
        
        if (scaryMessage != null)
            scaryMessage.SetActive(true);

       
        yield return new WaitForSeconds(delay);

        
        if (scaryMessage != null)
            scaryMessage.SetActive(false);

       
        if (ghost != null && ghostSpawnPoint != null)
        {
            ghost.transform.position = ghostSpawnPoint.position;
            ghost.transform.rotation = ghostSpawnPoint.rotation;
            ghost.SetActive(true);
        }
    }
}