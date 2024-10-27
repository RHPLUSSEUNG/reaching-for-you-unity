using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Repair : UI_Popup
{
    enum repairUI
    {
        RepairPanel,
        SkillSelectPanel,
        Level,
        CharacterIcon,
        ElementIcon,
        CharacterName,
        HPBar,
        MPBar,
        CloseButton,
        InvenTabButton,
        SkillTabButton
    }

    public Equip_Item focusEquip;
    public PlayerStat focusStat;

    GameObject repairPanel;
    GameObject skillPanel;
    GameObject[] playerList = new GameObject[4];
    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(repairUI));

        Managers.BattleUI.repairUI = GetComponent<UI_Repair>();

        // Managers.InvenUI.player = playerList[0];
        GameObject close = GetObject((int)repairUI.CloseButton);
        BindEvent(close, OnCloseButton, Define.UIEvent.Click);

        GameObject invenTabBtn = GetObject((int)repairUI.InvenTabButton);
        BindEvent(invenTabBtn, ClickInvenTab, Define.UIEvent.Click);

        GameObject skillTabBtn = GetObject((int)repairUI.SkillTabButton);
        BindEvent(skillTabBtn, ClickSkillTab, Define.UIEvent.Click);

        repairPanel = GetObject((int)repairUI.RepairPanel);
        skillPanel = GetObject((int)repairUI.SkillSelectPanel);
    }

    public void UpdatePlayerInfo()
    {
        PlayerStat statInfo = Managers.InvenUI.player.GetComponent<PlayerStat>();
        Equip_Item equipInfo = Managers.InvenUI.player.GetComponent<Equip_Item>();

        focusStat = statInfo;
        focusEquip = equipInfo;

        GameObject level = GetObject((int)repairUI.Level);
        Text levelText = level.GetComponent<Text>();
        levelText.text = statInfo.Level.ToString();
        GameObject icon = GetObject((int)repairUI.CharacterIcon);
        //Image charIcon = icon.GetComponent<Image>();
        //charIcon.sprite = statInfo.iconImage;
        //GameObject element = GetObject((int)repairUI.ElementIcon);
        //Image elementIcon = element.GetComponent<Image>();
        //elementIcon.sprite = statInfo.element;
        //GameObject name = GetObject((int)repairUI.CharacterName);
        //Text charName = name.GetComponent<Text>();
        //charName.text = statInfo.charname;

        BarUI hpBar = GetObject((int)repairUI.HPBar).GetComponent<BarUI>();
        BarUI mpBar = GetObject((int)repairUI.MPBar).GetComponent<BarUI>();
        hpBar.SetPlayerStat(statInfo.Hp, statInfo.MaxHp);
        mpBar.SetPlayerStat(statInfo.Mp, statInfo.MaxMp);

        Managers.InvenUI.SetPlayerEquipUI(equipInfo);
    }

    public void OnCloseButton(PointerEventData data)
    {
        Managers.UI.HideUI(gameObject);
    }

    public void ClickInvenTab(PointerEventData data)
    {
        repairPanel.SetActive(true);
        skillPanel.SetActive(false);
    }

    public void ClickSkillTab(PointerEventData data)
    {
        repairPanel.SetActive(false);
        skillPanel.SetActive(true);
    }
}
