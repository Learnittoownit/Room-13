using UnityEngine;

public class LightFlickerTimed : MonoBehaviour
{
    private Light flickerLight;

    public float flickerDuration = 5f; // Flicker for 5 seconds
    public float minTime = 0.05f;
    public float maxTime = 0.2f;
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.5f;

    private void Start()
    {
        flickerLight = GetComponent<Light>();
        if (flickerLight != null)
            StartCoroutine(Flicker());
    }

    private System.Collections.IEnumerator Flicker()
    {
        float elapsed = 0f;

        while (elapsed < flickerDuration)
        {
            flickerLight.intensity = Random.Range(minIntensity, maxIntensity);
            flickerLight.enabled = (Random.value > 0.2f);
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
            elapsed += Time.deltaTime;
        }

        // Stop flickering, turn light on permanently
        flickerLight.enabled = true;
        flickerLight.intensity = maxIntensity;
    }
}


