using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillUI : UI_Base
{
    enum selectSkillUI
    {
        SelectPanel,
        SkillName,
        SkillType
    }

    GameObject selectPanel;
    [SerializeField]
    int skill_ID;
    bool isSelected = false;

    public void SetSkillID(int id)
    {
        skill_ID = id;
    }

    public int GetSkillID()
    {
        return skill_ID;
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(selectSkillUI));

        selectPanel = GetObject((int)selectSkillUI.SelectPanel);
        BindEvent(gameObject, ClickSelectPanel, Define.UIEvent.Click);

        selectPanel.SetActive(false);
    }

    public void SetInfo(SkillData data)
    {
        TextMeshProUGUI skill_name = GetObject((int)selectSkillUI.SkillName).GetComponent<TextMeshProUGUI>();
        skill_name.text = data.SkillName;

        TextMeshProUGUI skill_type = GetObject((int)selectSkillUI.SkillType).GetComponent<TextMeshProUGUI>();
        switch (data.SkillType)
        {
            case skillType.Active:
                skill_type.text = "( A )";
                break;

            case skillType.Passive:
                skill_type.text = "( P )";
                break;

            default:
                Debug.Log("Skill Type Error");
                break;
        }
    }

    public void ClickSelectPanel(PointerEventData data)
    {
        if (isSelected)
        {
            isSelected = false;
            selectPanel.SetActive(false);
        }
        else
        {
            isSelected = true;
            selectPanel.SetActive(true);
        }
    }
}
