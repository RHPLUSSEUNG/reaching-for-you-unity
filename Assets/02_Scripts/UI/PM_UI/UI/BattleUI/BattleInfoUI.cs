using UnityEngine;
using UnityEngine.UI;

public class BattleInfoUI : UI_Popup
{
    enum battleInfoUI
    {
        CharacterIcon,
        Hp,
        Mp,
        // TODO : CharacterState 추가
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(battleInfoUI));

        Managers.BattleUI.battleInfoUI = gameObject.GetComponent<BattleInfoUI>();
    }

    public void SetInfo(GameObject character)
    {         
        EntityStat stat = character.GetComponent<EntityStat>();
        CharacterState state = character.GetComponent<CharacterState>();

        Image icon = GetObject((int)battleInfoUI.CharacterIcon).GetComponent<Image>();
        // TODO : Character Icon 반영

        string hpText;
        string mpText;
        hpText = $"{stat.MaxHp} / {stat.Hp}";
        GetObject((int)battleInfoUI.Hp).GetComponent<Text>().text = hpText;
        mpText = $"{stat.MaxMp} / {stat.Mp}";
        GetObject((int)battleInfoUI.Mp).GetComponent<Text>().text = mpText;

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
}
