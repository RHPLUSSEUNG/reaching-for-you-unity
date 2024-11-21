using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    public float noteSpeed = 300;

    Image noteImage;

    void OnEnable()
    {
        if(noteImage == null)
            noteImage = GetComponent<Image>();
        
        noteImage.enabled = true;
    }

    public void HideNote()
    {
        noteImage.enabled = false;
    }

    void Update()
    {
        transform.localPosition += Vector3.right * (noteSpeed * (NoteManager.timeLimit / NoteManager.remainTime)) * Time.deltaTime;
    }
}
