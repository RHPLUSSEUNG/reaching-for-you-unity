using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteManager : MonoBehaviour
{
    // 제한시간
    public static float timeLimit = 30f;
    public static float remainTime = 30f;

    [SerializeField]
    Slider timeSlider;
    [SerializeField]
    GameObject rankingPanel;

    // 노트 출현
    public int appearTime = 0;
    private double currentAppearTime = 0d;

    [SerializeField] Transform noteAppearTf = null;
    [SerializeField] GameObject goNote = null;

    TimingManager timingManager = null;
    JudgementEffect judgementEffect = null;

    void Start()
    {
        timingManager = GetComponent<TimingManager>();
        judgementEffect = GetComponent<JudgementEffect>();
        rankingPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(remainTime >= 0)
        {
            remainTime -= Time.deltaTime;
            currentAppearTime += Time.deltaTime;

            timeSlider.value = remainTime / timeLimit;

            if(currentAppearTime >= 60d / appearTime) // 노트 출현 간격
            {
                GameObject note = Instantiate(goNote, noteAppearTf.position, Quaternion.identity);
                note.transform.SetParent(this.transform);
                timingManager.boxNoteList.Add(note);
                currentAppearTime -= 60d / appearTime;
            }
        }
        else 
        {
            rankingPanel.SetActive(true);
            MagicBasicsScore.Instance.Ranking();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Note"))
        {
            judgementEffect.JudgementAnim(2);

            timingManager.boxNoteList.Remove(other.gameObject);
            Destroy(other.gameObject, 0.1f);
        }   
    }
}
