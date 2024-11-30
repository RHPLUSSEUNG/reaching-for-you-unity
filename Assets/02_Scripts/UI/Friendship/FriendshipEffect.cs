using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendshipEffect : MonoBehaviour
{
    float moveSpeed = 0.8f;
    float fadeSpeed = 1f;
    Vector2 startPos;
    RectTransform rectTransform;
    Image heartImage;
    Color color;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        heartImage = GetComponent<Image>();
        color = heartImage.color;
        startPos = rectTransform.anchoredPosition;
    }
    
    private void OnEnable()
    {
        color.a = 1f;
        rectTransform.anchoredPosition = startPos;
        StartCoroutine(HeartEffect());
    }

    IEnumerator HeartEffect()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            rectTransform.anchoredPosition += Vector2.up * moveSpeed * Time.deltaTime;

            color.a -= fadeSpeed * Time.deltaTime;
            heartImage.color = color;

            if (color.a <= 0)
            {
                InActive();
                break;
            }
        }
    }

    void InActive()
    {        
        gameObject.SetActive(false);
    }
}
