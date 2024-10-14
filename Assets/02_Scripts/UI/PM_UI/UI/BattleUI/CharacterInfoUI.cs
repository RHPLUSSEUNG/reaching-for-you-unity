using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterInfoUI : BattleInfoUI
{
    enum characterInfoUI
    {
        Blocker,
        InfoPanel,
        InfoNameText,
        CharacterIcon,
        HP,
        MP,
        // TODO : CharacterState 추가
    }

    RectTransform panelRect;

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(characterInfoUI));

        Managers.BattleUI.battleInfoUI = gameObject.GetComponent<BattleInfoUI>();

        GameObject blocker = GetObject((int)characterInfoUI.Blocker);
        BindEvent(blocker, ClickBlocker, Define.UIEvent.Click);

        panelRect = GetObject((int)characterInfoUI.InfoPanel).GetComponent<RectTransform>();

        StartCoroutine(AnimPopup(panelRect));
    }

    public override void SetInfo(GameObject character)
    {         
        EntityStat stat = character.GetComponent<EntityStat>();
        CharacterState state = character.GetComponent<CharacterState>();

        TextMeshProUGUI nameText = GetObject((int)characterInfoUI.InfoNameText).GetComponent<TextMeshProUGUI>();
        string originalName = character.name;
        string cleanName = originalName.Replace("(Clone)", "").Trim();
        nameText.text = cleanName;
        Image icon = GetObject((int)characterInfoUI.CharacterIcon).GetComponent<Image>();
        // TODO : Character Icon 반영
        // Temp : 임시로 스프라이트 가져오기
        Sprite charImage = Util.FindChild(character, "Character", true).GetComponent<SpriteRenderer>().sprite;
        icon.sprite = charImage;

        string hpText;
        string mpText;
        hpText = $"{stat.Hp} / {stat.MaxHp}";
        GetObject((int)characterInfoUI.HP).GetComponent<TextMeshProUGUI>().text = hpText;
        mpText = $"{stat.Mp} / {stat.MaxMp}";
        GetObject((int)characterInfoUI.MP).GetComponent<TextMeshProUGUI>().text = mpText;

        // TODO : State 반영
    }

    public override void SetPosition()
    {
        Vector3 mousePos = Input.mousePosition;

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float uiWidth = panelRect.rect.width;
        float uiHeight = panelRect.rect.height;

        Vector3 uiPos = mousePos;
        if (mousePos.x + uiWidth > screenWidth)
        {
            uiPos.x = mousePos.x - (uiWidth / 2);
        }
        else
        {
            uiPos.x = mousePos.x + (uiWidth / 2);
        }
        if (mousePos.y - uiHeight < 0)
        {
            uiPos.y = mousePos.y + (uiHeight / 2);
        }
        else
        {
            uiPos.y = mousePos.y;
        }

        panelRect.position = uiPos;
    }

    public void ClickBlocker(PointerEventData data)
    {
        StartCoroutine(ClosePopupUIAnim());
    }

    IEnumerator ClosePopupUIAnim()
    {
        StartCoroutine(CloseAnimPopup(panelRect));
        yield return new WaitForSeconds(animDuration);
        Managers.Prefab.Destroy(gameObject);
    }
}
