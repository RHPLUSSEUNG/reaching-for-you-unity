using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_CharInfo : UI_Base
{
    enum CharInfoUI
    {
        LevelText,
        CharIcon,
        ElementIcon,
        CharacterName,
        DirectImage
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(CharInfoUI));

        GameObject charIcon = Get<GameObject>((int)CharInfoUI.CharIcon);
        BindEvent(charIcon, OnClickIcon, Define.UIEvent.Click);
    }

    public void SetCharInfo(TestPlayerInfo info)
    {
        GameObject level = Get<GameObject>((int)CharInfoUI.LevelText);
        Text levelText = level.GetComponent<Text>();
        levelText.text = info.level;
        GameObject icon = Get<GameObject>((int)CharInfoUI.CharIcon);
        Image charIcon = icon.GetComponent<Image>();
        charIcon.sprite = info.charIcon;
        GameObject element = Get<GameObject>((int)CharInfoUI.ElementIcon);
        Image elementIcon = element.GetComponent<Image>();
        elementIcon.sprite = info.element;
        GameObject name = Get<GameObject>((int)CharInfoUI.CharacterName);
        Text charName = name.GetComponent<Text>();
        charName.text = info.name;
    }

    public void OnClickIcon(PointerEventData data)
    {
        
        
    }
}
