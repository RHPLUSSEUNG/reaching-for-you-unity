using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JudgeTextUI : UI_Base
{
    Image textImage;

    float distance = 100f;
    float duration = 1.0f;
    public override void Init()
    {
        textImage = GetComponent<Image>();
    }

    public void SetJudgeTextImage(Vector3 instantiatePos)
    {
        transform.position = instantiatePos + new Vector3(0, 100);
        StartCoroutine(TextAnimation());    
    }

    IEnumerator TextAnimation()
    {
        float elapsed = 0f;
        Vector3 startPos = transform.position;
        Color startColor = GetComponent<Image>().color;

        while (elapsed < duration)
        {
            float newPos = Mathf.Lerp(startPos.y, startPos.y + distance, elapsed /duration);
            transform.position = new Vector3(startPos.x, newPos, startPos.z);

            Color newColor = startColor;
            newColor.a = Mathf.Lerp(1, 0, elapsed /duration);
            GetComponent<Image>().color = newColor;
            
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = new Vector3(startPos.x, startPos.y + distance, startPos.z);
        Color endColor = startColor;
        endColor.a = 0f;
        GetComponent<Image>().color = endColor;

        DistroyTextImage();
    }

    void DistroyTextImage()
    {
        Managers.Prefab.Destroy(gameObject);
    }
}
