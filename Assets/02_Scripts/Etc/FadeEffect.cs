using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class FadeEffect : MonoBehaviour
{
    [Range(0.01f, 10f)]
    public float fadeTime;
    [Range(0.01f, 10f)]
    public float waitTime = 1.0f;
    Image img;
    [SerializeField]
    GameObject player;
    private void Awake()
    {
        img = GetComponent<Image>();
    }
    public void OnEnable()
    {
        this.gameObject.SetActive(true);
        img.color = Color.black;
        player.GetComponent<PlayerController>().ChangeActive();
        Invoke("StartFade", waitTime);
    }
    void StartFade()
    {
        StartCoroutine(Fade(1, 0));
    }

    IEnumerator Fade(float start, float end)
    {
        float curTime = 0f;
        float percent = 0f;

        while (percent < 1)
        {
            curTime += Time.deltaTime;
            percent = curTime / fadeTime;

            Color color = img.color;
            color.a = Mathf.Lerp(start, end, percent);
            img.color = color;

            yield return null;
        }
        player.GetComponent<PlayerController>().ChangeActive();
        this.gameObject.SetActive(false);
    }

}
