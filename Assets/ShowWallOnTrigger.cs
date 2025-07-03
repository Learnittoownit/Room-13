using UnityEngine;

public class ShowWallOnTrigger : MonoBehaviour
{
    public GameObject wallToShow;        // الحائط اللي راح يطلع
    public string playerTag = "Player";  // وسم اللاعب
    public AudioSource wallSound;        // الصوت اللي راح يشتغل
    private bool hasActivated = false;   // فلتر علشان ما يشتغل إلا مرة

    private void OnTriggerEnter(Collider other)
    {
        if (!hasActivated && other.CompareTag(playerTag))
        {
            hasActivated = true;         // ما يتكرر مرة ثانية

            wallToShow.SetActive(true);  // يظهر الحائط

            if (wallSound != null)
            {
                wallSound.Play();        // يشغل الصوت
            }

            Debug.Log("✅ أول مرة فقط: الحائط ظهر والصوت اشتغل");
        }
    }
}