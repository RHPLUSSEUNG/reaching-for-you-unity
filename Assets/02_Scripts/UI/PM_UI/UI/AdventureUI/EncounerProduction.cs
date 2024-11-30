using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounerProduction : MonoBehaviour
{
    [SerializeField]
    RectTransform maskImage;

    float duration = 1.5f;

    public float GetWaitTime()
    {
        return duration;
    }

    public IEnumerator Production()
    {
        float elapsed = 0f;
        Vector2 initialSize = new Vector2(10000f, 10000f);
        Vector2 targetSize = new Vector2(0f, 0f);
        while (elapsed < duration)
        {
            maskImage.sizeDelta = Vector2.Lerp(initialSize, targetSize, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        maskImage.sizeDelta = targetSize;
    }
}
