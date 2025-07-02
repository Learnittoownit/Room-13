using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BabyScaryTrigger : MonoBehaviour
{
    public GameObject ghostPrefab;
    public AudioClip ghostSound;
    public GameObject messagePanel;
    public Transform spawnPoint; // ✅ نقطة ظهور مخصصة
    public float messageDuration = 1.5f;
    public float shakeDuration = 0.3f;
    public float shakeMagnitude = 0.1f;

    private GameObject spawnedGhost;
    private AudioSource audioSource;
    private bool hasTriggered = false; // ✅ عشان ما يتكرر

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        messagePanel.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true; // ✅ ما يتكرر بعد أول مرة
            StartCoroutine(TriggerScare(other.transform));
        }
    }

    IEnumerator TriggerScare(Transform player)
    {
        // تفعيل الرسالة
        messagePanel.SetActive(true);
        yield return new WaitForSeconds(messageDuration);
        messagePanel.SetActive(false);

        // استخدام نقطة الظهور بدل موقع اللاعب
        Vector3 spawnPosition = spawnPoint.position;
        Quaternion rotation = Quaternion.LookRotation(player.position - spawnPosition);
        rotation *= Quaternion.Euler(0f, 180f, 0f);

        spawnedGhost = Instantiate(ghostPrefab, spawnPosition, rotation);

        // تشغيل الصوت
        if (ghostSound != null)
        {
            audioSource.clip = ghostSound;
            audioSource.Play();
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
        Vector3 originalLocalPos = cameraTransform.localPosition;

        float elapsed = 0.0f;
        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            cameraTransform.localPosition = originalLocalPos + new Vector3(x, y, 0);
            elapsed += Time.deltaTime;

            yield return null;
        }

        cameraTransform.localPosition = originalLocalPos;
    }
}
