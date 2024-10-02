using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleInfoUI : UI_Popup
{
    enum battleInfoUI
    {
        Blocker,
        InfoNameText,
        CharacterIcon,
        HP,
        MP,
        // TODO : CharacterState 추가
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(battleInfoUI));

        Managers.BattleUI.battleInfoUI = gameObject.GetComponent<BattleInfoUI>();

        GameObject blocker = GetObject((int)battleInfoUI.Blocker);
        BindEvent(blocker, ClickBlocker, Define.UIEvent.Click);
    }

    public void SetInfo(GameObject character)
    {         
        EntityStat stat = character.GetComponent<EntityStat>();
        CharacterState state = character.GetComponent<CharacterState>();

        TextMeshProUGUI nameText = GetObject((int)battleInfoUI.InfoNameText).GetComponent<TextMeshProUGUI>();
        string originalName = character.name;
        string cleanName = originalName.Replace("(Clone)", "").Trim();
        nameText.text = cleanName;
        Image icon = GetObject((int)battleInfoUI.CharacterIcon).GetComponent<Image>();
        // TODO : Character Icon 반영
        // Temp : 임시로 스프라이트 가져오기
        Sprite charImage = Util.FindChild(character, "Character", true).GetComponent<SpriteRenderer>().sprite;
        icon.sprite = charImage;

        string hpText;
        string mpText;
        hpText = $"{stat.Hp} / {stat.MaxHp}";
        GetObject((int)battleInfoUI.HP).GetComponent<TextMeshProUGUI>().text = hpText;
        mpText = $"{stat.Mp} / {stat.MaxMp}";
        GetObject((int)battleInfoUI.MP).GetComponent<TextMeshProUGUI>().text = mpText;

        // TODO : State 반영
    }

    public void SetPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        RectTransform uiTransform = gameObject.GetComponent<RectTransform>();

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float uiWidth = uiTransform.rect.width;
        float uiHeight = uiTransform.rect.height;

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

        uiTransform.position = uiPos;
    }

    public void ClickBlocker(PointerEventData data)
    {
        Managers.Prefab.Destroy(gameObject);
    }
}
