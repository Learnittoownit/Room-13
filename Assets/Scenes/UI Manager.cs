using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{


    public void ReloadGame(string sceneName)
    {

        SceneManager.LoadScene(sceneName);

    }
}
