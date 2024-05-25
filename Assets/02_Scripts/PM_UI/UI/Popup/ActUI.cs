using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActUI : UI_Popup
{
    enum actUI
    {
        MagicPanel,
        ItemPanel,
        MagicUseButton,
        ItemUseButton,
        DefenseButton,
        MagicButtonLayout,
        ItemLayout
    }

    GameObject magicPanel;
    GameObject itemPanel;
    GameObject magicBtnLayout;

    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(actUI));

        GameObject magicUseBtn = GetObject((int)actUI.MagicUseButton);
        GameObject itemUseBtn = GetObject((int)actUI.ItemUseButton);
        GameObject defenseBtn = GetObject((int)actUI.DefenseButton);
        magicPanel = GetObject((int)actUI.MagicPanel);
        itemPanel = GetObject((int)actUI.ItemPanel);
        magicBtnLayout = GetObject((int)actUI.MagicButtonLayout);

        BindEvent(magicUseBtn, UseMagicButtonClick, Define.UIEvent.Click);
        BindEvent(itemUseBtn, UseItemButtonClick, Define.UIEvent.Click);
        BindEvent(defenseBtn, UseDefenseButtonClick, Define.UIEvent.Click);

        PM_UI_Manager.UI.HideUI(magicPanel);
        PM_UI_Manager.UI.HideUI(itemPanel);

    }

    public void UpdateCharacterInfo()
    {
        SkillList skillList = Managers.Battle.currentCharacter.GetComponent<SkillList>();
        for (int i = 0; i < skillList.list.Count; i++)
        {
            MagicButtonUI magicBtn = magicBtnLayout.transform.GetChild(i).GetComponent<MagicButtonUI>();
            magicBtn.SetSkill(skillList.list[i]);
        }
        if (magicBtnLayout.transform.childCount < skillList.list.Count)
        {
            for (int i = skillList.list.Count; i < magicBtnLayout.transform.childCount; i++)
            {
                magicBtnLayout.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void UseMagicButtonClick(PointerEventData data)
    {
        PM_UI_Manager.UI.ShowUI(magicPanel);
        PM_UI_Manager.UI.HideUI(itemPanel);
    }

    public void UseItemButtonClick(PointerEventData data)
    {
        PM_UI_Manager.UI.ShowUI(itemPanel);
        PM_UI_Manager.UI.HideUI(magicPanel);
    }

    public void UseDefenseButtonClick(PointerEventData data)
    {
        PM_UI_Manager.UI.HideUI(magicPanel);
        PM_UI_Manager.UI.HideUI(itemPanel);
        PM_UI_Manager.UI.HideUI(gameObject);
    }
}
