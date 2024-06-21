using System.Collections;
using UnityEngine;

public class SlideEffect : MonoBehaviour
{
    RectTransform _object;
    public float duration = 0.4f;
    public float pauseDuration = 1.0f;
    Vector2 startPos;
    Vector2 endPos;

    public void SetSlideElement()
    {
        _object = gameObject.GetComponent<RectTransform>();
        gameObject.SetActive(true);

        // UI Initialize Position
        startPos = new Vector2(Screen.width, _object.anchoredPosition.y);
        endPos = new Vector2(0, _object.anchoredPosition.y);

        StartCoroutine(SlideStart());
    }


    IEnumerator SlideStart()
    {
        yield return StartCoroutine(SlideAnimation(_object, startPos, endPos, duration));

        yield return new WaitForSeconds(pauseDuration);

        gameObject.SetActive(false);
    }

    IEnumerator SlideAnimation(RectTransform _object, Vector2 start, Vector2 end, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            _object.anchoredPosition = Vector2.Lerp(start, end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        _object.anchoredPosition = end;
    }    
}
