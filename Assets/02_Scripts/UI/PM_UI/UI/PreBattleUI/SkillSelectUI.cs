using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSelectUI : UI_Popup
{
    enum SelectUI
    {
        SkillSelectPanel,
        PortraitName,
        CharacterPortrait,
        EquipSkillPanel,
        SkillContent,
        SkillEquipButton
    }

    int equip_Skill_Count = 0;
    int max_equip_skill = 5;
    List<int> select_skill_list = new List<int>();

    GameObject player;

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(SelectUI));

        Managers.BattleUI.skillSelectUI = GetComponent<SkillSelectUI>();
        GameObject skillEquipBtn = GetObject((int)SelectUI.SkillEquipButton);
        BindEvent(skillEquipBtn, ClickSkillEquipButton, Define.UIEvent.Click);

        player = Managers.Party.playerParty[0];         // Temp
        Debug.Log($"player : {player}");

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

    public bool AddSelectSkillList(int skill_ID)
    {
        if(equip_Skill_Count >= max_equip_skill)
        {
            Debug.Log("Equip Skill Count Over!");
            Managers.BattleUI.warningUI.SetText("캐릭터는 마법을 5개까지만 외울 수 있습니다!");
            Managers.BattleUI.warningUI.ShowWarningUI();
            return false;
        }
        if(select_skill_list.Contains(skill_ID))
        {
            Debug.Log("Skill Already Exist");
            return false;
        }
        select_skill_list.Add(skill_ID);
        equip_Skill_Count++;
        return true;
    }

    public bool RemoveSelectSkillList(int skill_ID)
    {
        if (equip_Skill_Count <= 0)
        {
            equip_Skill_Count = 0;
            return false;
        }
        if(!select_skill_list.Contains(skill_ID))
        {
            Debug.Log("Skill Not Exist in Skill List");
            return false;
        }
        select_skill_list.Remove(skill_ID);
        equip_Skill_Count--;
        return true;
    }

    public void UpdateEquipSkillUI()
    {
        Transform equipPanel = GetObject((int)SelectUI.EquipSkillPanel).transform;
        for(int idx = 0; idx < select_skill_list.Count; idx++)
        {
            int id = select_skill_list[idx];
            SkillUI skillUI = Managers.UI.MakeSubItem<SkillUI>(equipPanel, "Skill");
            SkillData skillData = Managers.Data.GetPlayerSkillData(id);
            skillUI.SetSkillID(id);
            skillUI.SetInfo(skillData);
        }
    }

    public void ClickSkillEquipButton(PointerEventData data)
    {
        if(select_skill_list.Count == 0)
        {
            Debug.Log("Skill Not Select");
            return;
        }

        SkillList skillList = player.GetComponent<SkillList>();
        for(int i = 0; i < select_skill_list.Count; i++)
        {
            skillList.AddSkill(select_skill_list[i]);
        }

        for(int i = 0; i < skillList.idList.Count; i++)
        {
            Debug.Log($"Skill ID : {skillList.idList[i]}");
        }

        UpdateEquipSkillUI();
    }
}
