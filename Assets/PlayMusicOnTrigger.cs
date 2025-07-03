using UnityEngine;

public class PlayMusicOnTrigger : MonoBehaviour
{
    public AudioSource musicSource;      // مصدر الصوت (فيه الأغنية)
    public string playerTag = "Player";  // تأكد اللاعب عليه هذا التاغ
    private bool hasPlayed = false;      // علشان تشتغل مرة وحدة

    private void OnTriggerEnter(Collider other)
    {
        if (!hasPlayed && other.CompareTag(playerTag))
        {
            hasPlayed = true;
            musicSource.Play();
            Debug.Log("🎵 الأغنية بدأت!");
        }
    }
}