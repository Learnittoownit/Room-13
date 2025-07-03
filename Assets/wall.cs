using UnityEngine;

public class wall : MonoBehaviour
{
   
    public float delay = 20f; // تقدر تغيرها من الـ Inspector

    void Start()
    {
        Invoke("HideWall", delay);
    }

    void HideWall()
    {
        gameObject.SetActive(false);
    }
}