using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageTextUI : UI_Base
{
    TextMeshProUGUI damageText;
    float moveDistance = 50f;
    float moveDuration = 0.7f;
    public override void Init()
    {
        damageText = GetComponent<TextMeshProUGUI>();
    }

    public void ShowDamageText(int damage)
    {
        damageText.text = damage.ToString();
        StartCoroutine(DamageAnim());
    }
    
    IEnumerator DamageAnim()
    {
        RectTransform damageRect = GetComponent<RectTransform>();

        Vector3 originalPos = damageRect.transform.position;
        Color originalColor = damageText.color;

        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / moveDuration;
            damageRect.transform.position = originalPos + Vector3.up * (moveDistance * t);

            Color newColor = originalColor;
            newColor.a = Mathf.Lerp(1f, 0f, t);
            damageText.color = newColor;

            yield return null;
        }

        Managers.Prefab.Destroy(gameObject);
    }
}
