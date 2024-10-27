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

    [SerializeField]
    int equip_Skill_Count = 0;
    [SerializeField]
    int max_equip_skill = 5;
    [SerializeField]
    List<int> select_skill_list = new List<int>();
    [SerializeField]
    List<GameObject> skill_UI_List = new List<GameObject>();

    [SerializeField]
    GameObject player;

    // [TODO] : ���� �߰� �� ���� ��ų �ݿ�

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(SelectUI));

        Managers.BattleUI.skillSelectUI = GetComponent<SkillSelectUI>();
        GameObject skillEquipBtn = GetObject((int)SelectUI.SkillEquipButton);
        BindEvent(skillEquipBtn, ClickSkillEquipButton, Define.UIEvent.Click);

        player = Managers.Party.playerParty[0];         // Temp(���� �߰� �� ���� ���)
        Debug.Log($"player : {player}");

        SetCharacterInfo();
        AddAllSelectSkill();
    }

    public void SetCharacterInfo()
    {
        Image portrait = GetObject((int)SelectUI.CharacterPortrait).GetComponent<Image>();
        // portrait.sprite = TODO : sprite ����;

        TextMeshProUGUI charName = GetObject((int)SelectUI.PortraitName).GetComponent<TextMeshProUGUI>();
        charName.text = "�Ƴ���Ÿ��";        // ĳ���� �̸� ����
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
        if(equip_Skill_Count >= max_equip_skill)
        {
            Debug.Log("Equip Skill Count Over!");
            Managers.BattleUI.warningUI.SetText("ĳ���ʹ� ������ 5�������� �ܿ� �� �ֽ��ϴ�!");
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
        equip_Skill_Count++;
        return true;
    }

    public bool RemoveSelectSkillList(int skill_ID, GameObject skill_UI)
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
        skill_UI_List.Remove(skill_UI);
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
            skillUI.isEquiped = true;
            skillUI.SetSkillID(id);
            skillUI.SetInfo(skillData);
        }
    }

    public void UnEquipSkill(int id)
    {
        if (equip_Skill_Count <= 0)
        {
            Debug.Log("Skill is None");
            return;
        }
        equip_Skill_Count--;
        select_skill_list.Remove(id);
        SkillList skillList = player.GetComponent<SkillList>();
        skillList.RemoveSkill(id);

        AddSelectSkill(id);
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

        for(int i = 0; i < skill_UI_List.Count;i++)
        {
            Managers.Prefab.Destroy(skill_UI_List[i]);
        }

        UpdateEquipSkillUI();
    }
}
