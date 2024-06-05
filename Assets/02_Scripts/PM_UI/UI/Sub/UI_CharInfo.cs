using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_CharInfo : UI_Base
{
    public UI_Repair repairUI;
    enum CharInfoUI
    {
        Level,
        CharIcon,
        ElementIcon,
        CharacterName,
        DirectImage
    }

    TestPlayerInfo playerStat;
    Equip_Item playerEquip;
    public override void Init()
    {
        Bind<GameObject>(typeof(CharInfoUI));

        GameObject charIcon = Get<GameObject>((int)CharInfoUI.CharIcon);
        BindEvent(charIcon, OnClickIcon, Define.UIEvent.Click);
    }

    public void SetCharInfo(TestPlayerInfo info, Equip_Item equipInfo)
    {
        GameObject level = GetObject((int)CharInfoUI.Level);
        Text levelText = level.GetComponent<Text>();
        levelText.text = info.level;
        GameObject icon = Get<GameObject>((int)CharInfoUI.CharIcon);
        Image charIcon = icon.GetComponent<Image>();
        charIcon.sprite = info.iconImage;
        GameObject element = Get<GameObject>((int)CharInfoUI.ElementIcon);
        Image elementIcon = element.GetComponent<Image>();
        elementIcon.sprite = info.element;
        GameObject name = Get<GameObject>((int)CharInfoUI.CharacterName);
        Text charName = name.GetComponent<Text>();
        charName.text = info.charname;

        playerStat = info;
        playerEquip = equipInfo;
    }

    public void OnClickIcon(PointerEventData data)
    {
        repairUI.UpdatePlayerInfo(playerStat, playerEquip);
        
    }
}
