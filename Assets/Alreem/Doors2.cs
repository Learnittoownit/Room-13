using UnityEngine;
using System.Collections;

public class Doors2 : MonoBehaviour
{
    public GameObject Door; // الباب اللي بنقفل ونفتح

    public float delayBeforeOpen = 3f; // التأخير قبل فتح الباب

    private bool triggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(ScareSequence());
        }
    }


    IEnumerator ScareSequence()
    {



        // قفل الباب مؤقتًا
        Door.SetActive(false);

        // ننتظر كم ثانية
        yield return new WaitForSeconds(delayBeforeOpen);

        // إعادة فتح الباب
        Door.SetActive(true);
    }

}