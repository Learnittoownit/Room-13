using UnityEngine;
using System.Collections;

public class door : MonoBehaviour
{
    public GameObject Door; // الباب اللي بنقفل ونفتح
    public AudioSource screamSound; // صوت الصرخة
    public GameObject shadow; // كائن الظل اللي بيتحرك
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


        // تشغيل صوت الصرخة
        screamSound.Play();

        // تشغيل حركة الظل
        shadow.SetActive(true);
        shadow.GetComponent<ShadowMover>().StartMoving(); // ← هذا هو السطر اللي نتكلم عنه

        // قفل الباب مؤقتًا
        Door.SetActive(false);
        
        // ننتظر كم ثانية
        yield return new WaitForSeconds(delayBeforeOpen);

        // إعادة فتح الباب
        Door.SetActive(true);
    }

}