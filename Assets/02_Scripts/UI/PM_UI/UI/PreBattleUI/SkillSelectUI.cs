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
        PortraitName,
        CharacterPortrait,
        EquipSkillPanel,
        SkillContent,
        SkillEquipButton
    }

    [SerializeField]
    int select_Skill_Count = 0;
    [SerializeField]
    int equip_Skill_Count = 0;
    
    [SerializeField]
    List<int> select_skill_list = new List<int>();
    [SerializeField]
    List<int> equip_skill_list = new List<int>();
    [SerializeField]
    List<GameObject> skill_UI_List = new List<GameObject>();

    [SerializeField]
    int max_equip_skill = 5;

    //[SerializeField]
    //GameObject player;

    // [TODO] : 동료 추가 시 동료 스킬 반영

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(SelectUI));

        Managers.BattleUI.skillSelectUI = GetComponent<SkillSelectUI>();
        GameObject skillEquipBtn = GetObject((int)SelectUI.SkillEquipButton);
        BindEvent(skillEquipBtn, ClickSkillEquipButton, Define.UIEvent.Click);

        SetCharacterInfo();
        AddAllSelectSkill();
    }

    public void SetCharacterInfo()
    {
        Image portrait = GetObject((int)SelectUI.CharacterPortrait).GetComponent<Image>();

        TextMeshProUGUI charName = GetObject((int)SelectUI.PortraitName).GetComponent<TextMeshProUGUI>();
        charName.text = "아나스타샤";        // 캐릭터 이름 설정
    }

    public void AddSelectSkill(int id)
    {
        Transform content = GetObject((int)SelectUI.SkillContent).transform;
        SkillUI skill_ui = Managers.UI.MakeSubItem<SkillUI>(content, "Skill");
        SkillData skillData = Managers.Data.GetPlayerSkillData(id);
        skill_ui.SetSkillID(id);
        skill_ui.SetInfo(skillData);

        skill_ui.transform.SetSiblingIndex(id);
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

    public bool AddSelectSkillList(int skill_ID, GameObject skill_UI)
    {
        if(select_Skill_Count >= max_equip_skill)
        {
            Debug.Log("Select Skill Count Over!");
            Managers.BattleUI.warningUI.SetText("캐릭터는 한번에 마법을 5개까지만 외울 수 있습니다!");
            Managers.BattleUI.warningUI.ShowWarningUI();
            return false;
        }
        if(select_skill_list.Contains(skill_ID))
        {
            Debug.Log("Skill Already Exist");
            return false;
        }
        select_skill_list.Add(skill_ID);
        skill_UI_List.Add(skill_UI);
        select_Skill_Count++;
        return true;
    }

    public bool RemoveSelectSkillList(int skill_ID, GameObject skill_UI)
    {
        if (select_Skill_Count <= 0)
        {
            select_Skill_Count = 0;
            return false;
        }
        if(!select_skill_list.Contains(skill_ID))
        {
            Debug.Log("Skill Not Exist in Skill List");
            return false;
        }
        select_skill_list.Remove(skill_ID);
        skill_UI_List.Remove(skill_UI);
        select_Skill_Count--;
        return true;
    }

    public void UpdateEquipSkillUI()
    {
        equip_Skill_Count += select_Skill_Count;
        select_Skill_Count = 0;
        for(int i = 0; i < select_skill_list.Count; i++)
        {
            equip_skill_list.Add(select_skill_list[i]);
        }

        Transform equipPanel = GetObject((int)SelectUI.EquipSkillPanel).transform;
        for(int idx = 0; idx < select_skill_list.Count; idx++)
        {
            int id = select_skill_list[idx];
            SkillUI skillUI = Managers.UI.MakeSubItem<SkillUI>(equipPanel, "Skill");
            SkillData skillData = Managers.Data.GetPlayerSkillData(id);
            skillUI.isEquiped = true;
            skillUI.SetSkillID(id);
            skillUI.SetInfo(skillData);
        }

        select_skill_list.Clear();
    }

    public void UnEquipSkill(int id)
    {
        if (equip_Skill_Count <= 0)
        {
            Debug.Log("Skill is None");
            return;
        }
        equip_Skill_Count--;
        equip_skill_list.Remove(id);
        SkillList skillList = Managers.BattleUI.player.GetComponent<SkillList>();
        skillList.RemoveSkill(id);

        AddSelectSkill(id);
    }

    public void ClickSkillEquipButton(PointerEventData data)
    {
        Debug.Log($"Player : {Managers.BattleUI.player}");
        if(select_skill_list.Count == 0)
        {
            Debug.Log("Skill Not Select");
            return;
        }
        if(!Managers.BattleUI.player.CompareTag("Player"))
        {
            Managers.BattleUI.warningUI.SetText("캐릭터를 먼저 생성해주세요!");
            Managers.BattleUI.warningUI.ShowWarningUI();
            return;
        }

        SkillList skillList = Managers.BattleUI.player.GetComponent<SkillList>();
        int equipCount = skillList.idList.Count + select_Skill_Count;
        if(equipCount > max_equip_skill)
        {
            Managers.BattleUI.warningUI.SetText("캐릭터는 한번에 마법을 5개까지만 외울 수 있습니다!");
            Managers.BattleUI.warningUI.ShowWarningUI();
            return;
        }
        for(int i = 0; i < select_skill_list.Count; i++)
        {
            skillList.AddSkill(select_skill_list[i]);
        }

        for(int i = 0; i < skill_UI_List.Count;i++)
        {
            Managers.Prefab.Destroy(skill_UI_List[i]);
        }
        skill_UI_List.Clear();

        UpdateEquipSkillUI();
    }
}
