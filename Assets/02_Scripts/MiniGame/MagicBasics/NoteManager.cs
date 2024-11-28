using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] TextMeshProUGUI countdownText = null;
    [SerializeField] RectTransform clockCenter = null;

    TimingManager timingManager = null;
    JudgementEffect judgementEffect = null;

    void Start()
    {
        timingManager = GetComponent<TimingManager>();
        judgementEffect = GetComponent<JudgementEffect>();
        rankingPanel.SetActive(false);

        SoundManager.Instance.StopMusic();
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        countdownText.gameObject.SetActive(true);

        Vector3 originalScale = countdownText.transform.localScale;
        Color countdownColor = countdownText.color;

        SoundManager.Instance.PlaySFX("SFX_StartSign_01");
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            countdownText.transform.localScale = originalScale * 0.5f;

            for (float t = 0; t <0.5f; t += Time.deltaTime)
            {
                float alpha = Mathf.Lerp(0, 1, t / 0.5f);
                float scale = Mathf.Lerp(0.5f, 1f, t / 0.5f);
                countdownColor.a = alpha;
                countdownText.color = countdownColor;
                countdownText.transform.localScale = originalScale * scale;
                yield return null;
            }

            yield return new WaitForSeconds(0.25f);
        }

        countdownText.text = "Start!";;
        countdownText.transform.localScale = originalScale * 0.5f;

        for (float t = 0; t < 0.5f; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(0, 1, t / 0.5f);
            float scale = Mathf.Lerp(0.5f, 1f, t / 0.5f);
            countdownColor.a = alpha;
            countdownText.color = countdownColor;
            countdownText.transform.localScale = originalScale * scale;
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);

        countdownText.gameObject.SetActive(false);
        StartCoroutine(MiniGameStart());
    }

    IEnumerator MiniGameStart()
    {
        SoundManager.Instance.PlayMusic("BGM_MiniGame_MagicTheory_01");
        float elapsed = 0f;
        remainTime = 30f;
        StartCoroutine(ClockRotate());

        while (elapsed < timeLimit)
        {
            remainTime -= Time.deltaTime;
            elapsed += Time.deltaTime;
            currentAppearTime += Time.deltaTime;

            timeSlider.value = remainTime / timeLimit;

            if(currentAppearTime >= 60d / appearTime) // 노트 출현 간격
            {
                GameObject note = Instantiate(goNote, noteAppearTf.position, Quaternion.identity, this.transform);
                // note.transform.SetParent(this.transform);
                timingManager.boxNoteList.Add(note);
                currentAppearTime -= 60d / appearTime;
            }
            yield return null;
        }

        ScoreUI();
        yield return null;
    }

    void ScoreUI()
    {
        rankingPanel.SetActive(true);
        MagicBasicsScore.Instance.IsPlaying = false;
        MagicBasicsScore.Instance.RankingUI();

        Debug.Log("Game Over");

        StopAllCoroutines();
        SoundManager.Instance.PlaySFX("SFX_MagicTheory_Miss_01");
    }

    IEnumerator ClockRotate()
    {
        float rotateSpeed = 360f / timeLimit;
        float elapsed = 0f;

        while (elapsed < timeLimit)
        {
            clockCenter.Rotate(Vector3.forward, -rotateSpeed * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Note"))
        {
            // List<GameObject> boxes = new List<GameObject>(timingManager.boxNoteList);
            judgementEffect.JudgementAnim(2);

            timingManager.boxNoteList.Remove(other.gameObject);
            other.gameObject.SetActive(false);

            Destroy(other.gameObject, 1f);
            // timingManager.boxNoteList = boxes;
        }   
    }

    public void GameEnd()
    {
        Debug.Log("Game End");
        SoundManager.Instance.PlayMusic("BGM_Academy_01");
        // 권희준 - 미니게임 끝나고 아카데미 씬 플레이어 다시 활성화
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().ChangeActive(true);

        GameObject parent = transform.parent.gameObject;
        Managers.Prefab.Destroy(parent);
    }
}
