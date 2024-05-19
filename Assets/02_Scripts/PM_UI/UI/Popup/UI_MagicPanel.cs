using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_MagicPanel : UI_Popup
{
    // TODO : magic Button 한개로 전부 처리하는 방법
    enum MagicButton
    {
        MagicButton1,
        MagicButton2,
        MagicButton3,
        MagicButton4,
        MagicButton5
    }
    [SerializeField] GameObject ActPanel;
    [SerializeField] Image descriptPanel;

    public ButtonState state = ButtonState.Idle;

    public GameObject skill;
    public SkillList skillList;

    public override void Init()
    {
        Bind<Button>(typeof(MagicButton));

        Button magicBtn1 = GetButton((int)MagicButton.MagicButton1);
        Button magicBtn2 = GetButton((int)MagicButton.MagicButton2);
        Button magicBtn3 = GetButton((int)MagicButton.MagicButton3);
        Button magicBtn4 = GetButton((int)MagicButton.MagicButton4);
        Button magicBtn5 = GetButton((int)MagicButton.MagicButton5);
        BindEvent(magicBtn1.gameObject, MagicButton1Click, Define.UIEvent.Click);
        BindEvent(magicBtn1.gameObject, MagicButton1Enter, Define.UIEvent.Enter);
        BindEvent(magicBtn1.gameObject, MagicButton1Exit, Define.UIEvent.Exit);
        BindEvent(magicBtn2.gameObject, MagicButton2Click, Define.UIEvent.Click);
        BindEvent(magicBtn2.gameObject, MagicButton2Enter, Define.UIEvent.Enter);
        BindEvent(magicBtn2.gameObject, MagicButton2Exit, Define.UIEvent.Exit);
        BindEvent(magicBtn3.gameObject, MagicButton3Click, Define.UIEvent.Click);
        BindEvent(magicBtn3.gameObject, MagicButton3Enter, Define.UIEvent.Enter);
        BindEvent(magicBtn3.gameObject, MagicButton3Exit, Define.UIEvent.Exit);
        BindEvent(magicBtn4.gameObject, MagicButton4Click, Define.UIEvent.Click);
        BindEvent(magicBtn4.gameObject, MagicButton4Enter, Define.UIEvent.Enter);
        BindEvent(magicBtn4.gameObject, MagicButton4Exit, Define.UIEvent.Exit);
        BindEvent(magicBtn5.gameObject, MagicButton5Click, Define.UIEvent.Click);
        BindEvent(magicBtn5.gameObject, MagicButton5Enter, Define.UIEvent.Enter);
        BindEvent(magicBtn5.gameObject, MagicButton5Exit, Define.UIEvent.Exit);
    }

    public void SetSkillList(SkillList info)
    {
        Managers.PlayerButton.state = ButtonState.Idle;
        skillList = info;
        Debug.Log(info);
    }

    public Active GetSkill()
    {
        if (skill == null || skill.GetComponent<Active>() == null)
        {
            return null;
        }
        return skill.GetComponent<Active>();
    }

    public void MagicButton1Click(PointerEventData data)
    {
        skill = skillList.list[0];
        state = ButtonState.Skill;
        Managers.PlayerButton.state = ButtonState.Skill;
        ActPanel.SetActive(false);
        gameObject.SetActive(false);
        descriptPanel.gameObject.SetActive(false);
    }

    public void MagicButton2Click(PointerEventData data)
    {
        Debug.Log("Magic2 Use");
        ActPanel.SetActive(false);
        gameObject.SetActive(false);
        descriptPanel.gameObject.SetActive(false);

        state = ButtonState.Skill;
    }

    public void MagicButton3Click(PointerEventData data)
    {
        Debug.Log("Magic3 Use");
        ActPanel.SetActive(false);
        gameObject.SetActive(false);
        descriptPanel.gameObject.SetActive(false);

        state = ButtonState.Skill;
    }

    public void MagicButton4Click(PointerEventData data)
    {
        Debug.Log("Magic4 Use");
        ActPanel.SetActive(false);
        gameObject.SetActive(false);
        descriptPanel.gameObject.SetActive(false);

        state = ButtonState.Skill;
    }

    public void MagicButton5Click(PointerEventData data)
    {
        Debug.Log("Magic5 Use");
        ActPanel.SetActive(false);
        gameObject.SetActive(false);
        descriptPanel.gameObject.SetActive(false);

        state = ButtonState.Skill;
    }

    public void MagicButton1Enter(PointerEventData data)
    {
        descriptPanel.gameObject.SetActive(true);
    }

    public void MagicButton1Exit(PointerEventData data)
    {
        descriptPanel.gameObject.SetActive(false);
    }

    public void MagicButton2Enter(PointerEventData data)
    {
        descriptPanel.gameObject.SetActive(true);
    }
    public void MagicButton2Exit(PointerEventData data)
    {
        descriptPanel.gameObject.SetActive(false);
    }

    public void MagicButton3Enter(PointerEventData data)
    {
        descriptPanel.gameObject.SetActive(true);
    }
    public void MagicButton3Exit(PointerEventData data)
    {
        descriptPanel.gameObject.SetActive(false);
    }

    public void MagicButton4Enter(PointerEventData data)
    {
        descriptPanel.gameObject.SetActive(true);
    }

    public void MagicButton4Exit(PointerEventData data)
    {
        descriptPanel.gameObject.SetActive(false);
    }

    public void MagicButton5Enter(PointerEventData data)
    {
        descriptPanel.gameObject.SetActive(true);
    }

    public void MagicButton5Exit(PointerEventData data)
    {
        descriptPanel.gameObject.SetActive(false);
    }
}
