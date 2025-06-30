using UnityEngine;

public class InteractTriggerA : MonoBehaviour
{
    public GameObject interactText;
    public GameObject notePanel;
    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Pressed E");

            
            bool isActive = notePanel.activeSelf;
            notePanel.SetActive(!isActive);

           
            interactText.SetActive(isActive);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactText.SetActive(true);
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactText.SetActive(false);
            playerInRange = false;

         
            notePanel.SetActive(false);
        }
    }
}