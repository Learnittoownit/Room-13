using UnityEngine;
using UnityEngine.SceneManagement; 

public class WinTrigger : MonoBehaviour
{
    public string SceneName; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}
