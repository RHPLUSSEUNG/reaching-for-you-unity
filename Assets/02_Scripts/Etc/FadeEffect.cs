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

    PlayerController playerController;

    bool isFading;
    private void Awake()
    {
        playerController = player.GetComponent<PlayerController>();
        img = GetComponent<Image>();
        StartFadeEffect();
    }
    public void StartFadeEffect()
    {
        if (isFading)
        {
            return;
        }
        this.gameObject.SetActive(true);
        isFading = true;
        img.color = Color.black;
        playerController.ChangeActive(false);
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
        isFading = false;
        playerController.ChangeActive(true);
        this.gameObject.SetActive(false);
    }

}
