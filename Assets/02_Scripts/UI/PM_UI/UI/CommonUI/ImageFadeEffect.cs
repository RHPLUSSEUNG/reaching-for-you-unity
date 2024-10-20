using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFadeEffect : MonoBehaviour
{
    [SerializeField]
    public float fadeTime;
    [SerializeField]
    public float waitTime;

    Image img;
    bool isFade;
    public void StartFadeEffect()
    {
        if(isFade)
        {
            return;
        }
        isFade = true;
        img = GetComponent<Image>();
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(waitTime);

        Color color = img.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            color.a = Mathf.Lerp(1, 0, elapsedTime / fadeTime);
            img.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = 0;
        img.color = color;

        gameObject.SetActive(false);
        isFade = false;
    }
}
