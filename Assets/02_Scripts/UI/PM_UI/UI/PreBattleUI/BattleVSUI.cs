using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleVSUI : UI_Popup
{
    enum vsUI
    {
        EnemySide,
        EnemyLayout,
        FriendlySide,
        FriendlyLayout,
        FadeImage
    }

    float duration = 0.5f;
    // float exitDuration = 0.25f;
    float waitTime = 3.0f;

    RectTransform enemySide;
    RectTransform friendlySide;
    Image fadeImage;
    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(vsUI));

        enemySide = GetObject((int)vsUI.EnemySide).GetComponent<RectTransform>();
        friendlySide = GetObject((int)vsUI.FriendlySide).GetComponent<RectTransform>();

        fadeImage = GetObject((int)vsUI.FadeImage).GetComponent<Image>();
        fadeImage.gameObject.SetActive(false);
        StartCoroutine(VSAnim());
    }

    IEnumerator VSAnim()
    {
        float elapsed = 0f;
        Vector2 enemyInitialPos = enemySide.anchoredPosition;
        Vector2 friendlyInitialPos = friendlySide.anchoredPosition;
        Vector2 enemyTargetPos = new Vector2(-500, 30);
        Vector2 friendlyTargetPos = new Vector2(500, 0);

        while(elapsed < duration)
        {
            enemySide.anchoredPosition = Vector2.Lerp(enemyInitialPos, enemyTargetPos, elapsed / duration);
            friendlySide.anchoredPosition = Vector2.Lerp(friendlyInitialPos, friendlyTargetPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        enemySide.anchoredPosition = enemyTargetPos;
        friendlySide.anchoredPosition = friendlyTargetPos;

        yield return new WaitForSeconds(waitTime);

        StartCoroutine(ExitAnim());
    }

    IEnumerator ExitAnim()
    {
        float elapsed = 0f;

        Vector2 enemyInitialPos = enemySide.anchoredPosition;
        Vector2 friendlyInitialPos = friendlySide.anchoredPosition;
        Vector2 enemyTargetPos = new Vector2(-3000, 30);
        Vector2 friendlyTargetPos = new Vector2(3000, 0);

        Vector3 initialScale = new Vector3(1, 1);
        Vector3 targetScale = new Vector3(2, 2);
        StartCoroutine(FadeOut());
        while (elapsed < duration)
        {
            enemySide.anchoredPosition = Vector2.Lerp(enemyInitialPos, enemyTargetPos, elapsed / duration);
            friendlySide.anchoredPosition = Vector2.Lerp(friendlyInitialPos, friendlyTargetPos, elapsed / duration);
            enemySide.localScale = Vector3.Lerp(initialScale, targetScale, elapsed / duration);
            friendlySide.localScale = Vector3.Lerp(initialScale, targetScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        enemySide.anchoredPosition = enemyTargetPos;
        friendlySide.anchoredPosition = friendlyTargetPos;
        enemySide.localScale = targetScale;
        friendlySide.localScale = targetScale;

        DestroyUI();
    }

    IEnumerator FadeOut()
    {
        fadeImage.gameObject.SetActive(true);

        float elapsed = 0f;
        Color fadeColor = fadeImage.color;

        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(0, 1, elapsed / duration);
            fadeColor.a = alpha;
            fadeImage.color = fadeColor;

            elapsed += Time.deltaTime;
            yield return null;
        }

        fadeColor.a = 1f;
        fadeImage.color = fadeColor;
    }

    void DestroyUI()
    {
        Managers.Prefab.Destroy(gameObject);
    }
}
