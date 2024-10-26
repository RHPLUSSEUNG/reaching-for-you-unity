using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextFadeEffect : MonoBehaviour
{
    [SerializeField]
    public float fadeTime;
    [SerializeField]
    public float waitTime;

    TextMeshProUGUI textUI;
    bool isFade;

    public void StartFadeEffect()
    {
        if(isFade)
        {
            return;
        }
        isFade = true;
        textUI = GetComponent<TextMeshProUGUI>();
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(waitTime);

        Color color = textUI.color;
        float elapsedTime = 0f;

        while(elapsedTime < fadeTime)
        {
            color.a = Mathf.Lerp(1, 0, elapsedTime / fadeTime);
            textUI.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = 0;
        textUI.color = color;

        gameObject.SetActive(false);
        isFade = false;
    }
}
