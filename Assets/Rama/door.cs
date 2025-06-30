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
        // قفل الباب أول شي
        Door.SetActive(false);

        // تشغيل صوت الصرخة
        screamSound.Play();

        // تشغيل حركة الظل
        shadow.SetActive(true);
        shadow.GetComponent<ShadowMover>().StartMoving();

        // ننتظر delayBeforeOpen (مثلاً 3 ثواني)
        yield return new WaitForSeconds(delayBeforeOpen);
        Door.SetActive(true);
        // ننتظر 15 ثانية إضافية
        yield return new WaitForSeconds(15f);

        // فتح الباب بعد الانتظار
        Door.SetActive(false);
    }

}