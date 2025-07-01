using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BabyScaryTrigger : MonoBehaviour
{
    public GameObject ghostPrefab;
    public AudioClip ghostSound;
    public GameObject messagePanel;
    public float messageDuration = 1.5f;
    public float shakeDuration = 0.3f;
    public float shakeMagnitude = 0.1f;

    private GameObject spawnedGhost;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        messagePanel.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TriggerScare(other.transform));
        }
    }

    IEnumerator TriggerScare(Transform player)
    {
        // تفعيل الرسالة
        messagePanel.SetActive(true);
        yield return new WaitForSeconds(messageDuration);
        messagePanel.SetActive(false);

        // توليد الجني أمام اللاعب
        Vector3 spawnPosition = player.position + player.forward * 1.5f;
        spawnPosition.y = 1f;

        // إنشاء الجني وتدويره
        Quaternion rotation = Quaternion.LookRotation(-player.forward); // يواجه اللاعب
        rotation *= Quaternion.Euler(0f, 180f, 0f); // تدوير إضافي 180 درجة

        spawnedGhost = Instantiate(ghostPrefab, spawnPosition, rotation);

        // تشغيل الصوت
        if (ghostSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(ghostSound);
        }

        // اهتزاز الكاميرا
        StartCoroutine(ShakeCamera());

        // حذف الجني بعد ثانيتين
        yield return new WaitForSeconds(2f);
        Destroy(spawnedGhost);
    }

    IEnumerator ShakeCamera()
    {
        Transform cameraTransform = Camera.main.transform;
        Vector3 originalPos = cameraTransform.localPosition;

        float elapsed = 0.0f;
        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            cameraTransform.localPosition = new Vector3(x, y, originalPos.z);
            elapsed += Time.deltaTime;

            yield return null;
        }

        cameraTransform.localPosition = originalPos;
    }
}
