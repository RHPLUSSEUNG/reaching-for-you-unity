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
    float waitTime = 3.0f;

    RectTransform enemySide;
    RectTransform friendlySide;
    Image fadeImage;

    [SerializeField]
    List<Sprite> character_sprite_List = new List<Sprite>();
    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(vsUI));

        enemySide = GetObject((int)vsUI.EnemySide).GetComponent<RectTransform>();
        friendlySide = GetObject((int)vsUI.FriendlySide).GetComponent<RectTransform>();

        fadeImage = GetObject((int)vsUI.FadeImage).GetComponent<Image>();
        fadeImage.gameObject.SetActive(false);

        StartVersusUI();
    }

    public void StartVersusUI()
    {
        SetCharacterImage();
        StartCoroutine(VSAnim());
    }

    void SetCharacterImage()
    {
        GameObject enemyLayout = GetObject((int)vsUI.EnemyLayout);
        GameObject friedlyLayout = GetObject((int)vsUI.FriendlyLayout);
        for(int i = 0; i < Managers.Party.monsterParty.Count; i++)
        {
            GameObject newImg = new GameObject("CharacterImage");
            newImg.transform.SetParent(enemyLayout.transform);
            newImg.transform.rotation = Quaternion.Euler(0, 180, 0);
            Image imgSprite = newImg.AddComponent<Image>();
            imgSprite.preserveAspect = true;
            Sprite sprite = ApplyTurnUISprite(Managers.Party.monsterParty[i].name);
            imgSprite.sprite = sprite;
        }
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
    Sprite ApplyTurnUISprite(string character_name)
    {
        Sprite character_sprite = null;
        switch (character_name)
        {
            case "Player_Girl_Battle(Clone)":
                character_sprite = character_sprite_List[0];
                break;
            case "Enemy_Crab(Clone)":
                character_sprite = character_sprite_List[1];
                break;
            case "Enemy_Lizard(Clone)":
                character_sprite = character_sprite_List[2];
                break;
            case "Enemy_Worker(Clone)":
                character_sprite = character_sprite_List[3];
                break;
            case "Enemy_Soldier(Clone)":
                character_sprite = character_sprite_List[4];
                break;
            case "Enmey_Golem(Clone)":
                character_sprite = character_sprite_List[5];
                break;
            case "Enemy_Queen(Clone)":
                character_sprite = character_sprite_List[6];
                break;

            default:
                Debug.Log("Not In Character_Sprite List");
                break;
        }

        return character_sprite;
    }
    void DestroyUI()
    {
        Managers.Prefab.Destroy(gameObject);
    }
}
