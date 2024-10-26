using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int appearTime = 0;
    private double currentTime = 0d;

    [SerializeField] Transform noteAppearTf = null;
    [SerializeField] GameObject goNote = null;

    TimingManager timingManager = null;
    JudgementEffect judgementEffect = null;

    void Start()
    {
        timingManager = GetComponent<TimingManager>();
        judgementEffect = GetComponent<JudgementEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= 60d / appearTime)
        {
            GameObject note = Instantiate(goNote, noteAppearTf.position, Quaternion.identity);
            note.transform.SetParent(this.transform);
            timingManager.boxNoteList.Add(note);
            currentTime -= 60d / appearTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Note"))
        {
            judgementEffect.JudgementAnim(4);

            timingManager.boxNoteList.Remove(other.gameObject);
            Destroy(other.gameObject, 0.1f);
        }   
    }
}
