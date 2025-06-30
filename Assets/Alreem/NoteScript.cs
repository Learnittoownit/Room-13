using UnityEngine;

public class NoteScript : MonoBehaviour
{

    private bool noteStatus;
    public GameObject Note;

   public void ToggleNote()
    {
        noteStatus =! noteStatus;
        Note.SetActive(noteStatus);
    }
        public bool GetNoteStatus()
    {
        return noteStatus;
    }
    
}
