using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelectUI : UI_Popup
{
    enum SelectUI
    {
        SkillSelectPanel,
        PortraitName,
        CharacterPortrait,
        EquipSkillPanel,
        SkillContent
    }

    int equip_Skill_Count = 0;
    int max_equip_skill = 5;

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(SelectUI));

        SetCharacterInfo();
        AddAllSelectSkill();
    }

    public void SetCharacterInfo()
    {
        Image portrait = GetObject((int)SelectUI.CharacterPortrait).GetComponent<Image>();
        // portrait.sprite = TODO : sprite 삽입;

        TextMeshProUGUI charName = GetObject((int)SelectUI.PortraitName).GetComponent<TextMeshProUGUI>();
        charName.text = "아나스타샤";        // 캐릭터 이름 설정
    }

    public void AddSelectSkill(int skillID)
    {

    }

    public void AddAllSelectSkill()
    {
        DataList data = GameObject.Find("DataList").GetComponent<DataList>();
        int skillCount = data.playerSkillList.Count;
        Transform content = GetObject((int)SelectUI.SkillContent).transform;
        for (int id = 0; id < skillCount; id++)
        {
            SkillUI skillUI = Managers.UI.MakeSubItem<SkillUI>(content, "Skill");
            SkillData skillData = Managers.Data.GetPlayerSkillData(id);
            skillUI.SetSkillID(id);
            skillUI.SetInfo(skillData);
        }
        
    }

    public void IncreaseEquipCount()
    {
        if(equip_Skill_Count >= max_equip_skill)
        {
            Debug.Log("Equip Skill Count Over!");
            Managers.BattleUI.warningUI.SetText("캐릭터는 마법을 5개까지만 외울 수 있습니다!");
            Managers.BattleUI.warningUI.ShowWarningUI();
            return;
        }
        equip_Skill_Count++;
    }

    public void DecreaseEquipCount()
    {
        if(equip_Skill_Count <= 0)
        {
            equip_Skill_Count = 0;
            return;
        }
        equip_Skill_Count--;
    }
}
