using UnityEngine;

public class PlayerNoteScript : MonoBehaviour
{
    private NoteScript activeNote;
    private GameObject InteractText;
    void Start()
    {
        InteractText = GameObject.Find("InteractText");
        InteractText.SetActive(false);

    }

    void Update()
    {
        if (activeNote)
        {
            if (Input.GetKeyDown("e"))
            {
                activeNote.ToggleNote();

            }
        }
    }
    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Note")
        {
            c.gameObject.TryGetComponent(out activeNote);
            InteractText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider c)
    {
        if(c.gameObject.tag == "Note")
        {
            if (activeNote.GetNoteStatus())
           activeNote.ToggleNote();
            activeNote=null;
            InteractText.SetActive(false);
        }
    }

}
