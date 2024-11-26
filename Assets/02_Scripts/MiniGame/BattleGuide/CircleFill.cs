using UnityEngine;
using UnityEngine.UI;

public class CircleFill : MonoBehaviour
{
    RectTransform image;
    public float fillSpeed = 5f;

    float elapsedTime = 0f;

    Vector2 startScale = new Vector2(0.1f, 0.1f); 
    Vector2 endScale = new Vector2(1f, 1f);
    void Start()
    {
        image = GetComponent<RectTransform>();
        image.localScale = startScale;
    }

    void FixedUpdate()
    {
        if (elapsedTime < fillSpeed)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / fillSpeed);
            image.localScale = Vector2.Lerp(startScale, endScale, progress);
        }
        else
        {
            gameObject.GetComponentInParent<HitZone>().Activate();
        }
    }
}
