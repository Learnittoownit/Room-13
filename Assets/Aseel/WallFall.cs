using UnityEngine;

public class WallFall : MonoBehaviour
{
    public float delay ; 

    void Start()
    {
        Invoke("HideWall", delay);
    }

    void HideWall()
    {
        gameObject.SetActive(false); 
    }
}