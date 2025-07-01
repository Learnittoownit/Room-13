using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScaryTrigger : MonoBehaviour
{
    public GameObject ghostPrefab;
    public AudioClip ghostSound;
    public GameObject messagePanel;
    public float messageDuration = 1.5f;
    public float shakeDuration = 0.3f;
    public float shakeMagnitude = 0.1f;

    private GameObject spawnedGhost;
    private AudioSource audioSource;
    private bool triggered = false;
    private Transform mainCamera;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        if (messagePanel != null)
            messagePanel.SetActive(false);

        mainCamera = Camera.main.transform;
    }

    void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(ScareSequence(other.transform));
        }
    }

    IEnumerator ScareSequence(Transform player)
    {
        // تظهر الرسالة
        if (messagePanel != null)
            messagePanel.SetActive(true);

        Debug.Log("في أحد وراك...");

        yield return new WaitForSeconds(messageDuration);

        if (messagePanel != null)
            messagePanel.SetActive(false);

        // نرسبن الجنية أمام اللاعب وعلى مستوى الأرض
        Vector3 forwardOffset = player.forward * 1.5f;
        Vector3 spawnPosition = player.position + forwardOffset;
        spawnPosition.y = 0f; // نثبتها بالأرض مباشرة

        spawnedGhost = Instantiate(ghostPrefab, spawnPosition, Quaternion.identity);

        // نخلي وجهها يطالع اللاعب
        Vector3 directionToPlayer = (player.position - spawnPosition).normalized;
        directionToPlayer.y = 0;
        spawnedGhost.transform.rotation = Quaternion.LookRotation(directionToPlayer);

        // نلفها 180 درجة لو الموديل معطي ظهره
        spawnedGhost.transform.Rotate(0f, 180f, 0f);

        // نشغل صوت الرعب
        if (ghostSound != null)
            audioSource.PlayOneShot(ghostSound);

        // نهز الكاميرا
        if (mainCamera != null)
            StartCoroutine(ShakeCamera());

        yield return new WaitForSeconds(2f);

        Destroy(spawnedGhost);
    }

    IEnumerator ShakeCamera()
    {
        Vector3 originalPos = mainCamera.localPosition;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float offsetX = Random.Range(-1f, 1f) * shakeMagnitude;
            float offsetY = Random.Range(-1f, 1f) * shakeMagnitude;

            mainCamera.localPosition = originalPos + new Vector3(offsetX, offsetY, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        mainCamera.localPosition = originalPos;
    }
}